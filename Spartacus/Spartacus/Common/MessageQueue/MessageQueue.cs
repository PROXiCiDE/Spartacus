using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Common.MessageQueue
{
    public class MessageQueue
    {
        public string Message { get; set; }

        public MessageQueue(string message)
        {
            Message = message;
        }
    }
}
