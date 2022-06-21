using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadIt.Models;
using ReadIt.Models.DTOs;
using ReadIt.Models.Entities;

namespace ReadIt.Services
{
    public class BookService
    {
        private LibraryDbContext dbContext;

        public BookService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<BookDTO> GetAll()
        {
            return dbContext.Books
                .Include(p => p.User)
                .Select(p => ToDto(p))
                .ToList();
        }


        public void Add (Book book, User user)
        {
            book.User = user;

            dbContext.Books.Add(book);
            dbContext.SaveChanges();
        }

        public Book GetById(int id)
        {
            return dbContext.Books.FirstOrDefault(x => x.Id == id);
        }

        public void Edit(Book book)
        {
            Book dbBook = GetById(book.Id);

            dbBook.Title = book.Title;
            dbBook.Author = book.Author;
       

            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Book dbBook = GetById(id);
            dbContext.Books.Remove(dbBook);
            dbContext.SaveChanges();
        }

        private static BookDTO ToDto(Book b)
        {
            BookDTO book = new BookDTO();

            book.Id = b.Id;
            book.Title = b.Title;
            book.Author = b.Author;
            book.CreatedBy = $"{b.User.FirstName} {b.User.LastName}";
            book.UserEmail = b.User.Email;

            return book;
        }
    }
}

