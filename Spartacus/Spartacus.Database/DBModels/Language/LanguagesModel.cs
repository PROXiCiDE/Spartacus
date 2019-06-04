namespace Spartacus.Database.DBModels.Language
{
    public class LanguagesModel
    {
        public int Id { get; set; }
        public string Locale { get; set; }
        public string Filename { get; set; }
        public int LocId { get; set; }
        public string Symbol { get; set; }
        public string Text { get; set; }
    }
}