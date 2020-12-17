using Database.Entities;
using Database.Entities.Interfaces;
using Database.General.Interfaces.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class MovieDatabaseContext : DbContext, IRepositoryContext
    {
        public MovieDatabaseContext( DbContextOptions options ) : base( options )
        {
            Movies              = new MovieDatabaseRepository<Movie>( _Movies );
            Genres              = new MovieDatabaseRepository<Genre>( _Genres );
            Languages           = new MovieDatabaseRepository<Language>( _Languages );
            MovieMetadatas      = new MovieDatabaseRepository<MovieMetadata>( _MovieMetadatas );
            Subtitles           = new MovieDatabaseRepository<Subtitle>( _Subtitles );
            Users               = new MovieDatabaseRepository<User>( _Users );

            Initialize();
        }

        private void Initialize()
        {

            this.Database.EnsureCreated();

            if ( this._Movies.Any() )
            {
                return;
            }

            var lang = new Language(){ Name = "English" };
            _Languages.Add( lang );


            _Movies.Add( new Movie() {
                Filename = "temp.mp4",
                Genres = new List<Genre>(),
                IMDBIndex = "www.imdb.com",
                Language = lang,
                ReleaseDate = DateTime.Now.AddDays( -100 ),
                Title = "A Test Movie",
            } );

            SaveChanges();
        }

        private DbSet<Movie> _Movies { get; set; }
        private DbSet<Genre> _Genres { get; set; }
        private DbSet<Language> _Languages { get; set; }
        private DbSet<MovieMetadata> _MovieMetadatas { get; set; }
        private DbSet<Subtitle> _Subtitles { get; set; }
        private DbSet<User> _Users { get; set; }

        public IRepository<Movie> Movies { get; }
        public IRepository<Genre> Genres { get; }
        public IRepository<Language> Languages { get; }
        public IRepository<MovieMetadata> MovieMetadatas { get; }
        public IRepository<Subtitle> Subtitles { get; }
        public IRepository<User> Users { get; }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }
    }
}
