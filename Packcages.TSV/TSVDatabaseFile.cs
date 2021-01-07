using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Packcages.TSV
{
    public class TSVDatabaseFile : IDisposable
    {
        public string[] ColumnNames { get; }

        private Stream Stream { get; }
        private StreamReader Reader { get; }

        private static bool IsNull( string value )
        {
            return ( value is null ) || ( value == @"\N" );
        }

        public static bool? ParseBool( string value )
        {
            if ( IsNull( value ) )
                return null;

            return value != "1";
        }

        public static int? ParseInt( string value )
        {
            if ( IsNull( value ) )
                return null;

            int.TryParse( value, out int result );
            return result;
        }

        public static string ParseString( string value )
        {
            if ( IsNull( value ) )
                return null;

            return value;
        }

        public static string[] ParseCollection( string value )
        {
            if ( IsNull( value ) )
                return null;

            return value.Split( ',' );
        }

        public static double? ParseDouble( string value )
        {
            if ( IsNull( value ) )
                return null;

            double.TryParse( value, out double result );
            return result;
        }

        public TSVDatabaseFile( Stream stream )
        {
            Stream = stream;
            Reader = new StreamReader( Stream );

            ColumnNames = GetColumnNames( Reader );
        }

        public TSVDatabaseFile( string filepath ) : this( new FileStream( filepath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
        {
        }

        public TSVDatabaseFile( byte[] buffer ) : this( new MemoryStream( buffer ) )
        {
        }

        ~TSVDatabaseFile()
        {
            Dispose( false );
        }

        private void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Reader.Dispose();
                Stream.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose( true );
        }

        private string[] GetColumnNames( StreamReader reader )
        {
            return reader.ReadLine().Trim().Split( '\t' );
        }

        public IEnumerable<string[]> GetAll()
        {
            string line;
            while ( ( line = Reader.ReadLine() ) != null )
            {
                var items = line.Split('\t');
                yield return items;
            }
        }
    }
}
