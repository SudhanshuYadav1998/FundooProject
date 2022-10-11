using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollabRL : ICollabRL
    {
        private readonly Fundoocontext fundoocontext;
        
        public CollabRL(Fundoocontext fundoocontext)
        {
            this.fundoocontext = fundoocontext;
            
        }

        public CollabEntity CreateCollab(long NoteId, string email)
        {
            try
            {
                var noteCheck = fundoocontext.NotesTable.Where(x => x.NoteId == NoteId).FirstOrDefault();
                var emailCheck = fundoocontext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if (noteCheck != null && emailCheck != null)
                {
                    CollabEntity collabEntity = new CollabEntity() {

                    CollabEmail = emailCheck.Email,
                    NoteId = noteCheck.NoteId,
                    UserId = emailCheck.UserId,
                    };
                    fundoocontext.Add(collabEntity);
                    fundoocontext.SaveChanges();
                    return collabEntity;
                        
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CollabEntity> GetAllCollab(long UserId)
        {
            try
            {
                return fundoocontext.CollabTable.Where(x => x.UserId == UserId).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteCollab(long collabid)
        {
            try
            {
                var collabCheck = fundoocontext.CollabTable.Where(x => x.CollabId == collabid).FirstOrDefault();
                this.fundoocontext.CollabTable.Remove(collabCheck);

                int result = this.fundoocontext.SaveChanges();
                if (result != 0)
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
    }
}
