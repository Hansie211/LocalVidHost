using Packages.UniversalMovieDatabase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.Entities
{
    public abstract class IMDBRecord : IMovieRecord
    {
        public Guid ID { get; set; }
        public string IMDBIdentiefier { get; set; }
        public string Title { get; set; }
        public double? Rating { get; set; }
    }
}
