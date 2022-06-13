using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Database.Entities
{
    [Index(nameof(Title))]
    public class Book
    {
        public Book()
        {
            BookCategories = new HashSet<BookCategory>();

            BookBorrowHistories = new HashSet<BookBorrowHistory>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Guid? AuthorId { get; set; }

        [DatabaseGenerated(databaseGeneratedOption: DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<BookCategory> BookCategories { get; set; }

        public virtual ICollection<BookBorrowHistory> BookBorrowHistories { get; set; }
    }
}
