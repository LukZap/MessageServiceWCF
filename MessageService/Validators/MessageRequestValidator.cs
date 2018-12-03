using FluentValidation;
using FluentValidation.Validators;
using MessageService.Enums;
using MessageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MessageService.Validators
{
    public class MessageRequestValidator : AbstractValidator<MessageRequest>
    {
        public MessageRequestValidator()
        {
            When(x => x.Recipient.LegalForm == LegalForm.Person, () => {
                RuleFor(x => x.Recipient.FirstName)
                    .NotEmpty()
                    .WithMessage("FirstName is required");

                RuleFor(x => x.Recipient.LastName)
                    .NotEmpty()
                    .WithMessage("LastName is required");

                RuleFor(x => x.Recipient.Contacts)
                    .Must(x => x.All(c => c.ContactType == ContactType.Email && isValidEmail(c.Value)))
                    .WithMessage("One contact's email address is not valid");
            });

            When(x => x.Recipient.LegalForm == LegalForm.Company, () => {
                RuleFor(x => x.Recipient.LastName)
                    .NotEmpty()
                    .WithMessage("LastName is required");

                RuleFor(x => x.Recipient.Contacts)
                    .Must(x => x.All(c => c.ContactType == ContactType.OfficeEmail && isValidEmail(c.Value)))
                    .WithMessage("One contact's email address is not valid");
                
            });
        }

        private bool isValidEmail(string email)
        {
            return Regex.IsMatch(email, @"[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*");
        }
    }
}
