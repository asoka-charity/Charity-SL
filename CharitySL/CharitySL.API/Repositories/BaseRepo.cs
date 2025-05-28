using CharitySL.API.Data;
using CharitySL.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CharitySL.API.Repositories
{
	public class BaseRepo(AppDbContext context) : IRepo
	{
		public AppDbContext _context = context;

		public int SaveChanges()
		{
			try
			{
				return _context.SaveChanges();
			}
			catch (DbUpdateConcurrencyException ex)
			{
				throw HandleConcurrencyError(ex);
			}
			catch (DbUpdateException ex)
			{
				throw HandleDbUpdateError(ex);
			}
		}

		protected virtual Exception HandleConcurrencyError(DbUpdateConcurrencyException ex)
		{
			// Update the values of the entity that failed to save from the store 
			ex.Entries.Single().Reload();

			throw new Exception("Something unexpected happen in the server");
		}

		protected virtual Exception HandleDbUpdateError(DbUpdateException ex)
		{
			// Update the values of the entity that failed to save from the store 
			ex.Entries.Single().Reload();

			throw new Exception("DB Update Error");
		}
	}
}
