using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadIt.Models.DTOs
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }

        public string CreatedBy { get; set; }
        public string UserEmail { get; set; }
    }
}
