using Huachin.MusicStore.Application.Contracts.Repositories;
using Huachin.MusicStore.Application.Contracts.Services;
using Huachin.MusicStore.Infrastructure.Configurations.Contexts;
using Huachin.MusicStore.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Huachin.MusicStore.Infrastructure.Services
{
	public class MusicStoreUnitOfWork : IMusicStoreUnitOfWork
	{
		private readonly MusicStoreDbContext _dbContext;
		private readonly ICustomerRepository _customerRepository;
		private IDbContextTransaction? _currentTransaction;

		public MusicStoreUnitOfWork(MusicStoreDbContext dbContext)
		{
			_dbContext = dbContext;
			_customerRepository = new CustomerRepository(_dbContext);
		}

		public async Task BeginTransactionAsync()
		{
			if (_currentTransaction != null)
			{
				throw new InvalidOperationException("There is already an active transaction.");
			}

			_currentTransaction = await _dbContext.Database.BeginTransactionAsync();
		}

		public async Task CommitTransactionAsync()
		{
			try
			{
				if (_currentTransaction == null)
					throw new InvalidOperationException("No transaction in progress to commit.");

				await SaveChangesAsync();
				await _currentTransaction.CommitAsync();
			}
			catch
			{
				await RollbackTransactionAsync();
				throw;
			}
			finally
			{
				if (_currentTransaction != null)
				{
					await _currentTransaction.DisposeAsync();
					_currentTransaction = null;
				}
			}
		}

		public async Task RollbackTransactionAsync()
		{
			try
			{
				if (_currentTransaction == null)
					throw new InvalidOperationException("No transaction in progress to roll back.");

				await _currentTransaction.RollbackAsync();
			}
			finally
			{
				if (_currentTransaction != null)
				{
					await _currentTransaction.DisposeAsync();
					_currentTransaction = null;
				}
			}
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _dbContext.SaveChangesAsync();
		}
	}
}
