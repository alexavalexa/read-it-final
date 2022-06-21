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
    public class NoteService
    {
        private LibraryDbContext dbContext;

        public NoteService(LibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<NoteDTO> GetAll()
        {
            return dbContext.Notes
                .Include(p => p.User)
                .Select(p => ToDto(p))
                .ToList();
        }
        public void Add(Note note, User user)
        {
            note.User = user;

            dbContext.Notes.Add(note);
            dbContext.SaveChanges();
        }

        public Note GetById(int id)
        {
            return dbContext.Notes.FirstOrDefault(x => x.Id == id);
        }

        public void Edit(Note note)
        {
            Note dbNote = GetById(note.Id);

            dbNote.Title = note.Title;
            dbNote.Text = note.Text;


            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Note dbNote = GetById(id);
            dbContext.Notes.Remove(dbNote);
            dbContext.SaveChanges();
        }

        private static NoteDTO ToDto(Note n)
        {
            NoteDTO note = new NoteDTO();

            note.Id = n.Id;
            note.Title = n.Title;
            note.Text = n.Text;
            note.CreatedBy = $"{n.User.FirstName} {n.User.LastName}";
            note.UserEmail = n.User.Email;

            return note;
        }
    }
}
