using System;
using System.Collections.Generic;

namespace MyEcommerceBook.Models;

public partial class AdminEmployee
{
    public int EmpId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateofBirth { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? PicturePath { get; set; }

    public virtual ICollection<AdminLogin> AdminLogins { get; } = new List<AdminLogin>();
}
