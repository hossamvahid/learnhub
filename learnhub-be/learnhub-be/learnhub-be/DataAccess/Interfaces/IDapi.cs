using learnhub_be.DataAccess.Models;

namespace learnhub_be.DataAccess.Interfaces
{
    public interface IDapi : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Curricula> Curriculas { get; }
        IRepository<Event> Events { get; }
        IRepository<Participant> Participants { get; }


        Task<int> CompleteAsync();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
