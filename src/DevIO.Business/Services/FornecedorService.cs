using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IUnityOfWork _unityOfWork;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 INotificador notificador,
                                 IUnityOfWork unityOfWork)
                               : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<bool> Adicionar(Fornecedor fornecedor)
        {

            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) ||
                !ExecutarValidacao(new EnderecoValidation(), fornecedor.Endereco)) 
                return false;

            if(_fornecedorRepository
                .Buscar(f => f.Documento == fornecedor.Documento)
                .Result
                .Any())
            {
                Notificar("Já existe um fornecedor com esse documento informado");
                return false;
            }

            await _fornecedorRepository.Adicionar(fornecedor);

            var commit = await _unityOfWork.Commit();

            if (!commit)
            {
                Notificar("Ocorreu um erro ao salvar o item");
            }
            
            return true;
        }

        public async Task<bool> Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return false;
            await _fornecedorRepository.Atualizar(fornecedor);

            var commit = await _unityOfWork.Commit();

            if (!commit)
            {
                Notificar("Ocorreu um erro ao atualizar o item");
            }

            return true;
        }


        public async Task<bool> Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);
            if(fornecedor is null)
            {
                Notificar("Não existe esse fornecedor");
                return false;
            }

            if (fornecedor.Produtos.Any())
            {
                Notificar("Exclua os produtos desse fornecedor antes de excluí-lo");
                return false;
            }
            await _fornecedorRepository.Remover(id);

            var commit = await _unityOfWork.Commit();

            if (!commit)
            {
                Notificar("Ocorreu um erro ao tentar remover o item");
            }

            return true;
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
        }
    }
}
