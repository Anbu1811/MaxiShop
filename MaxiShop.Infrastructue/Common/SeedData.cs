using MaxiShop.Domain.Model;
using MaxiShop.Infrastructue.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructue.Common
{
	public class SeedData
	{
		public static async Task SeedDataAsync( ApplicationDbContext _dbContext)
		{
			if (! _dbContext.Brands.Any())
			{
				await _dbContext.AddRangeAsync(
					new Brand
					{
						Name = "Samsung",
						ExtablishYear = 1995
					},
					new Brand
					{
						Name = "Nokia",
						ExtablishYear = 1995
					},
					new Brand
					{
						Name = "OppO",
						ExtablishYear = 1995
					},
					new Brand
					{
						Name = "Vivo",
						ExtablishYear = 1995
					},
					new Brand
					{
						Name = "OnePlus",
						ExtablishYear = 1995
					});
				await _dbContext.SaveChangesAsync();
			}
		}
	}
}
