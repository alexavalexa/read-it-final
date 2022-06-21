using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadIt.Models.DTOs;
using ReadIt.Models.Entities;
using ReadIt.Services;

namespace ReadIt.Controllers
{
    public class NoteController: Controller
    {
        private NoteService noteService;
        private UserManager<User> userManager;

        public NoteController(NoteService noteService, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.noteService = noteService;
        }

        public IActionResult Index()
        {
            List<NoteDTO> products = noteService.GetAll();

            return View(products);
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Note note)
        {
            User user = await userManager.GetUserAsync(User);
            note.User = user;
            noteService.Add(note, user);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Note note = noteService.GetById(id);

            return View(note);
        }

        [HttpPost]
        public IActionResult Edit(Note note)
        {
            noteService.Edit(note);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Note note = noteService.GetById(id);
            return View(note);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            noteService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
