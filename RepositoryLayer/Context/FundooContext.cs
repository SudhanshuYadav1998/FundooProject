using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{

    public class fundoocontext : DbContext
        {
            public fundoocontext(DbContextOptions options)
                : base(options)
            {
            }
            public DbSet<UserEntity> UserTable { get; set; }
            public DbSet<NoteEntity> NoteTable { get; set; }
        }
    }

