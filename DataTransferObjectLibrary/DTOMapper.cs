using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTransferObjectLibrary
{
    public static class DTOMapper
    {
        private static Dictionary<Type, List<PropertyInfo>> PropertyCache { get; } = new Dictionary<Type, List<PropertyInfo>>();

        static DTOMapper()
        {
            var precacheTypes = new Type[]{ 
                typeof(Episode), typeof(EpisodeDto),
                typeof(Genre), typeof(GenreDto),
                typeof(Language), typeof(LanguageDto),
                typeof(Movie), typeof(MovieDto),
                typeof(MovieMetadata), typeof(MovieMetadataDto),
                typeof(Subtitle), typeof(SubtitleDto),
                typeof(User), typeof(UserDto),
            };

            foreach( var type in precacheTypes )
            {
                GetProperties( type ); // build cache
            }
        }

        private static IEnumerable<PropertyInfo> GetProperties( Type type )
        {
            if ( PropertyCache.TryGetValue( type, out List<PropertyInfo> value ) )
            {
                return value;
            }

            var list = type.GetProperties().Where( o => o.CanRead && o.CanWrite && o.SetMethod != null && o.GetMethod != null ).ToList();
            PropertyCache.Add( type, list );
            return list;
        }

        private static TOut PropertyCopy<TOut, TIn>( TIn obj ) where TOut : class, new()
        {
            TOut result = new TOut();
            
            var propsIn = GetProperties( typeof(TIn) );
            var propsOut = GetProperties( typeof(TOut) );

            foreach( var propIn in propsIn )
            {
                var propOut = propsOut.FirstOrDefault( o => o.Name.Equals( propIn.Name, StringComparison.OrdinalIgnoreCase ) );
                if ( propOut is null )
                {
                    continue;
                }

                var value = propIn.GetValue( obj );
                propOut.SetValue( result, value );
            }

            return result;
        }

        public static EpisodeDto ToEpisodeDTO( Episode Episode )
        {
            var copy = PropertyCopy<EpisodeDto, Episode>( Episode );
            return copy;
        }

        public static Episode FromEpisodeDTO( EpisodeDto EpisodeDto )
        {
            var copy = PropertyCopy<Episode, EpisodeDto>( EpisodeDto );
            return copy;
        }

        public static GenreDto ToGenreDTO( Genre Genre )
        {
            var copy = PropertyCopy<GenreDto, Genre>( Genre );
            return copy;
        }

        public static Genre FromGenreDTO( GenreDto GenreDto )
        {
            var copy = PropertyCopy<Genre, GenreDto>( GenreDto );
            return copy;
        }

        public static LanguageDto ToLanguageDTO( Language Language )
        {
            var copy = PropertyCopy<LanguageDto, Language>( Language );
            return copy;
        }

        public static Language FromLanguageDTO( LanguageDto LanguageDto )
        {
            var copy = PropertyCopy<Language, LanguageDto>( LanguageDto );
            return copy;
        }

        public static MovieDto ToMovieDTO( Movie Movie )
        {
            var copy = PropertyCopy<MovieDto, Movie>( Movie );
            return copy;
        }

        public static Movie FromMovieDTO( MovieDto MovieDto )
        {
            var copy = PropertyCopy<Movie, MovieDto>( MovieDto );
            return copy;
        }

        public static MovieMetadataDto ToMovieMetadataDTO( MovieMetadata MovieMetadata )
        {
            var copy = PropertyCopy<MovieMetadataDto, MovieMetadata>( MovieMetadata );
            return copy;
        }

        public static MovieMetadata FromMovieMetadataDTO( MovieMetadataDto MovieMetadataDto )
        {
            var copy = PropertyCopy<MovieMetadata, MovieMetadataDto>( MovieMetadataDto );
            return copy;
        }

        public static SubtitleDto ToSubtitleDTO( Subtitle Subtitle )
        {
            var copy = PropertyCopy<SubtitleDto, Subtitle>( Subtitle );
            return copy;
        }

        public static Subtitle FromSubtitleDTO( SubtitleDto SubtitleDto )
        {
            var copy = PropertyCopy<Subtitle, SubtitleDto>( SubtitleDto );
            return copy;
        }

        public static UserDto ToUserDTO( User User )
        {
            var copy = PropertyCopy<UserDto, User>( User );
            return copy;
        }

        public static User FromUserDTO( UserDto UserDto )
        {
            var copy = PropertyCopy<User, UserDto>( UserDto );
            return copy;
        }
    }
}
