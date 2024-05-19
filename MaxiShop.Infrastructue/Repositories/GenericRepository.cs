using MaxiShop.Domain.Common;
using MaxiShop.Domain.Contracts;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructue.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
	{
		protected ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<T> CreateAsync(T entity)
		{
			var create = await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();

			return create.Entity;

		}

		public async Task DeleteAsync(T entity)
		{
			_dbContext.Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			var list = await _dbContext.Set<T>().AsNoTracking().ToListAsync();
			return list;
		}

		public async Task<T> GetByIdAsync(Expression<Func<T, bool>> contition)
		{
			var getId = await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(contition);
			return getId;
		}
	}
}
