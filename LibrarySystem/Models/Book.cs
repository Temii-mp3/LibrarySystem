using System;
using System.Collections.Generic;

namespace LibrarySystem.Models;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string? Author { get; set; }

    public string? Name { get; set; }

    public int? BorrowedBy { get; set; }

    public virtual Account? BorrowedByNavigation { get; set; }


    public override string ToString()
    {
        return $"ISBN: {Isbn}\nAuthor{Author}\n{Name}\n";
    }
}
