using System.Collections.Generic;

namespace FileCopier.Lib {
    public class FileCopyOption
    {
        public List<FileCopyItem> FileCopyItems { get; set; }
        public FileCopyOption()
        {
            FileCopyItems = new List<FileCopyItem>();
        }
    }
}
