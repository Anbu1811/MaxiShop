using MaxiShop.Application.Common;
using MaxiShop.Application.InputModel;
using MaxiShop.Application.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Application.Services
{
	public class AuthService : IAuthService
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		private ApplicationUser ApplicationUser { get; set; }


        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
			_signInManager = signInManager;
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
			  await	_userManager.AddToRoleAsync(ApplicationUser,"ADMIN");
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
				return true;
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
	}
}
