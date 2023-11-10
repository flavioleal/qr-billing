namespace QR.Billings.Business.IO.BillingExternal
{
    public class CreateBillingExternalOutput
    {
        public string Message { get; set; }
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Status { get; set; }
        public string QrCode { get; set; }
    }
}
