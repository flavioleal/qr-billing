using FluentValidation;
using QR.Billings.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR.Billings.Business.IO.Billing
{
    public class AddBillingInput
    {
        public decimal Value { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }

    public class AddBillingValidation : AbstractValidator<AddBillingInput>
    {
        public AddBillingValidation()
        {
            RuleFor(f => f.Value)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(p => p.CustomerName)
               .NotNull()
               .NotEmpty()
               .WithMessage(string.Format("O campo {0} precisa ser fornecido", "Nome"));

            RuleFor(p => p.CustomerEmail)
              .NotNull().WithMessage("O campo Nome precisa ser fornecido")
              .NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
              .EmailAddress().WithMessage("O campo Nome precisa ser um endereço de email válido");
        }
    }
}
