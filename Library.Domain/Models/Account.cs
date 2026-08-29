using System;
using System.Collections.Generic;

namespace LibraryDomain.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
