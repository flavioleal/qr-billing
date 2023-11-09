using QR.Billings.Business.IO.BillingExternal;

namespace QR.Billings.Business.Interfaces.ExternalServices
{
    public  interface IBillingExternalService
    {
        Task<BillingExternalCreateOutput> Create(decimal value);
        Task<BillingExternalCancelOutput> Cancel(string idTransaction);
    }
}
