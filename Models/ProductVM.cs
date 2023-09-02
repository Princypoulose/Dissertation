using System.ComponentModel.DataAnnotations;

namespace MyEcommerceBook.Models
{
    public class ProductVM
    {
        public int ProductID { get; set; }
        [Required, Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required, Display(Name = "Supplier")]
        public int SupplierId { get; set; }
        [Required, Display(Name = "Category")]

        //public int SupplierId { get; set; }

        public int CategoryId { get; set; }
        [Display(Name = "SubCategory")]
        public Nullable<int> SubCategoryId { get; set; }
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }
        [Required, Display(Name = "Previous Price")]
        public Nullable<decimal> OldPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }
        [Display(Name = "Stock")]
        public Nullable<int> UnitInStock { get; set; }
        [Display(Name = "Available?")]
        public Nullable<bool> ProductAvailable { get; set; }
        [Display(Name = "Description")]
        public string ShortDescription { get; set; }
       // [Display(Name = "Picture")]
        public string PicturePath { get; set; }
        public IFormFile Picture { get; set; }
       // [Required(ErrorMessage="Please select image"), Display(Name = "Image")]
        public string Note { get; set; }
    
}
}
