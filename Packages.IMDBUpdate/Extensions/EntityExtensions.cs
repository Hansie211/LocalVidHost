using Packages.UniversalMovieDatabase.Interfaces;
using Packages.IMDBUpdate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Packages.IMDBUpdate.Extensions
{
    public static class EntityExtensions
    {
        public static IEnumerable<T> Extend<T>( this IEnumerable<T> list, Func<IEnumerable<T>, IEnumerable<T>> extention )
        {
            return extention( list );
        }

        public static Dictionary<TKey, TValue> Invert<TKey, TValue>( this Dictionary<TValue, TKey> dict )
        {
            var result = new Dictionary<TKey,TValue>( dict.Count );

            foreach ( var pair in dict )
            {
                result.Add( pair.Value, pair.Key );
            }

            return result;
        }


        public static TValue GetValueOrDefault<TKey, TValue>( this Dictionary<TKey, TValue> dict, TKey key )
        {
            if ( !dict.TryGetValue(key, out TValue value ) )
                return default(TValue);

            return value;
        }

        public static void LoadFromTitle( this Movie movie, TSVItems.Title imdbTitle )
        {
            movie.IMDBIdentiefier   = imdbTitle.TitleConst;
            movie.IsAdult           = imdbTitle.IsAdult;
            movie.Rating            = imdbTitle.Rating?.AvgRating;
            movie.ReleaseYear       = imdbTitle.StartYear;
            movie.RuntimeMinutes    = imdbTitle.RuntimeMinutes;
            movie.Title             = imdbTitle.DisplayTitle;
        }

        public static void LoadGenresFromTitle( this Movie movie, TSVItems.Title imdbTitle, IEnumerable<IGenre> genreList )
        {
            movie.Genres = genreList.Where( o => imdbTitle.Genres.Any( genreName => genreName.Equals( o.Name ) ) ).ToArray();
        }
    }
}
