using MaxiShop.Application.ApplicatioConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.DTO.Brand;
using MaxiShop.Application.Exceptions;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Web.Controllers.V2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class BrandController : ControllerBase
    {

        public readonly IBrandService _brandServices;
        public APIResponse _apiResponse;

        public BrandController(IBrandService brandServices)
        {
            _brandServices = brandServices;
            _apiResponse = new APIResponse();
        }


        

        [HttpGet]
        [ResponseCache(CacheProfileName = "Default")]
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
