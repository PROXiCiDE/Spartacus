namespace Spartacus.ViewModels.AiCharacter
{
    public class CharacterMessageQueue
    {
        public string Filename { get; }

        public CharacterMessageQueue(string filename)
        {
            Filename = filename;
        }
    }
}