using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadIt.Models.Entities;

namespace ReadIt.Models
{
    public class LibraryDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Note> Notes { get; set; }
        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
