using DevIO.Business.Interfaces;
using DevIO.Data.Context;

namespace DevIO.Data.UoW
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly AppDbContext _context;

        public UnityOfWork(AppDbContext context) => _context = context;

        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
