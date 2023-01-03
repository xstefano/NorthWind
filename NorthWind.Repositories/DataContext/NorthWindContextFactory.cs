using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NorthWind.Repositories.EFCore.DataContext
{
	class NorthWindContextFactory : IDesignTimeDbContextFactory<NorthWindContext>
	{
		public NorthWindContext CreateDbContext(string[] args)
		{
			var OptionBuilder =
				new DbContextOptionsBuilder<NorthWindContext>();

			OptionBuilder.UseSqlServer(
				"Server=CLOUDS\\SQLEXPRESS;Database=DBNorthWind;Trusted_Connection=True;TrustServerCertificate=True;");

			return new NorthWindContext(OptionBuilder.Options);
		}
	}
}
