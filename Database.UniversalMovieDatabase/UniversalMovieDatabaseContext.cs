using Microsoft.EntityFrameworkCore;
using Packages.UniversalMovieDatabase.Interfaces;
using System;

namespace Database.UniversalMovieDatabase
{
    public class UniversalMovieDatabaseContext : DbContext
    {
        public DbSet<IMovie> Movies { get; set; }
        public DbSet<IEpisode> Episodes { get; set; }
        public DbSet<ISerie> Series { get; set; }
        public DbSet<IGenre> Genres { get; set; }
    }
}
