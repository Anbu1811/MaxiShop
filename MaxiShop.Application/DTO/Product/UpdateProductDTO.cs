﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.DTO.Product
{
	public class UpdateProductDTO
	{
		public int CategoryId { get; set; }

		public int BrandId { get; set; }

		public int Id { get; set; }
		public string Name { get; set; }

		public string Specification { get; set; }

		public double Price { get; set; }
	}
}
