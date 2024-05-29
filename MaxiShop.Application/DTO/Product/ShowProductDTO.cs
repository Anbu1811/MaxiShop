﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.DTO.Product
{
	public class ShowProductDTO
	{

		public int Id { get; set; }

		public int CategoryId { get; set; }

		public string Category {  get; set; }
		
		public int BrandId { get; set; }

		public string Brand {  get; set; }

		public int ExtablishYear { get; set; }
		
		
		public string Name { get; set; }

		public string Specification { get; set; }

		public double Price { get; set; }
	}
}
