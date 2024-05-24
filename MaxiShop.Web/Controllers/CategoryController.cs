using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Application.DTO.Category;
using MaxiShop.Application.Common;
using System.Net;
using MaxiShop.Application.ApplicatioConstants;

namespace MaxiShop.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		protected APIResponse _apiResponse;


		public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
			_apiResponse = new APIResponse();
        }

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		
		public async Task<ActionResult<APIResponse>> Create([FromBody] CreateCategoryDTO category)
		{
			try
			{
				var result = await _categoryService.CreateAsync(category);

				_apiResponse.StatusCode = HttpStatusCode.Created;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = result;
				_apiResponse.Message = CommonMessage.CreateOperationSuccess;
			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = CommonMessage.CreateOperationFailed;
				_apiResponse.AddError(CommonMessage.SystemError);
			}

			return Ok(_apiResponse);

		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		
		public async Task<ActionResult<APIResponse>> GetAll()
		{
			try
			{
				var result = await _categoryService.GetAllAsync();

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = result;
			}
			catch (Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.AddError(CommonMessage.SystemError);
			}
			
			return Ok(_apiResponse);

		}

		[HttpGet]
		[Route("Details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetById(int id)
		{
			try
			{

				var find = await _categoryService.GetByIdAsync(id);

				if (find == null)
				{
					_apiResponse.StatusCode = HttpStatusCode.NotFound;
					_apiResponse.Message = CommonMessage.RecordNotFound;
					return Ok(_apiResponse);
				}

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = find;
				

			}
			catch (Exception)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.AddError(CommonMessage.SystemError);
			}
			return Ok(_apiResponse);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateCategoryDTO category)
		{

			try
			{
				 var find = await _categoryService.GetByIdAsync(category.ID);

				if (find == null)
				{
					_apiResponse.StatusCode = HttpStatusCode.NotFound;
					_apiResponse.Message = CommonMessage.UpdateOperationFailed;

					return Ok(_apiResponse);
				}

				await _categoryService.UpdateAsync(category);

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = category;
				_apiResponse.Message = CommonMessage.UpdateOperationSuccess;

			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = CommonMessage.UpdateOperationFailed;
				_apiResponse.AddError(CommonMessage.UpdateOperationFailed);
			}
			

			return Ok(_apiResponse);
			}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Delete(int id)
		{
			try
			{
				var find = await _categoryService.GetByIdAsync(id);

				

				if (find == null)
				{
					
					_apiResponse.StatusCode = HttpStatusCode.NotFound;
					_apiResponse.Message = CommonMessage.DeleteOperationFailed;
					
					return Ok(_apiResponse);

				}

				await _categoryService.DeleteAsync(id);

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess	=true;
				_apiResponse.Result = find;
				_apiResponse.Message = CommonMessage.DeleteOperationSuccess;

			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.Message = CommonMessage.DeleteOperationFailed;
				_apiResponse.AddError(CommonMessage.SystemError);
			}

			return Ok(_apiResponse);
		}
    }
}
