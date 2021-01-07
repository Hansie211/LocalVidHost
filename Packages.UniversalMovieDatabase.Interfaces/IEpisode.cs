using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.UniversalMovieDatabase.Interfaces
{
    public interface IEpisode : IMovie
    {
        public int? SeasonNumber { get; }
        public int? EpisodeNumber { get; }
    }
}
