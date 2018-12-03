using MessageService.Enums;
using System.Runtime.Serialization;

namespace MessageService.Models
{

    [DataContract]
    public class Contact
    {
        public ContactType ContactType { get; set; }

        public string Value { get; set; }
    }
}