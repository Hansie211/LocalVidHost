using SettingsLib;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Catalogus.Movies
{
    public partial class MovieCatalogus
    {
        private static readonly string[] SupportedExtensions = {
            "WEBM", "MP4", "OGG", 
            //"MPG", "MP2", "MPEG", "MPE", "MPV", "M4P", "M4V", "AVI", "MMV", "MOV", "QT", "FLV", "SWF" 
            };

        public MovieCatalogus()
        {
        }

        public IEnumerable<string> GetSubfolders( string path )
        {
            path = MainSettings.Instance.GetMovieCompletePath( path );

            return Directory.GetDirectories( path ).Select( o => Path.GetFileName( o ) ).OrderByNatural( o => o );
        }

        public IEnumerable<string> GetMovies( string path )
        {
            path = MainSettings.Instance.GetMovieCompletePath( path );

            return Directory.GetFiles( path ).Where( filename => SupportedExtensions.Any( ext => filename.ToUpper().EndsWith( '.' + ext ) ) ).Select( o => Path.GetFileName( o ) ).OrderByNatural( o => o );
        }
    }
}
