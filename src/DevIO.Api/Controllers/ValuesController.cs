using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ValuesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult PegarFornecedores()
        {
            return Ok(_context.Fornecedores.ToList());  
        }
    }
}
