namespace Huachin.MusicStore.Application.Contracts.Services
{
	public interface IMusicStoreUnitOfWork
	{
		Task<int> SaveChangesAsync();
		Task BeginTransactionAsync();
		Task CommitTransactionAsync();
		Task RollbackTransactionAsync();
	}
}
