using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyEcommerceBook.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }
   
    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<GenPromoRight> GenPromoRights { get; } = new List<GenPromoRight>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public virtual ICollection<SubCategory> SubCategories { get; } = new List<SubCategory>();
}
