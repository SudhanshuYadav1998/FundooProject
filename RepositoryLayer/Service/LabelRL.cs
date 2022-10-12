using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL:ILabelRL
    {
        private readonly Fundoocontext fundoocontext;

        public LabelRL(Fundoocontext fundoocontext)
        {
            this.fundoocontext = fundoocontext;
        }

       
        public LabelEntity CreateLabel(LabelModel labelModel)
        {
            try
            {
                var noteCheck = fundoocontext.NotesTable.Where(x => x.NoteId == labelModel.NoteId).FirstOrDefault();

                
                if (noteCheck != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.NoteId = noteCheck.NoteId;
                    labelEntity.LabelName = labelModel.LabelName;
                    labelEntity.UserId= noteCheck.UserId;
                    fundoocontext.Add(labelEntity);
                    fundoocontext.SaveChanges();
                    return labelEntity;
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
        public List<LabelEntity> GetAllLabel(long UserId)
        {
            try
            {
                return fundoocontext.LabelTable.Where(x => x.UserId == UserId).ToList();
               
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteLabel(long labelid)
        {
            try
            {
                var LabelCheck = fundoocontext.LabelTable.Where(x => x.LabelId == labelid).FirstOrDefault();
                this.fundoocontext.LabelTable.Remove(LabelCheck);

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
