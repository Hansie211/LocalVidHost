using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.UniversalMovieDatabase.Entities
{
    public abstract class MovieRecord : IMovieRecord
    {
        public Guid ID { get; }
        public string IMDBIdentiefier { get; }
        public string Title { get; }
        public double? Rating { get; }
    }
}
