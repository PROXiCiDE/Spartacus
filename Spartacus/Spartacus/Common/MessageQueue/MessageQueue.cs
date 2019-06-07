namespace Spartacus.Common.MessageQueue
{
    public class MessageQueue
    {
        public MessageQueue(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}