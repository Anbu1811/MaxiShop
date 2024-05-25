using AutoMapper;
using MaxiShop.Application.DTO.Brand;
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
	public class BrandServices : IBrandService
	{

		public readonly IBrandRepository _brandRepository;
		public readonly IMapper _mapper;

        public BrandServices(IBrandRepository brandRepository, IMapper mapper)
        {
            
			_brandRepository = brandRepository;
			_mapper = mapper;
        }









        public async Task<ShowBrandDTO> CreateAsync(CreateBrandDTO brandDTO)
		{
			var convert =  _mapper.Map<Brand>(brandDTO);

			var create = await _brandRepository.CreateAsync(convert);

			var returnn = _mapper.Map <ShowBrandDTO>(create);

			return returnn;
		}

		public async Task DeleteAsync(int id)
		{
			var find = await _brandRepository.GetByIdAsync(x=>x.Id == id);

			 await  _brandRepository.DeleteAsync(find);

		}

		public async Task<IEnumerable<ShowBrandDTO>> GetAllAsync()
		{
			var getList = await _brandRepository.GetAllAsync();

			var convert = _mapper.Map<List<ShowBrandDTO>>(getList);

			return convert;
		}

		public async Task<ShowBrandDTO> GetByIdAsync(int id)
		{
			var getId = await _brandRepository.GetByIdAsync(x => x.Id == id);

			var convert = _mapper.Map<ShowBrandDTO>(getId);

			return convert;
		}

		public async Task UpdateAsync(UpdateBrandDTO brandDTO)
		{
			var convert = _mapper.Map<Brand>(brandDTO);

			 await _brandRepository.UpdateAsync(convert);

			
		}
	}
}
