using Packcages.TSV.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.TSVItems
{
    public class Title : TSVItem
    {
        [ColumnName( "titleType" )]
        public string TitleType { get; set; }
        [ColumnName( "primaryTitle" )]
        public string PrimaryTitle { get; set; }
        [ColumnName( "originalTitle" )]
        public string OriginalTitle { get; set; }
        [ColumnName( "isAdult" )]
        public bool? IsAdult { get; set; }
        [ColumnName( "startYear" )]
        public int? StartYear { get; set; }
        [ColumnName( "endYear" )]
        public int? EndYear { get; set; }
        [ColumnName( "runtimeMinutes" )]
        public int? RuntimeMinutes { get; set; }
        [ColumnName( "genres" )]
        public IEnumerable<string> Genres { get; set; }

        public Rating Rating { get; set; }

        public string DisplayTitle { get => OriginalTitle ?? PrimaryTitle ?? "Unknown"; }
    }
}
