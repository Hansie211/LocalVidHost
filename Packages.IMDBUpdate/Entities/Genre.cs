using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.Entities
{
    public class Genre : IGenre
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}
