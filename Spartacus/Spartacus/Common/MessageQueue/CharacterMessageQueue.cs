using ProjectCeleste.GameFiles.XMLParser;

namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterMessageQueue
    {
        public string Filename { get; }
        public AiCharacterXml AiCharacterXml { get; }

        public CharacterMessageQueue(string filename, AiCharacterXml aiCharacterXml)
        {
            Filename = filename;
            AiCharacterXml = aiCharacterXml;
        }
    }
}