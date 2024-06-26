using DevIO.Business.Models.@enum;

namespace DevIO.Business.Models
{
    public class Fornecedor : Entity
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public bool Ativo { get; set; }
        public Endereco Endereco { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }

        public Fornecedor(string nome, 
                          string documento, 
                          TipoFornecedor tipoFornecedor, 
                          bool ativo, 
                          Endereco endereco, 
                          IEnumerable<Produto> produtos)
        {
            Nome = nome;
            Documento = documento;
            TipoFornecedor = tipoFornecedor;
            Ativo = ativo;
            Endereco = endereco;
            Produtos = produtos;
        }

        public void Update(string nome,
                          string documento,
                          TipoFornecedor tipoFornecedor,
                          bool ativo,
                          Endereco endereco)
        {
            Nome = nome;
            Documento = documento;
            TipoFornecedor = tipoFornecedor;
            Ativo = ativo;
            Endereco = endereco;
        }

        public Fornecedor() { }

    }
}
