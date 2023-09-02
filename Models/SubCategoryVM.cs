using System.ComponentModel.DataAnnotations;

namespace MyEcommerceBook.Models
{
    public class SubCategoryVM
    {
        public int SubCategoryId { get; set; }
        [Required, Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Required, Display(Name = "Name")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

       // public virtual Category Category { get; set; } = null!;
    }
}
