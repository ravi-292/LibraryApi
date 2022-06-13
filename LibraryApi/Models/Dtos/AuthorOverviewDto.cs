using System;

namespace LibraryApi.Models.Dtos
{
    public class AuthorOverviewDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Books { get; set; }
    }
}
