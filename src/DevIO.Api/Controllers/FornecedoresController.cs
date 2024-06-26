using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Services;
using DevIO.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevIO.Api.Controllers
{
    [Route("api/fornecedores")]
    public class FornecedoresController : MainController
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorService fornecedorService, 
                                      IFornecedorRepository fornecedorRepository, 
                                      IMapper mapper,
                                      INotificador notificador)
                                      : base(notificador)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var fornecedors = await _fornecedorRepository.ObterTodos();

            return CustomResponse(HttpStatusCode.OK,
                                 _mapper.Map<IEnumerable<FornecedorViewModel>>(fornecedors));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorId(id);

            if (fornecedor is null) return NotFound();

            return CustomResponse(HttpStatusCode.OK,
                                 _mapper.Map<FornecedorViewModel>(fornecedor));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);

            fornecedor.Id = Guid.NewGuid();

            await _fornecedorService.Adicionar(fornecedor);

            return CustomResponse(HttpStatusCode.Created, fornecedorViewModel);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return BadRequest();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var fornecedorAtualizacao = await _fornecedorRepository.ObterPorId(id);

            fornecedorAtualizacao.Update(fornecedorViewModel.Nome,
                                         fornecedorViewModel.Documento,
                                         fornecedorViewModel.TipoFornecedor,
                                         fornecedorViewModel.Ativo,
                                        _mapper.Map<Endereco>(fornecedorViewModel.Endereco));

            await _fornecedorService.Atualizar(fornecedorAtualizacao);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterPorId(id);

            if (fornecedor is null) return NotFound();

            await _fornecedorService.Remover(fornecedor.Id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
