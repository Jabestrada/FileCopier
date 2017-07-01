namespace FileCopier
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.newSource = new System.Windows.Forms.TextBox();
            this.addSource = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.newTarget = new System.Windows.Forms.TextBox();
            this.addTarget = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sources = new System.Windows.Forms.CheckedListBox();
            this.copy = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.TextBox();
            this.browseSource = new System.Windows.Forms.Button();
            this.browseTarget = new System.Windows.Forms.Button();
            this.saveSettings = new System.Windows.Forms.Button();
            this.loadSettings = new System.Windows.Forms.Button();
            this.targets = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // newSource
            // 
            this.newSource.Location = new System.Drawing.Point(15, 41);
            this.newSource.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.newSource.Name = "newSource";
            this.newSource.Size = new System.Drawing.Size(624, 20);
            this.newSource.TabIndex = 1;
            // 
            // addSource
            // 
            this.addSource.Location = new System.Drawing.Point(684, 39);
            this.addSource.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.addSource.Name = "addSource";
            this.addSource.Size = new System.Drawing.Size(63, 20);
            this.addSource.TabIndex = 2;
            this.addSource.Text = "Add";
            this.addSource.UseVisualStyleBackColor = true;
            this.addSource.Click += new System.EventHandler(this.addSource_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Source File";
            // 
            // newTarget
            // 
            this.newTarget.Location = new System.Drawing.Point(15, 191);
            this.newTarget.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.newTarget.Name = "newTarget";
            this.newTarget.Size = new System.Drawing.Size(624, 20);
            this.newTarget.TabIndex = 1;
            // 
            // addTarget
            // 
            this.addTarget.Location = new System.Drawing.Point(684, 189);
            this.addTarget.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.addTarget.Name = "addTarget";
            this.addTarget.Size = new System.Drawing.Size(63, 20);
            this.addTarget.TabIndex = 2;
            this.addTarget.Text = "Add";
            this.addTarget.UseVisualStyleBackColor = true;
            this.addTarget.Click += new System.EventHandler(this.addTarget_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 172);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Target File";
            // 
            // sources
            // 
            this.sources.FormattingEnabled = true;
            this.sources.HorizontalScrollbar = true;
            this.sources.Location = new System.Drawing.Point(15, 70);
            this.sources.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.sources.Name = "sources";
            this.sources.Size = new System.Drawing.Size(734, 79);
            this.sources.TabIndex = 6;
            this.sources.SelectedIndexChanged += new System.EventHandler(this.sources_SelectedIndexChanged);
            this.sources.KeyUp += new System.Windows.Forms.KeyEventHandler(this.sources_KeyUp);
            // 
            // copy
            // 
            this.copy.Location = new System.Drawing.Point(605, 306);
            this.copy.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.copy.Name = "copy";
            this.copy.Size = new System.Drawing.Size(141, 27);
            this.copy.TabIndex = 8;
            this.copy.Text = "Copy";
            this.copy.UseVisualStyleBackColor = true;
            this.copy.Click += new System.EventHandler(this.copy_Click);
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(15, 355);
            this.status.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.status.Multiline = true;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.status.Size = new System.Drawing.Size(734, 114);
            this.status.TabIndex = 9;
            // 
            // browseSource
            // 
            this.browseSource.Location = new System.Drawing.Point(642, 39);
            this.browseSource.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.browseSource.Name = "browseSource";
            this.browseSource.Size = new System.Drawing.Size(28, 20);
            this.browseSource.TabIndex = 10;
            this.browseSource.Text = "...";
            this.browseSource.UseVisualStyleBackColor = true;
            this.browseSource.Click += new System.EventHandler(this.browseSource_Click);
            // 
            // browseTarget
            // 
            this.browseTarget.Location = new System.Drawing.Point(642, 189);
            this.browseTarget.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.browseTarget.Name = "browseTarget";
            this.browseTarget.Size = new System.Drawing.Size(28, 20);
            this.browseTarget.TabIndex = 11;
            this.browseTarget.Text = "...";
            this.browseTarget.UseVisualStyleBackColor = true;
            this.browseTarget.Click += new System.EventHandler(this.browseTarget_Click);
            // 
            // saveSettings
            // 
            this.saveSettings.Location = new System.Drawing.Point(453, 306);
            this.saveSettings.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.saveSettings.Name = "saveSettings";
            this.saveSettings.Size = new System.Drawing.Size(141, 27);
            this.saveSettings.TabIndex = 12;
            this.saveSettings.Text = "Save Settings";
            this.saveSettings.UseVisualStyleBackColor = true;
            this.saveSettings.Click += new System.EventHandler(this.saveSettings_Click);
            // 
            // loadSettings
            // 
            this.loadSettings.Location = new System.Drawing.Point(295, 306);
            this.loadSettings.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.loadSettings.Name = "loadSettings";
            this.loadSettings.Size = new System.Drawing.Size(141, 27);
            this.loadSettings.TabIndex = 13;
            this.loadSettings.Text = "Load Settings ...";
            this.loadSettings.UseVisualStyleBackColor = true;
            this.loadSettings.Click += new System.EventHandler(this.loadSettings_Click);
            // 
            // targets
            // 
            this.targets.FormattingEnabled = true;
            this.targets.Location = new System.Drawing.Point(17, 219);
            this.targets.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.targets.Name = "targets";
            this.targets.Size = new System.Drawing.Size(732, 82);
            this.targets.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 489);
            this.Controls.Add(this.targets);
            this.Controls.Add(this.loadSettings);
            this.Controls.Add(this.saveSettings);
            this.Controls.Add(this.browseTarget);
            this.Controls.Add(this.browseSource);
            this.Controls.Add(this.status);
            this.Controls.Add(this.copy);
            this.Controls.Add(this.sources);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addTarget);
            this.Controls.Add(this.newTarget);
            this.Controls.Add(this.addSource);
            this.Controls.Add(this.newSource);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Copier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox newSource;
        private System.Windows.Forms.Button addSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newTarget;
        private System.Windows.Forms.Button addTarget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox sources;
        private System.Windows.Forms.Button copy;
        private System.Windows.Forms.TextBox status;
        private System.Windows.Forms.Button browseSource;
        private System.Windows.Forms.Button browseTarget;
        private System.Windows.Forms.Button saveSettings;
        private System.Windows.Forms.Button loadSettings;
        private System.Windows.Forms.ListBox targets;
    }
}

