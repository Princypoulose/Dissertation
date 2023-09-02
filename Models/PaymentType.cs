using System;
using System.Collections.Generic;

namespace MyEcommerceBook.Models;

public partial class PaymentType
{
    public int PayTypeId { get; set; }

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Payment> Payments { get; } = new List<Payment>();
}
