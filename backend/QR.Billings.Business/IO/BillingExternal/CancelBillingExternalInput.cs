namespace QR.Billings.Business.IO.BillingExternal
{
    public class CancelBillingExternalInput
    {
        public CancelBillingExternalInput(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
