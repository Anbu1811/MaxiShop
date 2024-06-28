using MaxiShop.Application.ApplicatioConstants;
using MaxiShop.Application.Common;
using MaxiShop.Application.InputModel;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MaxiShop.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IAuthService _authService;
		public APIResponse _apiResponse;

		public UserController(IAuthService authService)
		{
			_authService = authService;
			_apiResponse = new APIResponse();
		}


		[HttpPost]
		[Route("Register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> Register(Register register)
		{

			try
			{
				if (!ModelState.IsValid)
				{
					_apiResponse.AddError(ModelState.ToString());
					_apiResponse.AddWarning(CommonMessage.RegistrationFailed);

					return _apiResponse;
				}

				var result = await _authService.Register(register);


				_apiResponse.StatusCode = HttpStatusCode.Created;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = result;
				_apiResponse.Message = CommonMessage.RegistrationSuccess;


			}
			catch (Exception)
			{


				_apiResponse.StatusCode = HttpStatusCode.InternalServerError;
				_apiResponse.IsSuccess = false;
				_apiResponse.Message = CommonMessage.RegistrationFailed;
				_apiResponse.AddError(CommonMessage.RegistrationFailed);
			}


			return Ok(_apiResponse);
		}

		[HttpPost]
		[Route("Login")]
		public async Task<ActionResult<APIResponse>> LogIn(Login login)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					_apiResponse.AddError(ModelState.ToString());
					_apiResponse.AddWarning(CommonMessage.LoginFailed);
					return _apiResponse;
				}

				var result = await _authService.Login(login);

				if (result is string)
				{
					_apiResponse.StatusCode =HttpStatusCode.BadRequest;
					
					_apiResponse.Message= CommonMessage.LoginFailed;
					_apiResponse.Result= result;

					return Ok(_apiResponse);
				}

				_apiResponse.StatusCode = HttpStatusCode.OK;
				_apiResponse.IsSuccess = true;
				_apiResponse.Result = result;
				_apiResponse.Message= CommonMessage.LoginSuccess;

				
			}
			catch (Exception)
			{
				_apiResponse.StatusCode =HttpStatusCode.InternalServerError;
				_apiResponse.IsSuccess = false;
				_apiResponse.Message = CommonMessage.LoginFailed;
				_apiResponse.AddError(CommonMessage.LoginFailed);

				
			}

			return Ok(_apiResponse);
		}
    }
}
