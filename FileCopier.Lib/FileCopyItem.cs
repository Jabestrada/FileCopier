using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier.Lib
{
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
