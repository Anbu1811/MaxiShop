using AutoMapper;
using MaxiShop.Application.InputModel;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
	public class PaginationService<T, S> : IPaginationService<T, S> where T : class
	{
		public readonly IMapper _mapper;

        public PaginationService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PaginationVM<T> GetPagination(List<S> source, PaginationIM input)
		{
			var currentPage = input.PageNumber;

			var pageSize = input.PageSize;

			var totalNoOfRecords = source.Count;

			var totalPages = (int)Math.Ceiling(totalNoOfRecords/ (double)pageSize);

			var result = source
				.Skip((input.PageNumber -1)*(input.PageSize))
				.Take(input.PageSize)
				.ToList();

			var final = _mapper.Map<List<T>>(result);

			PaginationVM<T> pagination = new PaginationVM<T>(currentPage,totalPages, pageSize, totalNoOfRecords, final);

			return pagination;
		}
	}
}
