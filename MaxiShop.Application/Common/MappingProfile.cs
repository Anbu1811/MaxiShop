using AutoMapper;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.DTO.Category;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Common
{
	public class MappingProfile : Profile 
	{
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, ShowCategoryDTO>().ReverseMap();


            CreateMap<Brand, CreateBrandDTO>().ReverseMap();
            CreateMap<Brand,  UpdateBrandDTO>().ReverseMap();
            CreateMap<Brand,  ShowBrandDTO>().ReverseMap();


            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product,  ShowProductDTO>()
                .ForMember(x=>x.Category,opt => opt.MapFrom(source => source.Category.Name))
				.ForMember(x => x.Brand, opt => opt.MapFrom(source => source.Brand.Name))
				.ForMember(x => x.ExtablishYear, opt => opt.MapFrom(source => source.Brand.ExtablishYear));
		}
    }
}
