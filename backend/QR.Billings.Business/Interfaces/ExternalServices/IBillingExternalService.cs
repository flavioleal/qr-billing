using QR.Billings.Business.IO.BillingExternal;

namespace QR.Billings.Business.Interfaces.ExternalServices
{
    public  interface IBillingExternalService
    {
        Task<CreateBillingExternalOutput> Create(decimal value);
        Task<CancelBillingExternalOutput> Cancel(string idTransaction);
    }
}
