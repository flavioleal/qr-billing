namespace QR.Billings.Business.IO.BillingExternal
{
    public class CreateBillingExternalInput
    {
        public CreateBillingExternalInput(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; set; }
    }
}
