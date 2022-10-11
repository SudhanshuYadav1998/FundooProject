using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
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
         IConfiguration _configure;
        
        
        public NotesRL(Fundoocontext fundoocontext,IConfiguration configure)
        {
            this.fundoocontext = fundoocontext;
           _configure = configure;
        }

        public NotesEntity GenerateNote(NotesModel noteModel,long userId)
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
                    Color=noteModel.Color,
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
        public bool UpdateNote(NotesModel noteModel, long noteId)
        {
            try
            {
                var update = fundoocontext.NotesTable.Where(x => x.NoteId == noteId).FirstOrDefault();
                if (update != null&& update.NoteId==noteId)
                
                    {
                    update.Title = noteModel.Title;
                    update.Description = noteModel.Description;
                    update.Reminder = noteModel.Reminder;
                    update.Edited = noteModel.Edited;
                    update.Color = noteModel.Color;
                    }

                    int result = fundoocontext.SaveChanges();
                    if (result != 0)
                    {
                        return true;
                    }
                else 
                {
                    return false;
                }
            }
            catch(Exception)
            {
                throw;
            }
           
        }
        public bool Pinned(long noteid)
        {
            try
            {
                var pinned = fundoocontext.NotesTable.Where(x => x.NoteId == noteid).FirstOrDefault();
                if (pinned.IsPinned==true)
                {
                    pinned.IsPinned = false;
                    fundoocontext.SaveChanges();
                    return false;
                }
                else
                {
                    pinned.IsPinned = false;
                    fundoocontext.SaveChanges();
                    return true;
                }
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
                var trash = fundoocontext.NotesTable.Where(x => x.NoteId == noteid).FirstOrDefault();
                if (trash.Trash == true)
                {
                    trash.Trash = false;
                    fundoocontext.SaveChanges();
                    return false;
                }
                else
                {
                    trash.Trash = true;
                    fundoocontext.SaveChanges();
                    return true;
                }
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
                var archieve = fundoocontext.NotesTable.Where(x => x.NoteId == noteid).FirstOrDefault();
                if (archieve.Archieve == true)
                {
                    archieve.Archieve = false;
                    fundoocontext.SaveChanges();

                    return false;
                }
                else
                {
                    archieve.Archieve = true;
                    fundoocontext.SaveChanges();
                    return true;
                }
            }

            catch (Exception)
            {

                throw;
            }
        }
        public bool AddColor(long NotesId, string color)
        {
            try
            {
                var addcolor = this.fundoocontext.NotesTable.Where(x => x.NoteId == NotesId).FirstOrDefault();
                if (addcolor.Color == "string")
                {
                    addcolor.Color = color;
                    this.fundoocontext.SaveChanges();
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
        public bool BGImage(long NotesId, IFormFile image)
        {
            try
            {
                if (NotesId != 0)
                {
                    var notes = this.fundoocontext.NotesTable.Where(x => x.NoteId == NotesId).FirstOrDefault();
                    if (notes != null)
                    {
                        Account account = new Account
                        (
                        _configure["Cloudinary:cloud_name"],
                        _configure["Cloudinary:api_key"],
                        _configure["Cloudinary:api_secret"]
                        );
                        var path = image.OpenReadStream();
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(image.FileName, path)
                        };
                        var uploadResult = cloudinary.Upload(uploadParams);
                        fundoocontext.NotesTable.Attach(notes);
                        notes.BgImage = uploadResult.Url.ToString();
                        fundoocontext.SaveChanges();
                        return true;

                    }
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

