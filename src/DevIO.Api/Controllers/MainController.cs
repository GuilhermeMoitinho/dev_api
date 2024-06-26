using DevIO.Business.Interfaces;
using DevIO.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace DevIO.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        protected MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected IActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, 
                                               Object result = null)
        {
            if (!OperacaoValida())
            {
               return BadRequest(new 
               { 
                   errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
               });   
            }

            return new ObjectResult(result)
            {
                StatusCode = Convert.ToInt32(statusCode)
            };  
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid)
            {
                NotificarErroModelInvalida(modelState);
                return BadRequest();
            }

            return CustomResponse();
        }

        protected void  NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(x => x.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            _notificador.Add(new Notificacao(mensagem));
        }
    }
}
