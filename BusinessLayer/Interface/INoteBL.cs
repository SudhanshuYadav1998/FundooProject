using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public bool GenerateNote(NotesModel noteModel, long userId);
        public List<Note> GetAllNotes(long UserId);
    }
}
