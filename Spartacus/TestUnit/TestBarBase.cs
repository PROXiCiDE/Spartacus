using System.IO;

namespace TestUnit
{
    public class TestBarBase
    {
        /// <summary>
        /// Default is local path hard-coded, override if necessary
        /// </summary>
        public string GamePath { get; set; } = @"G:\MS Age Of Empires Online";
        public string DataBar { get; set; }
        public string ArtUIBar { get; set; }

        public TestBarBase()
        {
            SyncFields();
        }

        public TestBarBase(string path)
        {
            if (string.IsNullOrEmpty(path))
                GamePath = path;
            SyncFields();
        }

        private void SyncFields()
        {
            DataBar = Path.Combine(GamePath, @"Data\Data.bar");
            ArtUIBar = Path.Combine(GamePath, @"Art\ArtUI.bar");
        }
    }
}