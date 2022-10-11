using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollabRL
    {
        public CollabEntity CreateCollab(long NoteId, string email);
        public List<CollabEntity> GetAllCollab(long UserId);
        public bool DeleteCollab(long collabid);
    }
}
