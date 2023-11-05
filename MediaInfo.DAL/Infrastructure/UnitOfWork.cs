using Microsoft.EntityFrameworkCore.Storage;

namespace MediaInfo.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private MediaInfoDbContext _dataContext;
        private bool _disposed;

        public UnitOfWork(MediaInfoDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IRepository<T> GetRepository<T>() where T : class => new Repository<T>(_dataContext);

        public IDbContextTransaction BeginTransaction() => _dataContext.Database.BeginTransaction();

        public async Task<int> SaveChangesAsync() => await _dataContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                    _disposed = true;
                }
            }
            _disposed = false;
        }
    }
}
