using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryDomain.Models;

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

    public virtual DbSet<Room> Rooms { get; set; }

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
            entity.HasKey(e => e.Isbn);

            entity.ToTable("Book");

            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.BorrowedByNavigation).WithMany(p => p.Books).HasForeignKey(d => d.BorrowedBy);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("Room");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bookedby).HasColumnName("BOOKEDBY");
            entity.Property(e => e.Type).HasColumnName("TYPE");

            entity.HasOne(d => d.BookedbyNavigation).WithMany(p => p.Rooms).HasForeignKey(d => d.Bookedby);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
