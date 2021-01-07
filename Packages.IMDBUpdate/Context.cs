using Microsoft.EntityFrameworkCore;
using Packages.IMDBUpdate.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate
{
    public class Context : DbContext
    {
        protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlite( @"Data Source=UniversalMovieDatabase.db" );
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
