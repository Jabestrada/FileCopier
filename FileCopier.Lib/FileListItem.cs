using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopier.Lib
{
    public class FileListItem
    {
        public string InternalFilename { get; set; }
        public string Filename { get; set; }
        public string SourceFile { get; set; }
        public bool Checked { get; set; }

        public override string ToString()
        {
            return Filename;
        }
    }
}
