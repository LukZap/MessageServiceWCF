using MessageService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Models
{
    [DataContract]
    public class MessageResponse
    {
        public ReturnCode ReturnCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
