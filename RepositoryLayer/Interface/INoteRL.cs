using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public Note GenerateNote(NotesModel noteModel, long userId);
        public List<Note> GetAllNotes(long UserId);
    }
}
