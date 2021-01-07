using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.UniversalMovieDatabase.Entities
{
    public class Episode : Movie, IEpisode
    {
        public int? SeasonNumber { get; }
        public int? EpisodeNumber { get; }
    }
}
