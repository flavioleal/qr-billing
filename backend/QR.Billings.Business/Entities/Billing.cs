using QR.Billings.Business.BusinessObjects;
using QR.Billings.Business.Enums;
using QR.Billings.Business.Interfaces.CurrentUser;
using QR.Billings.Business.IO.Billing;

namespace QR.Billings.Business.Entities
{
    public class Billing
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? TransactionId { get; set; }
        public decimal Value { get; set; }
        public string? QrCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public Guid? CancellationUser { get; set; }
        public DateTime? CancellationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Merchant Merchant { get; set; }
        public Customer Customer { get; set; }
        public bool? TransactionCanceled { get; set; }

        public Billing(decimal value)
        {
            Value = value;

            Validate();
        }

        internal void Validate()
        {
            if(Value == 0)
            {
                throw new BusinessException("O Valor não pode ser 0");
            }
        }
        internal void Cancel(Guid currentUserId)
        {
            Status = PaymentStatusEnum.Canceled;
            CancellationDate = DateTime.Now;
            CancellationUser = currentUserId;
        }

        internal void PrepareDataForAddition(AddBillingInput input, ICurrentUser _currentUser)
        {
            Status = PaymentStatusEnum.Pending;
            Customer = new Customer(input.CustomerName, input.CustomerEmail);
            Merchant = new Merchant(_currentUser.Id.Value, _currentUser.Name);
            CreatedAt = DateTime.Now;
        }
    }

    public class Merchant
    {
        public Merchant(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
    public class Customer
    {
        public Customer(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public Customer(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
