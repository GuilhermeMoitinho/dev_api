using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUnityOfWork _unityOfWork;

        public ProdutoService(IProdutoRepository produtoRepository,
                              INotificador notificador,
                              IUnityOfWork unityOfWork)
                            : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Adicionar(produto);

            var commit = await _unityOfWork.Commit();

            if (!commit)
            {
                Notificar("Ocorreu um erro ao salvar o item");
            }
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            await _produtoRepository.Atualizar(produto);

            var commit = await _unityOfWork.Commit();

            if (!commit)
            {
                Notificar("Ocorreu um erro ao atualizar o item");
            }
        }


        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);

            var commit = await _unityOfWork.Commit();

            if (!commit)
            {
                Notificar("Ocorreu um erro ao tentar remover o item");
            }
        }
        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
