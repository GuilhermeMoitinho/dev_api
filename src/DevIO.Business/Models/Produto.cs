namespace DevIO.Business.Models
{
    public class Produto : Entity
    {
        public Produto(string nome, 
                       string descricao, 
                       decimal valor, 
                       DateTime dataCadastro, 
                       bool ativo, 
                       Guid fornecedorId, 
                       Fornecedor fornecedor)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            DataCadastro = dataCadastro;
            Ativo = ativo;
            FornecedorId = fornecedorId;
            Fornecedor = fornecedor;
        }

        public Produto() { }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }


    }
}