using QR.Billings.Business.Enums;

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
        
    }

    public class Merchant
    {
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

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
