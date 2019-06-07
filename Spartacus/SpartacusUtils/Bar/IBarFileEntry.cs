namespace SpartacusUtils.Bar
{
    public interface IBarFileEntry
    {
        bool IsArchivedFile { get; set; }
        string FileName { get; set; }
        bool IsPhysicalFile { get; set; }
        long Offset { get; set; }
        long FileSize2 { get; set; }
    }
}