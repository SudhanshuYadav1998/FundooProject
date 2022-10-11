using BusinessLayer.Interface;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollabBL:ICollabBL
    {
        private readonly ICollabRL collabRL;
		public CollabBL(ICollabRL collabRL)
		{
			this.collabRL = collabRL;
		}
        public CollabEntity CreateCollab(long NoteId, string email)
        {
			try
			{
				return collabRL.CreateCollab(NoteId, email); 
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
                return collabRL.GetAllCollab(UserId);
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
				return collabRL.DeleteCollab(collabid);
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
