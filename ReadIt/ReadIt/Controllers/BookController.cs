using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ReadIt.Models;


using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadIt.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using ReadIt.Models.Entities;
using ReadIt.Services;

namespace ReadIt
{
    public class BookController : Controller
    {
        private BookService bookService;
        private UserManager<User> userManager;

        public BookController(BookService bookService, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.bookService = bookService;
        }

        public IActionResult Index()
        {
            List<BookDTO> products = bookService.GetAll();

            return View(products);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            User user = await userManager.GetUserAsync(User);
            book.User = user;
            bookService.Add(book, user);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Book book = bookService.GetById(id);

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            bookService.Edit(book);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Book book = bookService.GetById(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            bookService.Delete(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
