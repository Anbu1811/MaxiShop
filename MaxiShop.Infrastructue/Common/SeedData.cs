using MaxiShop.Domain.Model;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructue.Common
{
	public class SeedData
	{

		public static async Task SeedRoles(IServiceProvider serviceProvider)
		{
			var scope = serviceProvider.CreateScope();

			var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			var roles = new List<IdentityRole>
			{
				new IdentityRole {Name="ADMIN", NormalizedName="ADMIN"},
				new IdentityRole {Name="CUSTOMER", NormalizedName="CUSTOMER"}
			};


			foreach (var role in roles) 
			{
				if (! await rolemanager.RoleExistsAsync(role.Name)) 
				{
					await rolemanager.CreateAsync(role);
				}


				
			}



		}





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
