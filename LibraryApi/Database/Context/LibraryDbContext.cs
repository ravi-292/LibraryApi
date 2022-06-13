using LibraryApi.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryApi.Database.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookBorrowHistory>().HasKey(e => new { e.BookId, e.BorrowedOn });
            modelBuilder.Entity<Author>().Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Book>().Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Category>().Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");
            //modelBuilder.Entity<BookBorrowHistory>().Property(e => e.BorrowedOn).HasDefaultValueSql<DateTime>("GETDATE()");
            modelBuilder.Entity<BookCategory>().HasKey(e => new { e.BookId, e.CategoryId });
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    if (!builder.IsConfigured)
        //    {
        //        builder.UseInMemoryDatabase("LibraryDb");
        //    }

        //    base.OnConfiguring(builder);
        //}

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookBorrowHistory> BookBorrowHistories { get; set; }

        public DbSet<BookCategory> BookCategories { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
