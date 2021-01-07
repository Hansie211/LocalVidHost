using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.UniversalMovieDatabase.Interfaces
{
    public interface IGenre
    {
        public Guid ID { get; }

        public string Name { get; }
    }
}
