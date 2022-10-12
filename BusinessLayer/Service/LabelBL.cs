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
    public class LabelBL:ILabelBL
    {
        private readonly ILabelRL _labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            _labelRL = labelRL;
        }
        public LabelEntity CreateLabel(LabelModel labelModel)
        {
            try
            {
                return _labelRL.CreateLabel(labelModel);
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
                return _labelRL.GetAllLabel(UserId);
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
                return (_labelRL.DeleteLabel(labelid));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateLabel(LabelModel labelModel, long labelid)
        {
            try
            {
                return _labelRL.UpdateLabel(labelModel, labelid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
