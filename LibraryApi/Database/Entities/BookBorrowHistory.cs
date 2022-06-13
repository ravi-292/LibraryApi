using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Database.Entities
{
    public class BookBorrowHistory
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public DateTime BorrowedOn { get; set; }

        public DateTime? ReturnedOn { get; set; }

        [ForeignKey(nameof(BookId))]
        public virtual Book Book { get; set; }
    }
}
