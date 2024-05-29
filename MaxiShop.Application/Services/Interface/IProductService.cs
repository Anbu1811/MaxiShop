using MaxiShop.Application.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services.Interface
{
	public interface IProductService
	{
		Task<ShowProductDTO> CreateAsync(CreateProductDTO productDTO);

		Task UpdateAsync(UpdateProductDTO productDTO);

		Task DeleteAsync(int id);

		Task<ShowProductDTO> GetByIdAsync(int id);

		Task<IEnumerable<ShowProductDTO>> GetByFilterAsync(int? CategoryId, int? BrandId);

		Task<IEnumerable<ShowProductDTO>> GetAllAsync();
	}
}
