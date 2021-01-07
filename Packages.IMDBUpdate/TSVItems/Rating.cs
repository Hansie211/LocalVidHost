using Packages.TSV.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.TSVItems
{
    public class Rating : TSVItem
    {
        [ColumnName( "averageRating" )]
        public double? AvgRating {get; set;}
        [ColumnName( "numVotes" )]
        public int? NumVotes { get; set; }
    }
}
