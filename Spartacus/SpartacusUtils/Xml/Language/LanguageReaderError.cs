namespace SpartacusUtils.Xml.Language
{
    public class LanguageReaderError
    {
        public LanguageReaderError(string fileName, string id, string exception)
        {
            FileName = fileName;
            Id = id;
            Exception = exception;
        }

        public string Id { get; set; }
        public string FileName { get; set; }
        public string Exception { get; set; }
    }
}