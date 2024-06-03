using MaxiShop.Application.ApplicatioConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.DTO.Product;
using MaxiShop.Application.InputModel;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IProductService _productService;
		private APIResponse _apiResopnse;

		public ProductController(IProductService productServices)
		{
			_productService = productServices;
			_apiResopnse = new APIResponse();
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Create([FromBody] CreateProductDTO productDTO)
		{
			try
			{
				var result = await _productService.CreateAsync(productDTO);

				_apiResopnse.StatusCode = HttpStatusCode.Created;
				_apiResopnse.IsSuccess = true;
				_apiResopnse.Result = result;
				_apiResopnse.Message = CommonMessage.CreateOperationSuccess;

				return Ok(_apiResopnse);
			}
			catch (Exception)
			{

				_apiResopnse.StatusCode = HttpStatusCode.BadRequest;
				_apiResopnse.Message = CommonMessage.CreateOperationFailed;
				_apiResopnse.AddError(CommonMessage.SystemError);
			}

			return Ok(_apiResopnse);


		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetAll()
		{
			try
			{
				var getAll = await _productService.GetAllAsync();

				_apiResopnse.StatusCode = HttpStatusCode.OK;
				_apiResopnse.IsSuccess = true;
				_apiResopnse.Result = getAll;


				return Ok(_apiResopnse);

			}
			catch (Exception)
			{
				_apiResopnse.StatusCode = HttpStatusCode.NotFound;

				_apiResopnse.AddError(CommonMessage.SystemError);
			}

			return Ok(_apiResopnse);

		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]

		public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateProductDTO updateProductDTO)
		{
			try
			{
				await _productService.UpdateAsync(updateProductDTO);

				_apiResopnse.StatusCode = HttpStatusCode.Created;
				_apiResopnse.IsSuccess = true;
				_apiResopnse.Message = CommonMessage.UpdateOperationSuccess;

				return Ok(_apiResopnse);
			}
			catch (Exception)
			{

				_apiResopnse.StatusCode = HttpStatusCode.NotFound;
				_apiResopnse.Message = CommonMessage.SystemError;
				_apiResopnse.AddError(CommonMessage.SystemError);
			}

			return Ok(_apiResopnse);
		}

		[HttpGet]
		[Route("Details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetById(int id)
		{


			try
			{
				var getId = await _productService.GetByIdAsync(id);

				_apiResopnse.StatusCode = HttpStatusCode.OK;
				_apiResopnse.IsSuccess = true;
				_apiResopnse.Result = getId;

				return Ok(_apiResopnse);

			}
			catch (Exception)
			{

				_apiResopnse.StatusCode = HttpStatusCode.NotFound;
				_apiResopnse.Message = CommonMessage.SystemError;
			}

			return Ok(_apiResopnse);
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[Route("Filter")]
		public async Task<ActionResult<APIResponse>> GetbyFilter(int? CategoryId, int? BrandId)
		{
			try
			{
				var result = await _productService.GetByFilterAsync(CategoryId, BrandId);

				_apiResopnse.StatusCode = HttpStatusCode.OK;
				_apiResopnse.IsSuccess = true;
				_apiResopnse.Result = result;

				return Ok(_apiResopnse);
			}
			catch (Exception)
			{

				_apiResopnse.StatusCode = HttpStatusCode.NotFound;
				_apiResopnse.Message = CommonMessage.SystemError;
				_apiResopnse.IsSuccess = false;

			}
			return Ok(_apiResopnse);
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Delete(int id)
		{
			try
			{
				await _productService.DeleteAsync(id);

				_apiResopnse.StatusCode = HttpStatusCode.OK;
				_apiResopnse.IsSuccess = true;
				_apiResopnse.Message = CommonMessage.DeleteOperationSuccess;

				return Ok(_apiResopnse);
			}
			catch (Exception)
			{

				_apiResopnse.StatusCode = HttpStatusCode.NotFound;
				_apiResopnse.Message = CommonMessage.DeleteOperationSuccess;
			}

			return Ok(_apiResopnse);


		}

		[HttpPost]
		[Route("Pagination")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Getpagination(PaginationIM pagination)
		{
			try
			{
				var result = await _productService.GetPagination(pagination);

				_apiResopnse.StatusCode = HttpStatusCode.OK;
				_apiResopnse.IsSuccess = true;
				_apiResopnse.Result = result;


			}
			catch (Exception)
			{

				_apiResopnse.StatusCode = HttpStatusCode.NotFound;
				_apiResopnse.IsSuccess = false;

			}

			return Ok(_apiResopnse);
			 
		}

	}
}
