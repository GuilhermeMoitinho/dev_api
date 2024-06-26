using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DevIO.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected bool OperacaoValida()
        {
            return true;
        }

        protected IActionResult CustomResponse(Object result = null)
        {
            if (!OperacaoValida())
            {
                return BadRequest();
            }

            return Ok(result);  
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return CustomResponse();
        }

        protected void NotificarErro(string mensagem)
        {

        }
    }
}
