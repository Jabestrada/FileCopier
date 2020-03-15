namespace FileCopier.Lib {
    public class FileListItem
    {
        public string InternalFilename { get; set; }
        public string FileName { get; set; }
        public string SourceFile { get; set; }
        public bool Checked { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
