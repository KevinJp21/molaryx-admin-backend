using Domain.Contracts.IRepositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace FtfApiClient.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public bool IsInTransaction => _currentTransaction != null;

        public async Task BeginTransactionAsync(
            CancellationToken? cancellationToken = null)
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = await _context.Database
                .BeginTransactionAsync(cancellationToken ?? CancellationToken.None);
        }

        public async Task CommitTransactionAsync(CancellationToken? cancellationToken = null)
        {
            if (_currentTransaction == null)
                return;

            try
            {
                await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
                await _currentTransaction.CommitAsync(cancellationToken ?? CancellationToken.None);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken? cancellationToken = null)
        {
            if (_currentTransaction == null)
                return;

            await _currentTransaction.RollbackAsync(cancellationToken ?? CancellationToken.None);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task<int> SaveChangeAsync(CancellationToken? cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }

            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
