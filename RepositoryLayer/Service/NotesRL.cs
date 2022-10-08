using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RepositoryLayer.Service
{
    public class NotesRL : INoteRL
    {
        private readonly Fundoocontext fundoocontext;
        
        public NotesRL(Fundoocontext fundoocontext)
        {
            this.fundoocontext = fundoocontext;
            
        }

        public NotesEntity GenerateNote(NotesModel noteModel, long userId)
        {
            try
            {
                NotesEntity noteEntity = new NotesEntity
                {
                    UserId = userId,
                    Title = noteModel.Title,
                    Description = noteModel.Description,
                    Reminder = noteModel.Reminder,
                    Created = noteModel.Created,
                    Edited = noteModel.Edited,
                    IsPinned = noteModel.IsPinned,
                    BgImage = noteModel.BgImage,
                    Archieve=noteModel.Archieve,
                    Trash=noteModel.Trash,
                };
                fundoocontext.NotesTable.Add(noteEntity);
                   int result=fundoocontext.SaveChanges();
                if(result!=0)
                { 
                    return noteEntity;
                }
                else
                { return null; }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<NotesEntity> GetAllNotes(long UserId)
        {
            try
            {
                return fundoocontext.NotesTable.Where(x => x.UserId == UserId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteApi(long noteid)
        {
            try
            {
                var noteCheck= fundoocontext.NotesTable.Where(x => x.NoteId == noteid).FirstOrDefault();
                this.fundoocontext.NotesTable.Remove(noteCheck);
                int result = this.fundoocontext.SaveChanges();
                if (result!=0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity UpdateNote(NotesModel noteModel, long userId)
        {
            try
            {
                NotesEntity noteEntity = new NotesEntity
                {
                    UserId = userId,
                    Title = noteModel.Title,
                    Description = noteModel.Description,
                    Reminder = noteModel.Reminder,
                    Created = noteModel.Created,
                    Edited = noteModel.Edited
                };
                fundoocontext.NotesTable.Add(noteEntity);
                int result = fundoocontext.SaveChanges();
                if (result != 0)
                {
                    return noteEntity;
                }
                else
                { return null; }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
