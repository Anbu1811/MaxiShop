using System.ComponentModel.DataAnnotations;

namespace Maxishop.Frontent.DTO
{
	public class ProductDTO
	{

        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public int ExtablishYear { get; set; }

    }
}
