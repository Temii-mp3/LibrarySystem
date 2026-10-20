using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Models
{
    public class BookFile
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book? Book { get; set; }
        public string StorageKey { get; set; } = null!;
        public BookFormat Format { get; set; }
        public long SizeBytes { get; set; }
        public string ContentHash { get; set; } = null!;
        public DateTime UploadedAt { get; set; }
    }
}
