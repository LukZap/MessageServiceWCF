using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MessageService.Entities;
using MessageService.Enums;
using MessageService.Models;
using MessageService.Repositories;
using MessageService.Validators;

namespace MessageService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MessageWcfService : IMessageWcfService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly MessageRequestValidator _messageValidator;

        public SmtpSettings SmtpSettings { get; set; }

        public MessageWcfService(IMessageRepository messageRepository,
            MessageRequestValidator messageValidator)
        {
            _messageRepository = messageRepository;
            _messageValidator = messageValidator;
        }

        public MessageResponse Send(MessageRequest messageRequest)
        {
            var validationResult = _messageValidator.Validate(messageRequest);
            if (validationResult.IsValid)
            {
                var client = setSmtpClient();
                
                var messageToSent = setMailMessage(messageRequest);
                try
                {
                    client.Send(messageToSent);

                    foreach (var contact in messageRequest.Recipient.Contacts)
                    {
                        _messageRepository.Add(new Message
                        {
                            Content = messageRequest.Message,
                            EmailAddress = contact.Value,
                            ErrorCode = ReturnCode.Success,
                            SentDate = DateTime.Now
                        });
                    }

                    return new MessageResponse
                    {
                        ReturnCode = ReturnCode.Success
                    };
                }
                catch (Exception ex)
                {
                    foreach (var contact in messageRequest.Recipient.Contacts)
                    {
                        _messageRepository.Add(new Message
                        {
                            Content = messageRequest.Message,
                            EmailAddress = contact.Value,
                            ErrorCode = ReturnCode.InternalError,
                            ErrorMessage = ex.Message,
                            SentDate = DateTime.Now
                        });
                    }

                    return new MessageResponse
                    {
                        ReturnCode = ReturnCode.InternalError,
                        ErrorMessage = ex.Message
                    };
                }             
            }
            else
            {
                return new MessageResponse
                {
                    ReturnCode = ReturnCode.ValidationError,
                    ErrorMessage = validationResult.ToString()
                };
            }
        }

        private MailMessage setMailMessage(MessageRequest messageRequest)
        {
            MailMessage msg = new MailMessage
            {
                Subject = messageRequest.Subject,
                Body = messageRequest.Message,
                From = new MailAddress("lukasz.zaparucha@gmail.com"),
                IsBodyHtml = true
            };

            foreach (var contact in messageRequest.Recipient.Contacts)
            {
                msg.To.Add(contact.Value);
            }
            
            return msg;
        }

        private SmtpClient setSmtpClient()
        {
            if (SmtpSettings == null)
                SmtpSettings = new SmtpSettings();

            var smtpClient = new SmtpClient
            {
                Host = SmtpSettings.Host,
                Port = SmtpSettings.Port,
                EnableSsl = SmtpSettings.EnableSsl,
                UseDefaultCredentials = SmtpSettings.UseDefaultCredentials,
                Credentials = SmtpSettings.Credentials,
                DeliveryMethod = SmtpSettings.DeliveryMethod,
            };

            return smtpClient;
        }
    }
}
