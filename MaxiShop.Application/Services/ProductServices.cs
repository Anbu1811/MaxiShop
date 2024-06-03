using AutoMapper;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Application.InputModel;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Application.ViewModel;
using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
	public class ProductServices : IProductService
	{
		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepostory;
		private readonly IPaginationService<ShowProductDTO,Product> _pagination;

        public ProductServices(IProductRepository productRepostory, IMapper mapper, IPaginationService<ShowProductDTO, Product> pagination)
        {
			_productRepostory = productRepostory;
			_mapper = mapper;
			_pagination = pagination;
        }


        public async Task<ShowProductDTO> CreateAsync(CreateProductDTO productDTO)
		{
			var create =  _mapper.Map<Product>(productDTO);

			var result = await _productRepostory.CreateAsync(create);

			var show = _mapper.Map<ShowProductDTO>(result);
			
			return show;
		}

		public async Task DeleteAsync(int id)
		{
			var delete = await _productRepostory.GetByIdAsync(x=>x.Id == id);
			await _productRepostory.DeleteAsync(delete);
		}

		public async Task<IEnumerable<ShowProductDTO>> GetAllAsync()
		{
			var getAll = await _productRepostory.GetAllProductAsync();

			var result = _mapper.Map<List<ShowProductDTO>>(getAll);

			return result;
		}

		public async Task<IEnumerable<ShowProductDTO>> GetByFilterAsync(int? CategoryId, int? BrandId)
		{
			var data = await _productRepostory.GetAllProductAsync();

			IEnumerable<Product> query = data;


			if (CategoryId > 0)
			{
				query = query.Where(x=>x.CategoryId == CategoryId);
			}

			if (BrandId > 0) 
			{
				query = query.Where(x => x.BrandId == BrandId);
			}

			var result = _mapper.Map<List<ShowProductDTO>>(query);



			return result;
		}

		public async Task<ShowProductDTO> GetByIdAsync(int id)
		{
			var getId = await _productRepostory.GetByProductIdAsync(id);

			var result = _mapper.Map<ShowProductDTO>(getId);

			return result;
		}

		public async Task<PaginationVM<ShowProductDTO>> GetPagination(PaginationIM pagination)
		{

			var source = await _productRepostory.GetAllProductAsync();

			var result = _pagination.GetPagination(source, pagination);

			return result;
		}

		public async Task UpdateAsync(UpdateProductDTO productDTO)
		{
			var update = _mapper.Map<Product>(productDTO);

			 await _productRepostory.UpdateAsync(update);

			
		}
	}
}
