using ProjectCeleste.GameFiles.XMLParser;

namespace Spartacus.Common.MessageQueue
{
    public class CharacterMessageQueue
    {
        public CharacterMessageQueue(string filename, AiCharacterXml aiCharacterXml)
        {
            Filename = filename;
            AiCharacterXml = aiCharacterXml;
        }

        public string Filename { get; }
        public AiCharacterXml AiCharacterXml { get; }
    }
}