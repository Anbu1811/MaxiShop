using AutoMapper;
using MaxiShop.Application.DTO.Category;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
	internal class CategoryService : ICategoryService
	{

		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;


        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
			_mapper = mapper;
        }





        public async Task<ShowCategoryDTO> CreateAsync(CreateCategoryDTO categoryDTO)
		{
			var create = _mapper.Map<Category>(categoryDTO);

			var result = await _categoryRepository.CreateAsync(create);

			var createDTO = _mapper.Map<ShowCategoryDTO>(result);

			return createDTO;
		}

		public  async Task DeleteAsync(int id)
		{
			//var delete = _mapper.Map<Category>(id);

			var getid = await _categoryRepository.GetByIdAsync(x=>x.Id == id);
				 await _categoryRepository.DeleteAsync(getid);

			
		}

		
		public async Task<ShowCategoryDTO> GetByIdAsync(int id)
		{
			var result = await _categoryRepository.GetByIdAsync(x => x.Id == id);

			var getById = _mapper.Map<ShowCategoryDTO>(result);

			return getById;
			

			
		}

		public async Task UpdateAsync(UpdateCategoryDTO categoryDTO)
		{
			var update = _mapper.Map<Category>(categoryDTO);

			 await _categoryRepository.UpdateAsync(update);

			//var returnResult = _mapper.Map<ShowCategoryDTO>(result);

			//return result;
		}

		public async Task<IEnumerable<ShowCategoryDTO>> GetAllAsync()
		{

			var getAll = await _categoryRepository.GetAllAsync();


				var result = _mapper.Map<List<ShowCategoryDTO>>(getAll);

				return result;
		}
	}
}
