using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Model;
using MaxiShop.Infrastructue.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaxiShop.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> Create([FromBody] Category category)
		{
			await _categoryRepository.CreateAsync(category);

			return Ok();

		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		
		public async Task<ActionResult<List<Category>>> GetAll()
		{
			var result = await _categoryRepository.GetAllAsync();
			
			return Ok(result);

		}

		[HttpGet]
		[Route("Details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> GetById(int id)
		{
			//var find = _dbContext.Categories.Find(id);
			var find = await _categoryRepository.GetByIdAsync(x=>x.Id == id);

			if(find == null)
			{
				return NotFound($"Category not found Id - {id}");
			}

			return Ok(find);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public async Task<ActionResult> Update([FromBody] Category category)
		{
			//var check = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
			
			//if(check == null)
			//{
			//	return NotFound($"Category not found Id - {category.Id}");
			//}

			await _categoryRepository.UpdateAsync(category);
			

			return NoContent();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]

		public async Task<ActionResult> Delete(int id)
		{
			var find = await _categoryRepository.GetByIdAsync(x => x.Id == id);

			if(find == null)
			{
				return NotFound($"Category not found Id - {id}");
			}

			await _categoryRepository.DeleteAsync(find);

			return NoContent();
		}
    }
}
