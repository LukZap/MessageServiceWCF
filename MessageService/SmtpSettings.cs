using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MessageService
{
    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public NetworkCredential Credentials { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }

        public SmtpSettings()
        {
            //default config
            Host = "smtp.gmail.com";
            Port = int.Parse("587");
            EnableSsl = true;
            UseDefaultCredentials = false;
            Credentials = new NetworkCredential("lukasz.zaparucha@gmail.com", "pass");
            DeliveryMethod = SmtpDeliveryMethod.Network;
        }
    }
}
