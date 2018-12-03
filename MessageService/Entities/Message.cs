using MessageService.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageService.Entities
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MessageId { get; set; }
        public string EmailAddress { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public ReturnCode ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
