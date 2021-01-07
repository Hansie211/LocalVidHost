using Packages.UniversalMovieDatabase.Interfaces;
using Packages.IMDBUpdate.Entities;
using Packages.IMDBUpdate.Extensions;
using Packcages.TSV.Generics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Packages.IMDBUpdate
{
    class Program
    {
        private static readonly string SourceDirectory = Path.Combine( Path.GetDirectoryName( System.Reflection.Assembly.GetEntryAssembly().Location ), "data" );

        private static List<TTSVItem> GetItems<TTSVItem>( string filepath ) where TTSVItem : new()
        {
            return GetItems<TTSVItem>( filepath, x => x );
        }

        private static List<TTSVItem> GetItems<TTSVItem>( string filepath, Func<IEnumerable<TTSVItem>, IEnumerable<TTSVItem>> extension ) where TTSVItem : new()
        {
            using ( var file = new TSVDatabaseFile<TTSVItem>( filepath ) )
            {
                return file.Extend( extension ).ToList(); // force-read
            }
        }

        private static IEnumerable<IEpisode> GetEpisodes( IEnumerable<IGenre> genreList, IEnumerable<TSVItems.Episode> rawEpisodes )
        {
            foreach ( var rawEpisode in rawEpisodes )
            {
                var episode = new Episode();
                episode.LoadFromTitle( rawEpisode.Title );
                episode.LoadGenresFromTitle( rawEpisode.Title, genreList );
                episode.SeasonNumber  = rawEpisode.SeasonNumber;
                episode.EpisodeNumber = rawEpisode.EpisodeNumber;

                yield return episode;
            }
        }

        private static IEnumerable<KeyValuePair<T, U>> BuildRelations<T, U, K>( IEnumerable<T> ts, IEnumerable<U> us, Func<T, K> GetTKey, Func<U, K> GetUKey )
        {
            var t_enumerator = ts.GetEnumerator();
            var u_enumerator = us.GetEnumerator();

            while ( t_enumerator.MoveNext() )
            {
                var t = t_enumerator.Current;

                bool found = false;
                while ( u_enumerator.MoveNext() )
                {
                    found = GetTKey( t ).Equals( GetUKey( u_enumerator.Current ) );
                    if ( found )
                        break;
                }

                if ( !found )
                {
                    // Console.Error.WriteLine( $"Could not find relation for key { GetTKey( t ) }." );
                    continue;
                }

                var u = u_enumerator.Current;

                yield return new KeyValuePair<T, U>( t, u );
            }
        }

        private static IEnumerable<KeyValuePair<T, U>> BuildRelations<T,U>( IEnumerable<T> ts, IEnumerable<U> us ) where T : TSVItems.TSVItem where U : TSVItems.TSVItem
        {
            return BuildRelations( ts, us, x => x.TitleConst, x => x.TitleConst );
        }

        private static IEnumerable<KeyValuePair<TSVItems.Serie, TSVItems.Title>> BuildSerieRelations( IEnumerable<TSVItems.Serie> series, IEnumerable<TSVItems.Title> titles )
        {
            return BuildRelations( series, titles );
        }

        private static IEnumerable<KeyValuePair<TSVItems.Episode, TSVItems.Title>> BuildEpisodeRelations( IEnumerable<TSVItems.Episode> episodes, IEnumerable<TSVItems.Title> titles )
        {
            return BuildRelations( episodes, titles );
        }

        private static IEnumerable<KeyValuePair<TSVItems.Rating, TSVItems.Title>> BuildRatingRelations( IEnumerable<TSVItems.Rating> ratings, IEnumerable<TSVItems.Title> titles )
        {
            return BuildRelations( ratings, titles );
        }

        /*
        short
        movie
        tvShort
        tvMovie
        tvSeries
        tvEpisode
        tvMiniSeries
        tvSpecial
        video
        videoGame
        audiobook
        radioSeries
        episode
        */

        private static readonly IEnumerable<string> MovieTypes = new[] { "movie", "tvMovie" };
        private static readonly IEnumerable<string> IgnoreMovieTypes = new[] { "short", "video", "videoGame", "audiobook", "radioSeries", "tvSpecial" };

        static void Main( string[] args )
        {

#if DEBUG
            Stopwatch sw = new Stopwatch();
            sw.Start();
#endif
            Console.WriteLine( "Start parsing files." );

            // Load all data into memory
            var titles      = GetItems<TSVItems.Title>( Path.Combine( SourceDirectory, "titles.tsv" ), ( source ) => source.Where( title => !IgnoreMovieTypes.Contains( title.TitleType ) ) ).OrderBy( o => o.TitleConst ).ToList();
            var ratings     = GetItems<TSVItems.Rating>( Path.Combine( SourceDirectory, "ratings.tsv" ) ).OrderBy( o => o.TitleConst ).ToList();
            var series      = GetItems<TSVItems.Episode>( Path.Combine( SourceDirectory, "episodes.tsv" ) ).GroupBy( o => o.ParentTitleConst ).Select( o => new TSVItems.Serie(){ TitleConst = o.Key, Episodes = o } ).OrderBy( o => o.TitleConst ).ToList();

            Console.WriteLine( "Done" );
#if DEBUG
            sw.Stop();
            TimeSpan timeSpan = TimeSpan.FromMilliseconds( sw.ElapsedMilliseconds );
            Console.WriteLine( $"Files parsed in {timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}:{timeSpan.Milliseconds:D3}ms" );
#endif

            var serieRelations      = new Dictionary<TSVItems.Serie, TSVItems.Title>( BuildSerieRelations( series, titles ) );
            var ratingRelations     = new Dictionary<TSVItems.Rating, TSVItems.Title>( BuildRatingRelations( ratings, titles ) ).Invert();
            var episodeRelations    = new Dictionary<TSVItems.Episode, TSVItems.Title>( BuildEpisodeRelations( series.SelectMany( x => x.Episodes ).OrderBy( x => x.TitleConst ), titles ) );

            titles.AsParallel().ForAll( title => title.Rating = ratingRelations.GetValueOrDefault( title ) );
            series.SelectMany( x => x.Episodes ).AsParallel().ForAll( episode => episode.Title = episodeRelations[ episode ] );

            using ( var ctx = new Context() )
            {

                // Clear the database
                //ctx.Database.EnsureDeleted();
                ctx.Database.EnsureCreated();

                foreach ( var genreName in titles.Where( o => o.Genres != null ).SelectMany( o => o.Genres ).Distinct() )
                {
                    ctx.Genres.Add( new Genre() { Name = genreName } );
                }

                foreach ( var movieTitle in titles.Where( title => MovieTypes.Contains( title.TitleType ) ) )
                {
                    var movie = new Movie();
                    movie.LoadFromTitle( movieTitle );
                    movie.LoadGenresFromTitle( movieTitle, ctx.Genres );

                    ctx.Movies.Add( movie );
                }

                foreach ( var tsvSerie in series )
                {
                    var serieTitle = serieRelations[ tsvSerie ];
                    var serie = new Serie(){
                        IMDBIdentiefier = tsvSerie.TitleConst,
                        Rating      = serieTitle.Rating.AvgRating,
                        Title       = serieTitle.DisplayTitle,
                        Episodes    = GetEpisodes( ctx.Genres, tsvSerie.Episodes ).ToList(),
                    };

                    ctx.Series.Add( serie );
                }

                ctx.SaveChanges();
            }
        }
    }
}
