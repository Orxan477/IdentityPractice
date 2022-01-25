using FluentValidation;
using LoginRegisterPractice.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginRegisterPractice.Validators.AccountValidator
{
    public class RegisterValidator:AbstractValidator<RegisterVM>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName).NotEmpty()
                                    .NotNull()
                                    .MaximumLength(50);
            RuleFor(x => x.Username).NotEmpty()
                                    .NotNull()
                                    .MaximumLength(50);
            RuleFor(x => x.Email).NotNull()
                                 .NotEmpty()
                                 .EmailAddress();
            RuleFor(x => x.Password).NotEmpty()
                                    .NotNull();
            RuleFor(x => x.ConfirmPassword).NotNull()
                                           .NotEmpty()
                                           .Equal(x=>x.Password);
        }
    }
}
