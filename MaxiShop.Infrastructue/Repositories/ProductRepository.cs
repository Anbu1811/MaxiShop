using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructue.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{ 
        public ProductRepository(ApplicationDbContext _dbContext) : base(_dbContext)
        {
            
        }

		public async Task<List<Product>> GetAllProductAsync()
		{
			var result = await _dbContext.Products.Include(x=>x.Category).Include(x=>x.Brand).AsNoTracking().ToListAsync();

			return result;
		}

		public  async Task<Product> GetByProductIdAsync(int id)
		{
			var result = await  _dbContext.Products.Include(x=>x.Category).Include(x=>x.Brand).AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
			return result;
		}

		public async Task UpdateAsync(Product endity)
		{
			_dbContext.Update(endity);
			await _dbContext.SaveChangesAsync();
			
		}
	}
}
