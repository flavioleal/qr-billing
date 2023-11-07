using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QR.Billings.Api.Controllers.Base;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.IO.Billing;

namespace QR.Billings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : BaseApiController
    {
        private readonly IBillingService _billingService;
        public BillingController(INotifier notifier, IBillingService billingService) : base(notifier)
        {
            _billingService = billingService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddBillingInput input)
        {
            var result = await _billingService.AddAsync(input);

            return CustomResponse(result);
        }
    }
}
