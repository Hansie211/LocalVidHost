using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.Entities
{
    public class Episode : Movie, IEpisode
    {
        public int? SeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
    }
}
