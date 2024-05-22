using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MaxiShop.Application.Services.Interface;
using MaxiShop.Application.DTO.Category;

namespace MaxiShop.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;


		public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Create([FromBody] CreateCategoryDTO category)
		{
			var result =await _categoryService.CreateAsync(category);

			return Ok(result);

		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		
		public async Task<ActionResult<List<ShowCategoryDTO>>> GetAll()
		{
			var result = await _categoryService.GetAllAsync();
			
			return Ok(result);

		}

		[HttpGet]
		[Route("Details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetById(int id)
		{
			//var find = _dbContext.Categories.Find(id);
			var find = await _categoryService.GetByIdAsync(id);

			if(find == null)
			{
				return NotFound($"Category not found Id - {id}");
			}

			return Ok(find);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Update([FromBody] UpdateCategoryDTO category)
		{
			//var check = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);

			//if(check == null)
			//{
			//	return NotFound($"Category not found Id - {category.Id}");
			//}

			await _categoryService.UpdateAsync(category);
			

			return NoContent();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]

		public async Task<ActionResult> Delete(int id)
		{
			var find = await _categoryService.GetByIdAsync(id);

			if (find == null)
			{
				return NotFound($"Category not found Id - {id}");
			}

			await _categoryService.DeleteAsync(id);

			return NoContent();
		}
    }
}
