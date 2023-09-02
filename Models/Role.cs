using System;
using System.Collections.Generic;

namespace MyEcommerceBook.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<AdminLogin> AdminLogins { get; } = new List<AdminLogin>();
}
