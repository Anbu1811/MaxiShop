using MaxiShop.Application.ApplicatioConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.Exceptions;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandController : ControllerBase
	{

		public readonly IBrandService _brandServices;
		public APIResponse _apiResponse;

        public BrandController(IBrandService brandServices)
        {
            _brandServices = brandServices;
			_apiResponse = new APIResponse();
        }


		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Create([FromBody] CreateBrandDTO brandDTO)
		{
			try
			{
				var create = await _brandServices.CreateAsync(brandDTO);

				_apiResponse.StatusCode = HttpStatusCode.Created;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = create;
				_apiResponse.Message = CommonMessage.CreateOperationSuccess;


				return Ok(_apiResponse);
			}
			catch(BadRequestException ex)
			{
				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.IsSuccess = false;
				_apiResponse.Message = CommonMessage.CreateOperationFailed;
				_apiResponse.AddWarning(ex.Message);
				_apiResponse.AddError(CommonMessage.SystemError);
				_apiResponse.Result = ex.ValidationErrors;
			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.IsSuccess = false;
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
				var getList = await _brandServices.GetAllAsync();

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = getList;
				
				return Ok(_apiResponse);


			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.NotFound;
				_apiResponse.AddError(CommonMessage.SystemError);

			}

			return Ok(_apiResponse);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateBrandDTO brandDTO)
		{

			try
			{
				var find = await _brandServices.GetByIdAsync(brandDTO.Id);

				if (find == null)
				{
					_apiResponse.StatusCode = HttpStatusCode.NotFound;
					
					_apiResponse.Message = CommonMessage.UpdateOperationFailed;

					return Ok(_apiResponse);
				}

			    	await  _brandServices.UpdateAsync(brandDTO);

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = brandDTO;
				_apiResponse.Message = CommonMessage.UpdateOperationSuccess;


				return Ok(_apiResponse);
			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.AddError(CommonMessage.UpdateOperationFailed);
				_apiResponse.Message = CommonMessage.UpdateOperationFailed;
			}


			return Ok(_apiResponse);
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Delete(int id)
		{
			try
			{
				var find = await _brandServices.GetByIdAsync(id);

				if (find == null)
				{
					_apiResponse.StatusCode = HttpStatusCode.NotFound;
					
					_apiResponse.Message = CommonMessage.DeleteOperationFailed;
					return Ok(_apiResponse);
				}

				await _brandServices.DeleteAsync(id);


				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = find;
				_apiResponse.Message = CommonMessage.DeleteOperationSuccess;

				return Ok(_apiResponse);


			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.AddError(CommonMessage.SystemError);
				_apiResponse.Message = CommonMessage.DeleteOperationFailed;
			}

			return Ok(_apiResponse);
		}

		[HttpGet]
		[Route("Details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetId(int id)
		{
			try
			{
				var get = await _brandServices.GetByIdAsync(id);

				if (get == null)
				{
					_apiResponse.StatusCode = HttpStatusCode.NotFound;
					_apiResponse.IsSuccess = false;
					_apiResponse.Message = CommonMessage.RecordNotFound;

					return Ok(_apiResponse);
				}

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = get;


				return Ok(_apiResponse);


			}
			catch (Exception)
			{

				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.AddError(CommonMessage.SystemError);
			}

			return Ok(_apiResponse);
		}







	}
}
