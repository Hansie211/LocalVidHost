using Packages.TSV.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.TSVItems
{
    public abstract class TSVItem
    {
        [ColumnName( "tconst" )]
        public string TitleConst { get; set; }

        public override bool Equals( object obj )
        {
            return TitleConst.Equals( ( obj as TSVItem )?.TitleConst );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( TitleConst );
        }

        public static bool operator ==( TSVItem A, TSVItem B )
        {
            if ( A is null )
                return B is null;

            return A.Equals( B );
        }

        public static bool operator !=( TSVItem A, TSVItem B )
        {
            return !( A == B );
        }
    }
}
