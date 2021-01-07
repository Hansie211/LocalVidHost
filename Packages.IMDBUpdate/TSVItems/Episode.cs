using Packcages.TSV.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.TSVItems
{
    public class Episode : TSVItem
    {
        [ColumnName( "parentTconst" )]
        public string ParentTitleConst { get; set; }

        [ColumnName( "seasonNumber" )]
        public int? SeasonNumber { get; set; }
        [ColumnName( "episodeNumber" )]
        public int? EpisodeNumber { get; set; }

        public Title Title { get; set; }
    }
}
