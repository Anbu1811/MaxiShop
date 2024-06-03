using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.ViewModel
{
	public class PaginationVM<T>
	{
		public int CurrentPage { get; set; }

		public int TotalPage { get; set; }

		public int PageSize { get; set; }

		public int TotalNoOfRecords { get; set; }

		

		public bool HasPrevious => CurrentPage > 1;

		public bool HasNext => CurrentPage < TotalPage;

		public List<T> Items { get; set; }

		public PaginationVM(int currentpage, int totalpage, int pagesize, int totalnoofrecords, List<T> items)
        {
            CurrentPage = currentpage;
			TotalPage = totalpage;
			TotalNoOfRecords = totalnoofrecords;
			Items = items;
			PageSize = pagesize;
        }

    }
}
