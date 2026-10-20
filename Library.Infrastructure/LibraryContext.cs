using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Domain.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookFile> BookFiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=Library.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "IX_Account_Email").IsUnique();

            entity.HasIndex(e => e.Username, "IX_Account_Username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Book>(entity =>
        {

            entity.ToTable("Book");

            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Isbn, "IX_Book_Isbn").IsUnique();
            entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd().UseAutoincrement();
        });

        modelBuilder.Entity<BookFile>(entity =>
        {
            entity.ToTable("BookFile");

            entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd().UseAutoincrement();

            entity.HasOne(f => f.Book)
                .WithMany(b => b.BookFiles)
                .HasForeignKey(f => f.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(f => new { f.BookId, f.Format }, "IX_BookFile_BookId_Format").IsUnique();
            entity.HasIndex(f => f.StorageKey, "IX_BookFile_StorageKey").IsUnique();

            entity.Property(f => f.Format).HasConversion<string>();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
