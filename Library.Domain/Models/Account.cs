using System;
using System.Collections.Generic;

namespace Library.Domain.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? Username { get; set; } = null!;

    public string? Password { get; set; } = null!;

    public string? Email { get; set; } = null!;
    public bool IsAdmin { get; set;  } = false;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
