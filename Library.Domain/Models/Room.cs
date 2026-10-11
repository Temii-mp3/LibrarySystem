using System;
using System.Collections.Generic;

namespace Library.Domain.Models;

public partial class Room
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? Bookedby { get; set; }

    public virtual Account? BookedbyNavigation { get; set; }
}
