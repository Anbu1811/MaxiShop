using FluentValidation;
using MaxiShop.Application.DTO.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.DTO.Product
{
	public class CreateProductDTO
	{
		public int CategoryId { get; set; }

		public int BrandId { get; set; }

		public string Name { get; set; }

		public string Specification { get; set; }

		public double Price { get; set; }
	}

	public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
	{

        public CreateProductDTOValidator()
        {
			RuleFor(x => x.CategoryId).NotEmpty().NotNull().WithMessage("Id Field is Mandatory");
			RuleFor(x => x.BrandId).NotEmpty().NotNull().WithMessage("Id Field is Mandatory");
			RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name Field is Mandatory");
			RuleFor(x => x.Specification).NotEmpty().NotNull().MinimumLength(20).WithMessage("specification given more than 20 character ");
			RuleFor(x => x.Price).NotEmpty().NotNull().GreaterThanOrEqualTo(18000).WithMessage("Price given more than 15000");
			

		}
       
	}
}
