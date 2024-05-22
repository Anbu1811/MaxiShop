using MaxiShop.Application.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services.Interface
{
	public interface ICategoryService
	{
		Task<ShowCategoryDTO> CreateAsync(CreateCategoryDTO categoryDTO);

		Task UpdateAsync(UpdateCategoryDTO categoryDTO);

		Task DeleteAsync(int id);

		Task<ShowCategoryDTO> GetByIdAsync(int id);

		Task<IEnumerable<ShowCategoryDTO>> GetAllAsync();
	}
}
