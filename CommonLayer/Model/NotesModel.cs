using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
        
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public bool IsPinned { get; set; }
        public bool Trash { get; set; }
        public bool Archieve { get; set; }
        public string Color { get; set; }
        public string BgImage { get; set; }
    }
}
