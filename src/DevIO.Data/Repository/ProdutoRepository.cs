using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid id)
        {
            return await db.Produtos.AsNoTracking()
                                    .Include(f => f.Fornecedor)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            return await db.Produtos.AsNoTracking()
                                    .Include(f => f.Fornecedor)
                                    .OrderBy(p => p.Nome)
                                    .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
