using LibraryApi.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Database.Context
{
    public static class LibraryDbContextSeed
    {
        public static async Task SeedDefaultData(LibraryDbContext context)
        {
            using (StreamReader r = new("Database/DefaultData/authors.json"))
            {
                string json = r.ReadToEnd();
                AuthorModel authorJsonObj = JsonConvert.DeserializeObject<AuthorModel>(json);
                var existingAuthors = await context.Authors.ToListAsync();
                var newAuthors = authorJsonObj.Authors.GroupBy(e => e.Name).Select(e => e.First()).Where(e => existingAuthors.All(c => c.Name != e.Name && c.ImageUrl != e.ImageUrl)).ToList();
                if (newAuthors.Count > 0)
                {
                    foreach (var item in newAuthors)
                    {
                        context.Authors.Add(new Author { Name = item.Name, ImageUrl = item.ImageUrl, CreatedOn = DateTime.Now });
                    }
                    await context.SaveChangesAsync();
                }

            }



            using (StreamReader r = new("Database/DefaultData/categories.json"))
            {
                string json = r.ReadToEnd();
                CategoryModel categoryJsonObj = JsonConvert.DeserializeObject<CategoryModel>(json);
                var existingCategories = await context.Categories.ToListAsync();
                var newCategories = categoryJsonObj.Categories.GroupBy(e => e.Name).Select(e => e.First()).Where(e => existingCategories.All(c => c.Name != e.Name)).ToList();
                if (newCategories.Count > 0)
                {
                    foreach (var item in newCategories)
                    {
                        context.Categories.Add(new Category { Name = item.Name, CreatedOn = DateTime.Now });
                    }
                    await context.SaveChangesAsync();
                }
            }

            using (StreamReader r = new("Database/DefaultData/books.json"))
            {
                string json = r.ReadToEnd();
                BookModel bookJsonObj = JsonConvert.DeserializeObject<BookModel>(json);
                var existingBooks = await context.Books.ToListAsync();
                var newBooks = bookJsonObj.Books.Where(e => existingBooks.All(c => c.Title != e.Title)).ToList();
                if (newBooks.Count > 0)
                {
                    foreach (var item in newBooks)
                    {
                        var book = new Book { Title = item.Title, Description = item.Description,ImageUrl = item.ImageUrl, CreatedOn = DateTime.Now };
                        Random random = new();
                        var skip = (int)(random.NextDouble() * context.Authors.Count());
                        var author = context.Authors.OrderBy(o => o.Id).Skip(skip).Take(1).First();
                        if (author != null)
                        {
                            book.Author = author;
                        }
                        context.Books.Add(book);
                    }
                    await context.SaveChangesAsync();
                }
            }
        }

        private class AuthorModel
        {
            public List<Author> Authors { get; set; }
        }

        private class BookModel
        {
            public List<Book> Books { get; set; }
        }

        private class CategoryModel
        {
            public List<Category> Categories { get; set; }
        }
    }
}
