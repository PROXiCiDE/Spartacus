using System.IO;

namespace TestUnit.Bar
{
    public class TestBarBase
    {
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

        /// <summary>
        ///     Default is local path hard-coded, override if necessary
        /// </summary>
        public string GamePath { get; set; } = @"G:\MS Age Of Empires Online";

        public string DataBar { get; set; }
        public string ArtUIBar { get; set; }

        private void SyncFields()
        {
            DataBar = Path.Combine(GamePath, @"Data\Data.bar");
            ArtUIBar = Path.Combine(GamePath, @"Art\ArtUI.bar");
        }
    }
}