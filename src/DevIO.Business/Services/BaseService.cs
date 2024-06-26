using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace DevIO.Business.Services
{
    public abstract class BaseService
    {
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            if (validationResult.Errors.Any())
            {
                foreach (var error in validationResult.Errors)
                {
                    Notificar(error.ErrorMessage);
                }
            }
        }

        protected void Notificar(string Mensagem)
        {
            _notificador.Add(new Notificacao(Mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) 
            where TV : AbstractValidator<TE>
            where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (!validator.IsValid)
            {
                var erros = validator.Errors.Select(x => x.ErrorMessage);
                foreach(var erro in erros)
                {
                    Notificar(erro);
                }
                return false;
            }

            return true;
        }
    }
}
