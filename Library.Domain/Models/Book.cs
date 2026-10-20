using System;
using System.Collections.Generic;

namespace Library.Domain.Models;

public partial class Book
{
    public int Id { get; set; }
    public string? Isbn { get; set; }

    public string? Author { get; set; }

    public string? Name { get; set; }
    public ICollection<BookFile> BookFiles { get; set; } = new List<BookFile>();


    public override string ToString()
    {
        return $"ISBN: {Isbn}\nAuthor{Author}\n{Name}\n";
    }
}
