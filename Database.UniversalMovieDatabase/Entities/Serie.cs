using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.UniversalMovieDatabase.Entities
{
    public class Serie : Movie, ISerie
    {
        public ICollection<IEpisode> Episodes { get; }
    }
}
