using Database.Entities;
using Database.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DataTransferObjectLibrary
{
    public static class DTOMapper
    {
        private static TDTO ToDTO<TDTO, TEntity>( TEntity entity ) where TDTO : IDataTransferObject<TEntity>, new() where TEntity : DatabaseRecord, new()
        {
            var result = new TDTO();
            result.FromEntity( entity );
            return result;
        }

        private static TEntity ToEntity<TEntity, TDTO>( TDTO dto ) where TEntity : DatabaseRecord, new() where TDTO : IDataTransferObject<TEntity>, new()
        {
            var result = new TEntity();
            dto.ToEntity( result );
            return result;
        }

        public static EpisodeDto ToEpisodeDTO( Episode episode )
        {
            return ToDTO<EpisodeDto, Episode>( episode );
        }

        public static Episode FromEpisodeDTO( EpisodeDto episodeDto )
        {
            return ToEntity<Episode, EpisodeDto>( episodeDto );
        }

        public static GenreDto ToGenreDTO( Genre genre )
        {
            return ToDTO<GenreDto, Genre>( genre );
        }

        public static Genre FromGenreDTO( GenreDto genreDto )
        {
            return ToEntity<Genre, GenreDto>( genreDto );
        }

        public static LanguageDto ToLanguageDTO( Language language )
        {
            return ToDTO<LanguageDto, Language>( language );
        }

        public static Language FromLanguageDTO( LanguageDto languageDto )
        {
            return ToEntity<Language, LanguageDto>( languageDto );
        }

        public static MovieDto ToMovieDTO( Movie movie )
        {
            return ToDTO<MovieDto, Movie>( movie );
        }

        public static Movie FromMovieDTO( MovieDto movieDto )
        {
            return ToEntity<Movie, MovieDto>( movieDto );
        }

        public static MovieMetadataDto ToMovieMetadataDTO( MovieMetadata movieMetadata )
        {
            return ToDTO<MovieMetadataDto, MovieMetadata>( movieMetadata );
        }

        public static MovieMetadata FromMovieMetadataDTO( MovieMetadataDto movieMetadataDto )
        {
            return ToEntity<MovieMetadata, MovieMetadataDto>( movieMetadataDto );
        }

        public static SubtitleDto ToSubtitleDTO( Subtitle subtitle )
        {
            return ToDTO<SubtitleDto, Subtitle>( subtitle );
        }

        public static Subtitle FromSubtitleDTO( SubtitleDto subtitleDto )
        {
            return ToEntity<Subtitle, SubtitleDto>( subtitleDto );
        }

        public static UserDto ToUserDTO( User user )
        {
            return ToDTO<UserDto, User>( user );
        }

        public static User FromUserDTO( UserDto userDto )
        {
            return ToEntity<User, UserDto>( userDto );
        }
    }
}