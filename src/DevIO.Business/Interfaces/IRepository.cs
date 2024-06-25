using DevIO.Business.Models;
using System.Linq.Expressions;

namespace DevIO.Business.Interfaces
{
    public interface IRepository<TE> : IDisposable where TE : Entity
    {
        Task<IEnumerable<TE>> Buscar(Expression<Func<TE, bool>> expression);
        Task<TE> ObterPorId(Guid id);
        Task<List<TE>> ObterTodos();
        Task Adicionar(TE entity);
        Task Atualizar(TE entity);
        Task Remover(Guid id);
    }
}
