using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;

namespace Packages.UniversalMovieDatabase.Interfaces.Concrete
{
    public class Movie : MovieRecord, IMovie
    {
        public int? ReleaseYear { get; set; }
        public bool? IsAdult { get; set; }
        public int? RuntimeMinutes { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
