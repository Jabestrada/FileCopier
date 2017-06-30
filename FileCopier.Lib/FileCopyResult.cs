using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier.Lib
{
    public class FileCopyResult
    {
        public List<FileCopyResultItem> Results { get; set; }

        public FileCopyResult()
        {
            Results = new List<FileCopyResultItem>();
        }
    }
}
