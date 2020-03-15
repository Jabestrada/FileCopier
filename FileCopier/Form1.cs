using FileCopier.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FileCopier
{
    public partial class Form1 : Form
    {
        private List<FileListItem> _sourceList;
        private List<FileListItem> _targetList;
        private const string BASE_FORM_CAPTION = "File Copier";
        private string _currentConfigFile = "";

        private ContextMenu _contextMenu;

        private MenuItem _copyTextMenuItem;
        private MenuItem _deleteMenuItem;

        public Form1()
        {
            _sourceList = new List<FileListItem>();
            _targetList = new List<FileListItem>();

            InitializeComponent();

            CreateContextMenus();

            CheckAdminRights();
        }

     
        #region Event handlers
        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItemFromContextMenu(sender);
        }

        private void copyTextMenuItem_Click(object sender, EventArgs e)
        {
            CopySelectedFileItemTextToClipboard(sender);          
        }

        private void ContextMenu_Popup(object sender, EventArgs e)
        {
            EvaluateContextMenuItemDisplay(sender);
        }
       

        private void addSource_Click(object sender, EventArgs e)
        {
            AddSource();
        }

        private void addTarget_Click(object sender, EventArgs e)
        {
            AddTarget();
        }

        private void sources_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterTargetList();
        }

        private void targets_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedTarget();
            }
        }

        private void sources_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedSource();
            }
        }

        private void copy_Click(object sender, EventArgs e)
        {
            DoFileCopy();  
        }

        private void browseSource_Click(object sender, EventArgs e)
        {
            BrowseSource();
        }
        private void browseTarget_Click(object sender, EventArgs e)
        {
            BrowseTarget();
        }

        private void saveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void loadSettings_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }


        #endregion


        private void CheckAdminRights()
        {
            if (!IsAdministrator())
            {
                MessageBox.Show(@"FileCopier is running WITHOUT admin privileges. Copy operations will fail on target folders that require admin access like \inetpub.");
            }
        }

        private void LoadSettings()
        {
            var fileDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "|*.xml"
            };

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                // https://stackoverflow.com/questions/2434534/serialize-an-object-to-string
                XmlSerializer xmlSerializer = new XmlSerializer(new FileItemsSerialization().GetType());
                StringReader textReader = new StringReader(File.ReadAllText(fileDialog.FileName));

                var fileItems = xmlSerializer.Deserialize(textReader) as FileItemsSerialization;
                _sourceList = fileItems.Sources;
                _targetList = fileItems.Targets;

                SetListItems(sources, fileItems.Sources);
                SetListItems(targets, fileItems.Targets);
                if (fileItems.Sources.Any()) {
                    sources.SetSelected(0, fileItems.Sources.First().Checked);
                }
                FilterTargetList();

                _currentConfigFile = fileDialog.FileName;
                RefreshFormTitle();
            }
        }

        private void SaveSettings()
        {
            if (string.IsNullOrWhiteSpace(_currentConfigFile))
            {
                var fileDialog = new OpenFileDialog
                {
                    CheckFileExists = false,
                    CheckPathExists = true,
                    Filter = "|*.xml"
                };
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var filename = fileDialog.FileName;
                    SerializeToFile(filename);
                    _currentConfigFile = filename;
                    RefreshFormTitle();
                } else
                {
                    return;
                }
            }
            else
            {
                SerializeToFile(_currentConfigFile);
            }
            MessageBox.Show("Settings saved to " + _currentConfigFile);
        }

        private void SerializeToFile(string filename) {
            var sourceItems = GetFilesFromList(sources);
            var targetItems = _targetList;
            var forSerialization = new FileItemsSerialization
            {
                Sources = sourceItems,
                Targets = targetItems
            };
            XmlSerializer xmlSerializer = new XmlSerializer(forSerialization.GetType());
            using (TextWriter textWriter = new StreamWriter(filename))
            {

                xmlSerializer.Serialize(textWriter, forSerialization);
                textWriter.Close();
            }
        }

        private void RefreshFormTitle()
        {
            this.Text = string.Format("{0}{1}", 
                                        BASE_FORM_CAPTION,
                                        string.IsNullOrWhiteSpace(_currentConfigFile) ? "" : " - " + _currentConfigFile);
        }

        private void SetListItems(CheckedListBox list, List<FileListItem> items)
        {
            foreach(var item in items)
            {
                list.Items.Add(item, item.Checked);
            }
        }

        private void SetListItems(ListBox list, List<FileListItem> items)
        {
            foreach (var item in items)
            {
                list.Items.Add(item);
            }
        }

        private List<FileListItem> GetFilesFromList(CheckedListBox list)
        {
            var items = new List<FileListItem>();
            for (var k = 0; k < list.Items.Count; k++) {
                var fileItem = list.Items[k] as FileListItem;
                if (fileItem != null)
                {
                    fileItem.Checked = list.GetItemChecked(k);
                    items.Add(fileItem);
                }
            }
            return items;
        }
        
        private void DeleteSelectedTarget()
        {
            var selectedSource = sources.SelectedItem as FileListItem;
            if (selectedSource == null)
            {
                return;
            }
            var selectedTarget = targets.SelectedItem as FileListItem;
            if (selectedTarget == null)
            {
                return;
            }

            
            var itemToDelete = _targetList.FirstOrDefault(f => f.InternalFileName == selectedTarget.InternalFileName &&
                                                            f.SourceFile == selectedSource.InternalFileName);
            if (itemToDelete != null)
            {
                _targetList.Remove(itemToDelete);
                FilterTargetList();
            }
        }

        
        private void DeleteSelectedSource()
        {
            var selectedSource = sources.SelectedItem as FileListItem;
            if (selectedSource == null)
            {
                return;
            }

            var sourceItemToDelete = _sourceList.FirstOrDefault(f => f.InternalFileName == selectedSource.InternalFileName);
            if (sourceItemToDelete == null)
            {
                return;
            }

            _targetList.RemoveAll(f => f.SourceFile == selectedSource.InternalFileName);
            FilterTargetList();
            sources.Items.Remove(selectedSource);
            _sourceList.Remove(sourceItemToDelete);
        }
        
        private async void DoFileCopy()
        {
            var fileCopyOptions = new FileCopyOption();

            for (var k = 0; k < sources.Items.Count; k++)
            {
                var itemHasTarget = false;
                if (sources.GetItemChecked(k))
                {
                    var sourceItem = sources.Items[k] as FileListItem;
                    if (sourceItem == null)
                    {
                        continue;
                    }

                    var fileCopyItem = new FileCopyItem
                    {
                        SourceFile = sourceItem.FileName
                    };

                    foreach(var targetItem in _targetList
                                            .Where(t => t.SourceFile == sourceItem.InternalFileName)) {
                        itemHasTarget = true;
                        fileCopyItem.TargetFiles.Add(targetItem.FileName);
                    }
                    
                    if (itemHasTarget)
                    {
                        fileCopyOptions.FileCopyItems.Add(fileCopyItem);
                    }
                }
            }
            if (fileCopyOptions.FileCopyItems.Count == 0)
            {
                MessageBox.Show("No source file selected to copy or selected source files have no targets");
                return;
            }

            var fileCopier = new FileCopier.Lib.FileCopier(fileCopyOptions);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var result = await fileCopier.Start();
            sw.Stop();
            var output = new StringBuilder();
            output.AppendLine(string.Format("File copy completed in {0} ms on {1}", sw.ElapsedMilliseconds, DateTime.Now));
            foreach (var fileCopyResult in result.Results)
            {
                if (fileCopyResult.Succeeded)
                {
                    output.AppendLine(string.Format("SUCCEEDED: {0} copied to {1} [{2}]",
                                fileCopyResult.FileCopyItem.SourceFile,
                                fileCopyResult.TargetFile,
                                fileCopyResult.DidOverwrite ? "Overwritten" : "Created"));
                }
                else {
                    output.AppendLine(string.Format("FAILED: {0} cannot be copied to {1}. Reason: {2}",
                               fileCopyResult.FileCopyItem.SourceFile,
                               fileCopyResult.TargetFile,
                               fileCopyResult.Reason));
                }

            }
            status.Text = output.ToString();
        }

        private void AddSource()
        {
            var newSourceFile = newSource.Text.Trim();
            if (string.IsNullOrWhiteSpace(newSourceFile))
            {
                BrowseSource();
                return;
            }
            if (!File.Exists(newSourceFile))
            {
                MessageBox.Show(string.Format("Source file {0} does not exist.", newSourceFile));
                return;
            }
            var newInternalFilename = newSourceFile.ToLowerInvariant();

            if (_sourceList.Any(f => f.InternalFileName == newInternalFilename))
            {
                MessageBox.Show(string.Format("Source {0} already exists.", newSourceFile));
                return;
            }
            else
            {
                var newFileItem = new FileListItem
                {
                    InternalFileName = newInternalFilename,
                    FileName = newSourceFile,
                    SourceFile = string.Empty,
                    Checked = true
                };
                _sourceList.Add(newFileItem);

                var index = sources.Items.Add(newFileItem, true);
                sources.SetSelected(index, true);
                newSource.Clear();
                newSource.Focus();
            }
        }


        private void AddTarget()
        {
            var selectedSource = sources.SelectedItem as FileListItem;
            if (selectedSource == null)
            {
                MessageBox.Show("Please select a source file first.");
                return;
            }
            else
            {
                var newSourceFile = newTarget.Text.Trim();
                if (string.IsNullOrWhiteSpace(newSourceFile))
                {
                    BrowseTarget();
                    return;
                }
                var newInternalFilename = newSourceFile.ToLowerInvariant();
                if (newInternalFilename == selectedSource.InternalFileName) {
                    MessageBox.Show("Target file cannot be identical to source file");
                    return;
                }

                if (_targetList.Any(f => f.SourceFile == selectedSource.InternalFileName &&
                        f.InternalFileName == newInternalFilename))
                {
                    MessageBox.Show(string.Format("Target {0} already exists for source {1}.",
                                            newSourceFile, selectedSource.FileName));
                    return;
                }
                else
                {
                    var newFileItem = new FileListItem
                    {
                        InternalFileName = newInternalFilename,
                        FileName = newSourceFile,
                        SourceFile = selectedSource.InternalFileName,
                        Checked = true
                    };
                    _targetList.Add(newFileItem);

                    var index = targets.Items.Add(newFileItem);
                    targets.SetSelected(index, true);
                    newTarget.Clear();
                    newTarget.Focus();
                }
            }
        }

        private void FilterTargetList()
        {
            var selectedSource = sources.SelectedItem as FileListItem;
            if (selectedSource == null)
            {
                return;
            }

            targets.Items.Clear();
            foreach (var item in _targetList.Where(f => f.SourceFile == selectedSource.InternalFileName)) {
                targets.Items.Add(item);
            }
        }

       
        private void BrowseSource()
        {
            var fileDialog = new OpenFileDialog {
                 CheckFileExists = true,
                 CheckPathExists = true,
                 Multiselect = false
            };

            if (fileDialog.ShowDialog() == DialogResult.OK) {
                newSource.Text = fileDialog.FileName;
                AddSource();
            }

        }

        private void BrowseTarget()
        {

            var fileDialog = new OpenFileDialog
            {
                CheckPathExists = true,
                CheckFileExists = false,
                Multiselect = false
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                newTarget.Text = fileDialog.FileName;
                AddTarget();
            }
        }

        private FileListItem GetFileItemFromContextMenuItem(object sender)
        {
            var menuItem = sender as MenuItem;
            if (menuItem == null) return null;

            var contextMenu = menuItem.GetContextMenu();
            if (contextMenu == null) return null;

            var sourceList = contextMenu.SourceControl as CheckedListBox ?? contextMenu.SourceControl as ListBox;
            if (sourceList != null)
            {
                return sourceList.SelectedItem as FileListItem;
            }
            return null;
        }

        private void DeleteItemFromContextMenu(object sender)
        {
            var filelistItem = GetFileItemFromContextMenuItem(sender);
            if (filelistItem != null)
            {
                if (!string.IsNullOrWhiteSpace(filelistItem.SourceFile))
                {
                    DeleteSelectedTarget();
                }
                else
                {
                    DeleteSelectedSource();
                }
            }
        }

        private void CopySelectedFileItemTextToClipboard(object sender)
        {
            var filelistItem = GetFileItemFromContextMenuItem(sender);
            if (filelistItem != null)
            {
                Clipboard.SetText(filelistItem.FileName);
            }
        }

        private void EvaluateContextMenuItemDisplay(object sender)
        {
            _deleteMenuItem.Visible = false;
            _copyTextMenuItem.Visible = false;

            var contextMenu = sender as ContextMenu;
            if (contextMenu == null) return;

            var sourceList = contextMenu.SourceControl as CheckedListBox ?? contextMenu.SourceControl as ListBox;
            if (sourceList != null)
            {
                var selectedItem = sourceList.SelectedItem as FileListItem;
                if (selectedItem != null)
                {
                    _deleteMenuItem.Visible = true;
                    _copyTextMenuItem.Visible = true;
                }
            }
        }

        public bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void CreateContextMenus()
        {
            _contextMenu = new ContextMenu();
            _contextMenu.Popup += ContextMenu_Popup;

            _copyTextMenuItem = new MenuItem("Copy");
            _copyTextMenuItem.Click += copyTextMenuItem_Click;

            _deleteMenuItem = new MenuItem("Delete");
            _deleteMenuItem.Click += deleteMenuItem_Click;

            _contextMenu.MenuItems.Add(_copyTextMenuItem);
            _contextMenu.MenuItems.Add(_deleteMenuItem);

            sources.ContextMenu = _contextMenu;
            targets.ContextMenu = _contextMenu;
        }

    }
}
