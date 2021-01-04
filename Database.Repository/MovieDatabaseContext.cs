using Database.Entities;
using Database.Entities.Interfaces;
using Database.General.Interfaces.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            Series              = new MovieDatabaseRepository<Serie>( _Series );

            Initialize();
        }

        private void Initialize()
        {
            // this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            var x = _Series.Include( o => o.Episodes ).FirstOrDefault();



            Console.WriteLine( x?.ToString() );

            if ( this._Movies.Any() )
            {
                return;
            }

            var lang = new Language(){ Name = "English" };
            var genre = new Genre(){ Name = "Detective" };

            //const string path = @"D:\Data\Downloads\Torrents\Agatha Christie - Poirot- The Complete Series";
            var series = new Serie(){ Title = "Poirot", Episodes = new List<Episode>() };
            _Series.Add( series );

            Regex regularExpression = new Regex( @"^Poirot S(\d+)e(\d+).+? (.*?)[\(-].*$", RegexOptions.Compiled );

            foreach ( var file in Directory.GetFiles( path ) )
            {
                string name = Path.GetFileNameWithoutExtension( file );
                var matches = regularExpression.Matches( name );

                Console.Write( $"{name,-75} => " );

                Match match = matches.First() as Match;

                int seasonIndex     = int.Parse( match.Groups[ 1 ].Value );
                int episodeIndex    = int.Parse( match.Groups[ 2 ].Value );

                string episodeName = match.Groups[ 3 ].Value.Trim();

                var episode = new Episode(){
                    Genres = new List<Genre>(){ genre },
                    IMDBIndex = null,
                    Language = lang,
                    Number = episodeIndex,
                    Season = seasonIndex,
                    ReleaseDate = DateTime.MinValue,
                    Title = episodeName,
                };

                series.Episodes.Add( episode );
                SaveChanges();

                episode.Filename            = $"Movies/{ episode.ID }.mp4";
                episode.ThumbnailFilename   = $"Thumbnails/{ episode.ID }.png";

                File.Move( file, @$"X:\LocalVidHost\BlazorApp\Storage\{ episode.Filename }" );
                SaveChanges();
            }

        }

        private DbSet<Movie> _Movies { get; set; }
        private DbSet<Genre> _Genres { get; set; }
        private DbSet<Language> _Languages { get; set; }
        private DbSet<MovieMetadata> _MovieMetadatas { get; set; }
        private DbSet<Subtitle> _Subtitles { get; set; }
        private DbSet<User> _Users { get; set; }
        private DbSet<Serie> _Series { get; set; }

        public IRepository<Movie> Movies { get; }
        public IRepository<Genre> Genres { get; }
        public IRepository<Language> Languages { get; }
        public IRepository<MovieMetadata> MovieMetadatas { get; }
        public IRepository<Subtitle> Subtitles { get; }
        public IRepository<User> Users { get; }
        public IRepository<Serie> Series { get; }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }
    }
}
