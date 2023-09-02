using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEcommerceBook.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? Name { get; set; }

    public int SupplierId { get; set; }

    public int CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal? OldPrice { get; set; }

    public decimal? Discount { get; set; }

    public int? UnitInStock { get; set; }

    public bool? ProductAvailable { get; set; }

    public string? ShortDescription { get; set; }

    public string? PicturePath { get; set; }

    public string? Note { get; set; }
    [NotMapped]
    public IFormFile Picture { get; set; } 

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<RecentlyView> RecentlyViews { get; } = new List<RecentlyView>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual SubCategory? SubCategory { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;

    public virtual ICollection<Wishlist> Wishlists { get; } = new List<Wishlist>();
}
