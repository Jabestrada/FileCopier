using System.Collections.Generic;

namespace FileCopier.Lib {
    public class FileCopyItem
    {
        public string SourceFile { get; set; }
        public List<string> TargetFiles { get; set; }
        public FileCopyItem()
        {
            TargetFiles = new List<string>();
        }
    }
}
