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
        public CustomerInput Customer { get; set; }
    }

    public class AddBillingValidation : AbstractValidator<AddBillingInput>
    {
        public AddBillingValidation()
        {
            RuleFor(f => f.Value)
                .GreaterThan(0)
                .WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}
