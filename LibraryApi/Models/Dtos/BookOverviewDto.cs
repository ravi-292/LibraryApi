using System;

namespace LibraryApi.Models.Dtos
{
    public class BookOverviewDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string CreatedOn { get; set; }

        public AuthorOverviewDto Author { get; set; }

        public bool IsAvailable { get; set; }
    }
}
