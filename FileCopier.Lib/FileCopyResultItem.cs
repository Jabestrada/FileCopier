namespace FileCopier.Lib {
    public class FileCopyResultItem
    {
        public FileCopyItem FileCopyItem { get; set; }
        public string TargetFile { get; set; }
        public bool Succeeded{ get; set; }
        public bool DidOverwrite { get; set; }
        public string Reason { get; set; }
    }
}
