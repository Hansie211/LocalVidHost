using System;
using System.Collections.Generic;

namespace Packages.UniversalMovieDatabase.Interfaces
{
    public interface IMovie : IMovieRecord
    {
        public bool? IsAdult { get; }
        public int? ReleaseYear { get; }
        
        public int? RuntimeMinutes { get; }

        public IEnumerable<IGenre> Genres { get; }
    }
}
