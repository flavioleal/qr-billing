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

        // GET: api/<BillingController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] BillingFilterInput input)
        {
            var result = await _billingService.GetPagedListByFilterAsync(input);
            return result != null && result.List.Count() > 0 ? CustomResponse(result) : NoContent();
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _billingService.GetAll();
            return result != null && result.Count() > 0 ? CustomResponse(result) : NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AddBillingInput input)
        {
            var result = await _billingService.AddAsync(input);

            return CustomResponse(result);
        }

        // PUT api/<BillingController>/5
        [HttpPut("cancel/{id}")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelBillingByIdAsync(Guid id, [FromBody] CancelBillingInput input)
        {
            if (id != input.Id)
            {
                return BadRequest();
            }

            var result = await _billingService.CancelBillingByIdAsync(input.Id);

            return CustomResponse(result);
        }
    }
}
