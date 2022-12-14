using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NoteId { get; set; }
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
        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}
