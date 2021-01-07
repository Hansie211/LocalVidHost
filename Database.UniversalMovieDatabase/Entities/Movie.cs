using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;

namespace Database.UniversalMovieDatabase.Entities
{
    public class Movie : MovieRecord, IMovie
    {
        public int? ReleaseYear { get; }
        public bool? IsAdult { get; }
        public int? RuntimeMinutes { get; }
        public IEnumerable<IGenre> Genres { get; }
    }
}
