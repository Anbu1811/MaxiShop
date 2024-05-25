using MaxiShop.Application.DTO.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services.Interface
{
	public interface IBrandService
	{
		Task<ShowBrandDTO> CreateAsync(CreateBrandDTO brandDTO);

		Task UpdateAsync(UpdateBrandDTO brandDTO);

		Task DeleteAsync(int id);

		Task<IEnumerable<ShowBrandDTO>> GetAllAsync();

		Task<ShowBrandDTO> GetByIdAsync(int id);
	}
}
