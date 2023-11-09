using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Services.Notifier;
using QR.Billings.Business.IO.Api;

namespace QR.Billings.Api.Controllers.Base
{
    public abstract class BaseApiController : ControllerBase
    {
        private readonly INotifier _notifier;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="notifier"></param>
        protected BaseApiController(INotifier notifier)
        {
            _notifier = notifier;
        }

        /// <summary>
        /// OperationValid
        /// </summary>
        /// <returns></returns>
        protected bool OperationValid()
        {
            return !_notifier.HasNotification();
        }

        /// <summary>
        /// CustomResponse
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse<T>(T result)
        {
            if (OperationValid())
            {
                return Ok(new ApiOutput<T>(result));
            }
            return BadRequest(new ApiOutput<T>(result, _notifier.GetNotifications().Select(n => n.Message).ToList()));
        }

        /// <summary>
        /// CustomResponse
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotifyInvalidModelError(modelState);

            return CustomResponse(null);
        }

        /// <summary>
        /// NotifyInvalidModelError
        /// </summary>
        /// <param name="modelState" />
        protected void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        /// <summary>
        /// NotifyError
        /// </summary>
        /// <param name="mensagem"></param>
        protected void NotifyError(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }
    }
}
