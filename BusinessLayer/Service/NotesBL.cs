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
    public class NotesBL:INoteBL
    {
        private readonly INoteRL notesRL;
        public NotesBL(INoteRL notesRL)
        {
            this.notesRL = notesRL;
        }
       
        public Note GenerateNote(NotesModel noteModel, long userId)
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
        public List<Note> GetAllNotes(long UserId)
        {
            return notesRL.GetAllNotes(UserId);
        }
    }
}
