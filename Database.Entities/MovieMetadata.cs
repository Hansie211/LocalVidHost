using Database.General;
using System;


namespace Database.Entities
{
    public class MovieMetadata : DatabaseRecord
    {
        public Movie Movie { get; set; }
        public User User { get; set; }

        public double LastPosition { get; set; }
        public int ViewCount { get; set; }
        public bool IsFavorite { get; set; }
    }
}
