using MaxiShop.Application.Common;
using MaxiShop.Application.InputModel;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Application.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
	public class AuthService : IAuthService
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IConfiguration _config;

		private ApplicationUser ApplicationUser { get; set; }


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
			_signInManager = signInManager;
			_config = config;
			ApplicationUser = new();
        }

        public async Task<IEnumerable<IdentityError>> Register(Register register)
		{
			ApplicationUser.FirstName = register.FirstName;
			ApplicationUser.LastName = register.LastName;
			ApplicationUser.Email = register.Email;
			ApplicationUser.UserName = register.Email;
			//ApplicationUser.UserName = register.FirstName + " " + register.LastName;




			var result = await _userManager.CreateAsync(ApplicationUser,register.Password);

			if (result.Succeeded)
			{
			  await	_userManager.AddToRoleAsync(ApplicationUser,"CUSTOMER");
			}

			return result.Errors;

		}

		public async Task<object> Login(Login login)
		{
			ApplicationUser = await _userManager.FindByEmailAsync(login.Email);

			if (ApplicationUser == null)
			{
				return "Please input valid Email ID";
			}

			var result = await _signInManager.PasswordSignInAsync(ApplicationUser,login.Password,isPersistent:true,lockoutOnFailure:true);

			var isValid = await _userManager.CheckPasswordAsync(ApplicationUser,login.Password);

			if(result.Succeeded)
			{
				var token = await GenerateToken();

				LoginResponse loginResponse = new LoginResponse
				{
					UserId = ApplicationUser.Id,
					Token = token,
				};

				return loginResponse;
			}
			else
			{
				if (result.IsLockedOut)
				{
					return "Your accout is locked Plaease contact Admin";
				}
				if (result.IsNotAllowed)
				{
					return "Please verify your Email Address";
				}
				if(isValid == false)
				{
					return "Invalid Password";
				}
				else
				{
					return "LogIn Failed";
				}
			}



			
		 
		}


		public async Task<string> GenerateToken()
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));

			var signInCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

			var roles = await _userManager.GetRolesAsync(ApplicationUser);

			var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

			List<Claim> claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Email, ApplicationUser.Email),
			}.Union(roleClaims).ToList();

			var token = new JwtSecurityToken
				(
				issuer: _config["JwtSettings:Issuer"],
				audience: _config["JwtSettings:Audience"],
				claims: claims,
				signingCredentials: signInCredentials,
				expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JwtSettings:DurationInMinutes"]))
				);

			return new JwtSecurityTokenHandler().WriteToken(token);

			
		
		}
	}

	
}
