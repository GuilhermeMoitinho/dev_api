using DevIO.Business.Models.@enum;
using DevIO.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.ViewModels
{
    public class FornecedorViewModel
    {
        public Guid Id { get; private set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }
        public TipoFornecedor TipoFornecedor { get; set; }
        public bool Ativo { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public IEnumerable<ProdutoViewModel> Produtos { get; set; } 
    }
}
