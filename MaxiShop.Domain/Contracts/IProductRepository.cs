using MaxiShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Domain.Contracts
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		Task<List<Product>> GetAllProductAsync();

	
		Task <Product> GetByProductIdAsync(int id);
		Task UpdateAsync(Product endity);
	}
}
