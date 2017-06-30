using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier.Lib
{
    public class FileCopier
    {
        private FileCopyOption _options;

        public FileCopier(FileCopyOption options)
        {
            _options = options;
        }

        public async Task<FileCopyResult> Start() {
            return await Task.Run(() => {
                var result = new FileCopyResult();
                foreach (var fileCopyItem in _options.FileCopyItems) {
                    foreach (var target in fileCopyItem.TargetFiles) {
                        if (target.ToLowerInvariant() == fileCopyItem.SourceFile.ToLowerInvariant())
                        {
                            result.Results.Add(new FileCopyResultItem
                            {
                                FileCopyItem = fileCopyItem,
                                Succeeded = false,
                                Reason = "Source and target are identical"
                            });
                            continue;
                        }
                        var targetFileExists = File.Exists(target);
                        try
                        {
                            File.Copy(fileCopyItem.SourceFile, target, true);
                            result.Results.Add(new FileCopyResultItem {
                                FileCopyItem = fileCopyItem,
                                TargetFile = target,
                                Succeeded = true,
                                DidOverwrite = targetFileExists
                            });
                            }
                            catch (Exception exc) {
                                result.Results.Add(new FileCopyResultItem
                                {
                                    FileCopyItem = fileCopyItem,
                                    TargetFile = target,
                                    Succeeded = false,
                                    Reason = exc.Message,
                                    DidOverwrite = targetFileExists
                                });
                            }
                        }
                }
                return result;
            });
        }
    }
}
