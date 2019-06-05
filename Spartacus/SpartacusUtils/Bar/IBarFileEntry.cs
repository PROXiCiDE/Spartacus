namespace SpartacusUtils.Bar
{
    public interface IBarFileEntry
    {
        bool IsLocalFile { get; set; }
        string FileName { get; set; }
        string FullName { get; set; }
        long FileSize { get; set; }
        long FileSize2 { get; set; }
        long Offset { get; set; }
        BarFileLastWriteTime LastWriteTime { get; set; }

    }
}