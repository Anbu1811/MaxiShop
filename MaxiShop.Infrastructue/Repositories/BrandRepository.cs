using MaxiShop.Domain.Model;
using MaxiShop.Domain.Contracts;
using MaxiShop.Infrastructue.DbContexts;
using MaxiShop.Infrastructue.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MaxiShop.Infrastructue.Repositories
{
	public class BrandRepository : GenericRepository<Brand>, IBrandRepository
	{
		public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
		{

		}






		
		public async Task UpdateAsync(Brand entity)
		{
			 _dbContext.Update(entity);
			await _dbContext.SaveChangesAsync();


		}
	}
}
