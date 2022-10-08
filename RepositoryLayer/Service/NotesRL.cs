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

        public Note GenerateNote(NotesModel noteModel, long userId)
        {
            try
            {
                Note noteEntity = new Note();

                    noteEntity.UserId = userId;
                    noteEntity.Title = noteModel.Title;
                    noteEntity.Description = noteModel.Description;
                    noteEntity.Reminder = noteModel.Reminder;
                    noteEntity.Created = noteModel.Created;
                    noteEntity.Edited = noteModel.Edited;
                    noteEntity.Trash = noteModel.Trash;
                    noteEntity.Archieve = noteModel.Archieve;
                    noteEntity.Color = noteModel.Color;
                    noteEntity.Image = noteModel.Image;
                    noteEntity.IsPinned = noteModel.IsPinned;
                    fundoocontext.NoteTable.Add(noteEntity);
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
        public List<Note> GetAllNotes(long UserId)
        {
            try
            {
                return fundoocontext.NoteTable.Where(x => x.UserId == UserId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Note> DeleteApi(long noteid)
        {
            try
            {
                return fundoocontext.NoteTable.Where(x => x.NoteId == noteid).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
