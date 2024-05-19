using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using MaxiShop.Infrastructue.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructue.Repositories
{
	internal class CategoryRepository : GenericRepository<Category>, ICategoryRepository
	{
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task UpdateAsync(Category entity)
		{
			_dbContext.Update(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}
