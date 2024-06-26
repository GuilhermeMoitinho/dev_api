using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppDbContext context) : base(context) { }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid FornecedorId)
        {
            return await db.Enderecos.AsNoTracking()
                                     .FirstOrDefaultAsync(x => x.FornecedorId == FornecedorId);
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
           return await db.Fornecedores.AsNoTracking()
                                       .Include(x => x.Endereco)
                                       .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await db.Fornecedores.AsNoTracking()
                                       .Include(x => x.Endereco )
                                       .Include(x => x.Produtos)
                                       .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoverEnderecoFornecedor(Endereco endereco)
        { 
            db.Enderecos.Remove(endereco);
            await db.SaveChangesAsync();    
        }
    }
}
