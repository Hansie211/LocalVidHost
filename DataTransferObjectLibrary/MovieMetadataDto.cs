using System;


namespace DataTransferObjectLibrary
{
    public class MovieMetadataDto
    {
        public Guid ID { get; set; }

        public MovieDto Movie { get; set; }
        public UserDto User { get; set; }

        public double LastPosition { get; set; }
        public int ViewCount { get; set; }
        public bool IsFavorite { get; set; }
    }
}
