using NorthWind.Entities.Interfaces;
using NorthWind.Repositories.EFCore.DataContext;

namespace NorthWind.Repositories.EFCore.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly NorthWindContext Context;

		public UnitOfWork(NorthWindContext context)
		{
			Context = context;
		}

		public Task<int> SaveChangesAsync()
		{
			return Context.SaveChangesAsync();
		}
	}
}
