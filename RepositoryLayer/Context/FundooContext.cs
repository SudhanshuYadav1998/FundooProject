using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{

    public class Fundoocontext : DbContext
    {
            public Fundoocontext(DbContextOptions options)
                : base(options)
            {
            }
            public DbSet<UserEntity> UserTable { get; set; }
           
            public DbSet<NotesEntity> NotesTable { get; set; }
            public DbSet<CollabEntity> CollabTable { get; set; }
            public DbSet<LabelEntity> LabelTable { get; set; }



    }
}

