using Database.Entities;
using System;


namespace DataTransferObjectLibrary
{
    public class MovieMetadataDto : IDataTransferObject<MovieMetadata>
    {
        public Guid ID { get; set; }

        public MovieDto Movie { get; set; }
        public UserDto User { get; set; }

        public double LastPosition { get; set; }
        public int ViewCount { get; set; }
        public bool IsFavorite { get; set; }

        void IDataTransferObject<MovieMetadata>.FromEntity( MovieMetadata entity )
        {
            ID      = entity.ID;
            Movie   = DTOMapper.ToMovieDTO( entity.Movie );
            User    = DTOMapper.ToUserDTO( entity.User );

            LastPosition    = entity.LastPosition;
            ViewCount       = entity.ViewCount;
            IsFavorite      = entity.IsFavorite;
        }

        void IDataTransferObject<MovieMetadata>.ToEntity( MovieMetadata entity )
        {
            entity.ID       = ID;
            entity.Movie    = DTOMapper.FromMovieDTO( Movie );
            entity.User     = DTOMapper.FromUserDTO( User );

            entity.LastPosition = LastPosition;
            entity.ViewCount    = ViewCount;
            entity.IsFavorite   = IsFavorite;
        }
    }
}
