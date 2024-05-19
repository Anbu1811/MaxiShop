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
		private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public ActionResult Create([FromBody] Category category)
		{
			_dbContext.Categories.Add(category);
			_dbContext.SaveChanges();

			return Ok();

		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		
		public ActionResult<List<Category>> GetAll()
		{
			var result = _dbContext.Categories.ToList();
			
			return Ok(result);

		}

		[HttpGet]
		[Route("Details")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult GetById(int id)
		{
			//var find = _dbContext.Categories.Find(id);
			var find = _dbContext.Categories.FirstOrDefault(x => x.Id == id);

			if(find == null)
			{
				return NotFound($"Category not found Id - {id}");
			}

			return Ok(find);
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public ActionResult Update([FromBody] Category category)
		{
			//var check = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
			
			//if(check == null)
			//{
			//	return NotFound($"Category not found Id - {category.Id}");
			//}

			_dbContext.Categories.Update(category);
			_dbContext.SaveChanges();

			return NoContent();
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]

		public ActionResult Delete(int id)
		{
			var find = _dbContext.Categories.FirstOrDefault(x =>x.Id == id);

			if(find == null)
			{
				return NotFound($"Category not found Id - {id}");
			}

			_dbContext.Categories.Remove(find);
			_dbContext.SaveChanges();

			return NoContent();
		}
    }
}
