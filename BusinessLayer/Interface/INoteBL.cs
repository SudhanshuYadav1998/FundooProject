using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NotesEntity GenerateNote(NotesModel noteModel, long userId);
        public List<NotesEntity> GetAllNotes(long UserId);
        public bool DeleteApi(long noteid);

    }
}
