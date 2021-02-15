using Packages.UniversalMovieDatabase.Interfaces.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.UniversalMovieDatabase.Interfaces
{
    public interface ISerie : IMovie
    {
        public ICollection<Episode> Episodes { get; }
    }
}
