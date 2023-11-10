using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QR.Billings.Api.Controllers.Base;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Services;
using QR.Billings.Business.IO.Billing;

namespace QR.Billings.Api.Controllers
{
    /// <summary>
    /// Controller for managing billing operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BillingController : BaseApiController
    {
        private readonly IBillingService _billingService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="notifier">The notifier.</param>
        /// <param name="billingService">The billing service.</param>
        public BillingController(INotifier notifier, IBillingService billingService) : base(notifier)
        {
            _billingService = billingService;
        }

        /// <summary>
        /// Get a list of billings based on the provided filter.
        /// </summary>
        /// <param name="input">The billing filter input.</param>
        /// <returns>A list of billings or NoContent if no billings are found.</returns>
        /// <response code="200">A list of billings</response>
        /// <response code="204">if no billings are found.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromQuery] BillingFilterInput input)
        {
            var result = await _billingService.GetPagedListByFilterAsync(input);
            return result != null && result.List.Count() > 0 ? CustomResponse(result) : NoContent();
        }

        /// <summary>
        /// Add a new billing entry.
        /// </summary>
        /// <param name="input">The input for adding a new billing.</param>
        /// <returns>The result of the operation.</returns>
        /// <response code="200">Billing add</response>
        /// <response code="400">Billing not add</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "lojista")]
        public async Task<IActionResult> Post([FromBody] AddBillingInput input)
        {
            var result = await _billingService.AddAsync(input);

            return CustomResponse(result);
        }

        /// <summary>
        /// Cancel a billing entry by ID.
        /// </summary>
        /// <param name="id">The ID of the billing entry to cancel.</param>
        /// <param name="input">The input for canceling the billing.</param>
        /// <returns>The result of the operation.</returns>
        /// <response code="200">Billing cancel</response>
        /// <response code="400">Billing not cancel</response>
        [HttpPut("cancel/{id}")]
        [Authorize(Roles = "admin")]
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
