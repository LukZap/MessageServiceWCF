using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Models
{
    [DataContract]
    public class MessageRequest
    {
        public string Subject { get; set; }

        public string Message { get; set; }

        public Recipient Recipient { get; set; }
    }
}
