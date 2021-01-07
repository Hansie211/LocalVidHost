using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.UniversalMovieDatabase.Entities
{
    public class Genre : IGenre
    {
        public Guid ID { get; }
        public string Name { get; }
    }
}
