using learnhub_be.DataAccess.Interfaces;
using learnhub_be.DataAccess.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace learnhub_be.DataAccess.Implementations
{
    public class Dapi : IDapi
    {
        private readonly PgSqlDbContext _context;
        private IDbContextTransaction? _transaction;

        public IRepository<User> Users { get; }
        public IRepository<Event> Events { get; }
        public IRepository<Curricula> Curriculas { get; }
        public IRepository<Participant> Participants { get; }



        public Dapi(PgSqlDbContext context)
        {
            _context = context;
            Users = new Repository<User>(_context);
            Events = new Repository<Event>(_context);
            Curriculas = new Repository<Curricula>(_context);
            Participants = new Repository<Participant>(_context);

        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress.");
            }

            try
            {
                await _transaction.CommitAsync();
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress.");
            }

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
