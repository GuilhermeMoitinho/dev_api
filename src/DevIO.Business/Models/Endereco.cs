using System.Text.Json.Serialization;

namespace DevIO.Business.Models
{
    public class Endereco : Entity
    {
        public Endereco(string logradouro, 
                        string numero, 
                        string complemento, 
                        string cep, 
                        string bairro, 
                        string cidade, 
                        string estado, 
                        Guid fornecedorId, 
                        Fornecedor fornecedor)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            FornecedorId = fornecedorId;
            Fornecedor = fornecedor;
        }

        public Endereco() { }

        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }
    }
}
