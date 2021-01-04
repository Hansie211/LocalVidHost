using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransferObjectLibrary
{
    public class MovieDto : IDataTransferObject<Movie>
    {
        public Guid ID { get; set; }

        public string Title { get; set; }
        public string Filename { get; set; }

        public List<GenreDto> Genres { get; set; }
        public List<SubtitleDto> Subtitles { get; set; }
        public LanguageDto Language { get; set; }

        public string IMDBIndex { get; set; }
        public DateTime ReleaseDate { get; set; }

        void IDataTransferObject<Movie>.FromEntity( Movie entity )
        {
            ID          = entity.ID;
            Title       = entity.Title;
            Filename    = entity.Filename;

            Genres      = entity.Genres.Select( o => DTOMapper.ToGenreDTO( o ) ).ToList();
            Subtitles   = entity.Subtitles.Select( o => DTOMapper.ToSubtitleDTO( o ) ).ToList();
            Language    = DTOMapper.ToLanguageDTO( entity.Language );

            IMDBIndex   = entity.IMDBIndex;
            ReleaseDate = entity.ReleaseDate;
        }

        void IDataTransferObject<Movie>.ToEntity( Movie entity )
        {
            entity.ID           = ID;
            entity.Title        = Title;
            entity.Filename     = Filename;

            entity.Genres       = Genres.Select( o => DTOMapper.FromGenreDTO( o ) ).ToList();
            entity.Subtitles    = Subtitles.Select( o => DTOMapper.FromSubtitleDTO( o ) ).ToList();
            entity.Language     = DTOMapper.FromLanguageDTO( Language );

            entity.IMDBIndex    = IMDBIndex;
            entity.ReleaseDate  = ReleaseDate;
        }
    }
}
