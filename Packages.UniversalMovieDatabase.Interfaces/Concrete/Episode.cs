using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.UniversalMovieDatabase.Interfaces.Concrete
{
    public class Episode : Movie, IEpisode
    {
        public int? SeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
    }
}
