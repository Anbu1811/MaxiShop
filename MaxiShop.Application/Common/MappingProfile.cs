using AutoMapper;
using MaxiShop.Application.DTO.Category;
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

        }
    }
}
