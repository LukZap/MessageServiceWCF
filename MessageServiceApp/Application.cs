using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using MessageService;


namespace MessageServiceApp
{
    public class Application : IApplication
    {
        private readonly IMessageWcfService _service;
        public Application(IMessageWcfService service)
        {
            _service = service;
        }

        public void Run()
        {
            var smtpSettings = new SmtpSettings();
            _service.SmtpSettings = smtpSettings;

            Uri baseAddress = new Uri("http://localhost:8000/lukzap/");

            ServiceHost selfHost = new ServiceHost(_service, baseAddress);

            try
            {
                selfHost.AddServiceEndpoint(typeof(IMessageWcfService), new WSHttpBinding(), "MessageWcfService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;                
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();
                Console.WriteLine("The service is ready.");

                while (true)
                {
                    Console.WriteLine();

                    Console.WriteLine("Press Q to terminate service.");
                    Console.WriteLine("Press S to change SMTP settings.");

                    var keyInfo = Console.ReadKey();

                    if (keyInfo.Key == ConsoleKey.Q)
                        break;

                    if (keyInfo.Key == ConsoleKey.S)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Credentials Username (current - {smtpSettings.Credentials.UserName}): ");
                        var input_username = Console.ReadLine();
                        if(!string.IsNullOrWhiteSpace(input_username))
                        {
                            smtpSettings.Credentials.UserName = input_username;
                        }

                        Console.WriteLine($"Credentials Password (current - {smtpSettings.Credentials.Password}): ");
                        var input_password = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input_password))
                        {
                            smtpSettings.Credentials.Password = input_password;
                        }

                        Console.WriteLine($"Host (current - {smtpSettings.Host}): ");
                        var input_host = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input_host))
                        {
                            smtpSettings.Host = input_host;
                        }

                        Console.WriteLine($"Port (current - {smtpSettings.Port}): ");
                        var input_port = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input_port))
                        {
                            if (int.TryParse(input_port, out int parsedPort))
                                smtpSettings.Port = parsedPort;
                        }

                        Console.WriteLine($"EnableSsl (current - {smtpSettings.EnableSsl}) - pass 1 for true or 0 for false: ");
                        var input_enableSsl = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input_enableSsl))
                        {
                            if (int.TryParse(input_enableSsl, out int parsedEnableSsl))
                                smtpSettings.EnableSsl = parsedEnableSsl == 0 ? false : true;
                        }

                        Console.WriteLine($"UseDefaultCredentials (current - {smtpSettings.UseDefaultCredentials})  - pass 1 for true or 0 for false: ");
                        var input_useDefaultCredentials = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input_useDefaultCredentials))
                        {
                            if (int.TryParse(input_useDefaultCredentials, out int parsedDefCreds))
                                smtpSettings.UseDefaultCredentials = parsedDefCreds == 0 ? false : true;
                        }

                        Console.WriteLine($"DeliveryMethod (current - {smtpSettings.DeliveryMethod}): 0 for Network, 1 for SpecifiedPickupDirectory, 2 for PickupDirectoryFromIis:");
                        var input_deliveryMethod = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(input_deliveryMethod))
                        {
                            if (int.TryParse(input_deliveryMethod, out int parsedDeliveryMethod))
                            {
                                switch (parsedDeliveryMethod)
                                {
                                    case 0:
                                        smtpSettings.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                                        break;
                                    case 1:
                                        smtpSettings.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory;
                                        break;
                                    case 2:
                                        smtpSettings.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                                        break;
                                }
                            }

                        }

                        // FOR TESTING

                        _service.Send(new MessageService.Models.MessageRequest
                        {
                            Message = "Treść",
                            Recipient = new MessageService.Models.Recipient
                            {
                                FirstName = "Łukasz",
                                LastName = "Zaparucha",
                                LegalForm = MessageService.Enums.LegalForm.Person,
                                Contacts = new MessageService.Models.Contact[]
                            {
                                new MessageService.Models.Contact{
                                    ContactType = MessageService.Enums.ContactType.Email,
                                    Value = "lukasz.zaparucha@gmail.com"
                                },
                                new MessageService.Models.Contact{
                                    ContactType = MessageService.Enums.ContactType.Email,
                                    Value = "lukasz.zaparucha@gmail.com"
                                }
                            }
                            },
                            Subject = "Test"
                        });
                    }                    
                }

                Console.ReadLine();

                // Close the ServiceHostBase to shutdown the service.
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
