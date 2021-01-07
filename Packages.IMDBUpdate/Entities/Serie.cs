using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.Entities
{
    public class Serie : Movie, ISerie
    {
        public ICollection<IEpisode> Episodes { get; set; }
    }
}
