using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity CreateCollab(long NoteId, string email);
        public List<CollabEntity> GetAllCollab(long UserId);
        public bool DeleteCollab(long collabid);
    }
}
