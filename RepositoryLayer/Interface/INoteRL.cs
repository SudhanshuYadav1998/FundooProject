using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NotesEntity GenerateNote(NotesModel noteModel, long userId);
        public List<NotesEntity> GetAllNotes(long UserId);
        public bool DeleteApi(long noteid);
        public bool UpdateNote(NotesModel noteModel, long noteId);
        public bool Pinned(long noteid);
        public bool Trash(long noteid);

        public bool Archieve(long noteid);
        public bool BGImage(long NotesId, IFormFile image);
        public bool AddColor(long NotesId, string color);
    }
}
