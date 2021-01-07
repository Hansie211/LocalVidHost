using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;

namespace Packages.IMDBUpdate.Entities
{
    public class Movie : IMDBRecord, IMovie
    {
        public int? ReleaseYear { get; set; }
        public bool? IsAdult { get; set; }
        public int? RuntimeMinutes { get; set; }
        public IEnumerable<IGenre> Genres { get; set; }
    }
}
