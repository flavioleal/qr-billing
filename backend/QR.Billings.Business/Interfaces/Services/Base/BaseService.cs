using FluentValidation;
using FluentValidation.Results;
using QR.Billings.Business.Interfaces.Notifier;
using QR.Billings.Business.Interfaces.Services.Notifier;

namespace QR.Billings.Business.Interfaces.Services.Base
{
    public abstract class BaseService
    {
        private readonly INotifier _notifier;

        protected BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }

        protected void Notify(string mensagem)
        {
            _notifier.Handle(new Notification(mensagem));
        }

        protected bool ExecuteValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

        protected bool IsValid()
        {
            return _notifier.HasNotification();
        }
    }
}