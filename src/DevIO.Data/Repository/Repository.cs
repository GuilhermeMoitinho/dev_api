using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DevIO.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly DbSet<T> DbSet;
        protected readonly AppDbContext db;

        protected Repository(AppDbContext context)
        {
            db = context;
            DbSet = context.Set<T>();  
        }

        public virtual async Task<T> ObterPorId(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> expression)
        {
            return await DbSet.AsNoTracking().Where(expression).ToListAsync();
        }

        public virtual async Task<List<T>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(T entity)
        {
            await DbSet.AddAsync(entity);   
        }

        public virtual async Task Atualizar(T entity)
        {
            DbSet.Update(entity);   
        }

        public virtual async Task Remover(Guid id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
        }

        public void Dispose()
        {
            db.Dispose(); 
        }
    }
}
