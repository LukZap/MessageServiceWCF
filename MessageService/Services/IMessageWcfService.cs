using MessageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MessageService
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IService1” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IMessageWcfService
    {
        [OperationContract]
        MessageResponse Send(MessageRequest message);
        SmtpSettings SmtpSettings { get; set; }
    } 
}
