using Microsoft.EntityFrameworkCore;
using Packages.UniversalMovieDatabase.Interfaces.Concrete;
using System;

namespace Database.UniversalMovieDatabase
{
    public class UniversalMovieDatabaseContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
