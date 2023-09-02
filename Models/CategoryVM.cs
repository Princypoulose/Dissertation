﻿using System.ComponentModel.DataAnnotations;

namespace MyEcommerceBook.Models
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
