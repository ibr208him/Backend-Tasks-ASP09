using System.ComponentModel.DataAnnotations;

namespace CRUD2_API.DTOs.Product
{
    public class CreateProductDTO
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        [MinLength(3, ErrorMessage = "Length should be greater than 3 chars")]
        [MaxLength(30, ErrorMessage = "Length should be at most 30 chars")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product price is required")]
        [Range(20,3000,ErrorMessage ="Price shall be between 20 and 3000")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        [MinLength(3, ErrorMessage = "Length should be greater than 10 chars")]
        public string Description { get; set; }
    }
}
