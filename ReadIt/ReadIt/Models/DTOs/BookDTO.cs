using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadIt.Models.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string CreatedBy { get; set; }
        public string UserEmail { get; set; }
    }
}
