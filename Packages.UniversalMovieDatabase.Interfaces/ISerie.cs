using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.UniversalMovieDatabase.Interfaces
{
    public interface ISerie : IMovie
    {
        public ICollection<IEpisode> Episodes { get; }
    }
}
