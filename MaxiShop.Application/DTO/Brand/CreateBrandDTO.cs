using FluentValidation;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.DTO.Brand
{
	public class CreateBrandDTO
	{
		
		public string Name { get; set; }

		
		public int ExtablishYear { get; set; }
	}

	public class CreateBrandDTOValidation : AbstractValidator<CreateBrandDTO>
	{
		public CreateBrandDTOValidation() 
		{
			RuleFor(x=>x.Name).NotEmpty().NotNull().WithMessage("Name Field is Mandotory");
			RuleFor(x=>x.ExtablishYear).NotEmpty().NotNull().GreaterThanOrEqualTo(1950).WithMessage("Brand year between 1950 to current year");
		}

	}
}
