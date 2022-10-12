using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity CreateLabel(LabelModel labelModel);
        public List<LabelEntity> GetAllLabel(long UserId);
        public bool DeleteLabel(long labelid);
        public bool UpdateLabel(LabelModel labelModel, long labelid);
    }
}
