using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.Controllers
{
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService, 
                                  IProdutoRepository produtoRepository, 
                                  IMapper mapper)
        {
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterProdutosFornecedores();

            return Ok(_mapper.Map<IEnumerable<ProdutoViewModel>>(produtos));
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto is null) return NotFound();

            return Ok(_mapper.Map<ProdutoViewModel>(produto));
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var produto = _mapper.Map<Produto>(produtoViewModel);

            await _produtoService.Adicionar(produto);

            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, new { Sucesso = true});
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Atualizar(Guid id, ProdutoViewModel produtoViewModel)
        {
            if(id != produtoViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var produtoAtualizacao = await _produtoRepository.ObterPorId(id);

            produtoAtualizacao.Update(produtoViewModel.Nome,
                                      produtoViewModel.Descricao,
                                      produtoViewModel.Valor,
                                      produtoViewModel.Ativo);


            await _produtoService.Atualizar(produtoAtualizacao);

            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            if (produto is null) return NotFound();

            await _produtoService.Remover(produto.Id);

            return NoContent();
        }
    }
}
