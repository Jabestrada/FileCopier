using System.Collections.Generic;

namespace FileCopier.Lib {
    public class FileCopyResult
    {
        public List<FileCopyResultItem> Results { get; set; }

        public FileCopyResult()
        {
            Results = new List<FileCopyResultItem>();
        }
    }
}
