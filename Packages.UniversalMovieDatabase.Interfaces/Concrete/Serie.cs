using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.UniversalMovieDatabase.Interfaces.Concrete
{
    public class Serie : Movie, ISerie
    {
        public ICollection<Episode> Episodes { get; set; }
    }
}
