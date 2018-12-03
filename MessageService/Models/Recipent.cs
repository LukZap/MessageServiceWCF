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
    public class Recipient
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public LegalForm LegalForm { get; set; }

        public Contact[] Contacts { get; set; }
    }
}
