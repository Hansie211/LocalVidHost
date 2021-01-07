using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.UniversalMovieDatabase.Interfaces
{
    public interface IMovieRecord
    {
        public Guid ID { get; }

        public string IMDBIdentiefier { get; }

        public string Title { get; }
        public double? Rating { get; }
    }
}
