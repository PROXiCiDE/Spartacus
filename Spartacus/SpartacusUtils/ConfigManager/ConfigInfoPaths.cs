using System.IO;

namespace SpartacusUtils.ConfigManager
{
    public  class ConfigInfoPaths
    {
        public ConfigInfoPaths(string gameInstallPath)
        {
            GameInstallPath = gameInstallPath;

            Data = $"{Path.Combine(GameInstallPath, "Data")}";
            Art  = $"{Path.Combine(GameInstallPath, "Art")}";
            Ai  = $"{Path.Combine(GameInstallPath, "Ai")}";
            Rm  = $"{Path.Combine(GameInstallPath, "Rm")}";
            Fonts  = $"{Path.Combine(GameInstallPath, "Fonts")}";
            Render  = $"{Path.Combine(GameInstallPath, "Render")}";
            Scenario = $"{Path.Combine(GameInstallPath, "Scenario")}";
            Sound  = $"{Path.Combine(GameInstallPath, "Sound")}";
            SoundAmbient  = $"{Path.Combine(Sound, "Amb")}";
            SoundSets  = $"{Path.Combine(Sound, "SoundSets")}";
            SoundMusic  = $"{Path.Combine(Sound, "Music")}";
        }

        public  string GameInstallPath { get; set; }
        public  string Data { get; set; } 
        public  string Art { get; set; }
        public  string Ai { get; set; } 
        public  string Rm { get; set; } 
        public  string Fonts { get; set; } 
        public  string Render { get; set; } 
        public  string Scenario { get; set; } 
        public  string Sound { get; set; } 
        public  string SoundAmbient { get; set; } 
        public  string SoundSets { get; set; }
        public  string SoundMusic { get; set; } 
    }
}