using Maxishop.Frontent.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maxishop.Frontent.Pages
{
    public class ProductModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ProductModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<ProductDTO> Products { get; set; }

        public async void OnGet()
        {
            var httpclient = _httpClientFactory.CreateClient("worldwebAPI");
            Products = await httpclient.GetFromJsonAsync<List<ProductDTO>>("api/v1/Brand");

		}
    }
}
