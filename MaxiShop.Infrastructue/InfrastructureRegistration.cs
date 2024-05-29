using MaxiShop.Domain.Contracts;
using MaxiShop.Infrastructue.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructue
{
	public static class InfrastructureRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) 
		{
			services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IBrandRepository, BrandRepository>();
			services.AddScoped<IProductRepository, ProductRepository>();

			return services;
		}
	}
}
