using ReadIt.Models;
using ReadIt.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using ReadIt.Models.Entities;
using ReadIt.Models.DTOs;

namespace ReadIt.Tests
{
    public class Test
    {
        private LibraryDbContext context;
        private BookService bookService;
        private NoteService noteService;
        private User user;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            this.context = new LibraryDbContext(options);
            bookService = new BookService(this.context);
            noteService = new NoteService(this.context);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

        [Test]
        public void TestGetById()
        {
            Book Book = CreateBook(1, "Name");

            bookService.Add(Book, user);

            Book dbBook = bookService.GetById(1);

            Assert.AreEqual(dbBook.Title, "Name");

            Note Note = CreateNote(1, "NAME");

            noteService.Add(Note, user);

            Note dbNote = noteService.GetById(1);

            Assert.AreEqual(dbNote.Title, "NAME");
        }

        [Test]
        public void TestCreate()
        {
            Book Book = CreateBook(1, "Name");

            bookService.Add(Book, user);

            Book dbBook = context.Books.FirstOrDefault();

            Assert.NotNull(dbBook);

            Note Note = CreateNote(1, "NAME");

            noteService.Add(Note, user);

            Note dbNote = context.Notes.FirstOrDefault();

            Assert.NotNull(dbNote);
        }

        [Test]
        public void TestEdit()
        {
            BookService postService = new BookService(this.context);

            Book Book = new Book();
            Book.Id = 1;
            Book.Title = "Book Name";

            bookService.Add(Book, user);

            Book editBook = new Book();

            editBook.Id = 1;
            editBook.Title = "asd";

            postService.Edit(editBook);

            Book dbBook = context.Books.FirstOrDefault(x => x.Id == 1);

            Assert.NotNull(dbBook);
            Assert.AreEqual(dbBook.Title, "asd");
        }

        [Test]
        public void TestDelete()
        {
            BookService bookService = new BookService(this.context);

            Book Book = new Book();
            Book.Id = 1;
            Book.Title = "Book Name";

            bookService.Add(Book, user);

            bookService.Delete(1);

            Book dbProduct = context.Books.FirstOrDefault(x => x.Id == 1);
            Assert.Null(dbProduct);

            NoteService noteService = new NoteService(this.context);

            Note Note = new Note();
            Note.Id = 2;
            Note.Title = "Book Name";

            noteService.Add(Note, user);

            noteService.Delete(2);

            Note dbNote = context.Notes.FirstOrDefault(x => x.Id == 2);
            Assert.Null(dbNote);
        }

        private Book CreateBook(int id, string title)
        {
            Book Book = new Book();
            Book.Id = id;
            Book.Title = title;

            return Book;
        }

        private Note CreateNote(int id, string title)
        {
            Note Note = new Note();
            Note.Id = id;
            Note.Title = title;

            return Note;
        }

    }
}
