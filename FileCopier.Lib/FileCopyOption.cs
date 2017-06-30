using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier.Lib
{
    public class FileCopyOption
    {
        public List<FileCopyItem> FileCopyItems { get; set; }
        public FileCopyOption()
        {
            FileCopyItems = new List<FileCopyItem>();
        }
    }
}
