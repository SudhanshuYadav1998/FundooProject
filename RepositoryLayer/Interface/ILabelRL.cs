using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity CreateLabel(LabelModel labelModel);
        public List<LabelEntity> GetAllLabel(long UserId);
        public bool DeleteLabel(long labelid);
    }
}
