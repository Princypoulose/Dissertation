using System;
using System.Collections.Generic;

namespace MyEcommerceBook.Models;

public partial class AdminLogin
{
    public int LoginId { get; set; }

    public int EmpId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int? RoleType { get; set; }

    public string? Notes { get; set; }

    public int? RoleRoleId { get; set; }

    public virtual AdminEmployee Emp { get; set; } = null!;

    public virtual Role? RoleRole { get; set; }
}
