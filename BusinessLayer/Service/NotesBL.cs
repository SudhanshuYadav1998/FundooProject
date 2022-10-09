using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INoteBL
    {
        private readonly INoteRL notesRL;
        public NotesBL(INoteRL notesRL)
        {
            this.notesRL = notesRL;
        }

        public NotesEntity GenerateNote(NotesModel noteModel, long userId)
        {

            try
            {
                return notesRL.GenerateNote(noteModel, userId);
            }
            catch (SystemException)
            {

                throw;
            }

        }
        public List<NotesEntity> GetAllNotes(long UserId)
        {
            return notesRL.GetAllNotes(UserId);
        }
        public bool DeleteApi(long noteid)
        {
            try
            {
                return notesRL.DeleteApi(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesModel UpdateNote(NotesModel noteModel, long noteId)
        {
            try
            {
                return notesRL.UpdateNote(noteModel,noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Pinned(long noteid)
        {
            try
            {
                return notesRL.Pinned(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Trash(long noteid)
        {
            try
            {
                return notesRL.Trash(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Archieve(long noteid)
        {
            try
            {
                return notesRL.Archieve(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}
