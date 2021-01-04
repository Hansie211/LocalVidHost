using Packages.SettingsLib;
using Packages.SettingsLib.Attributes;
using Packages.SettingsLib.IO;
using System;
using System.IO;

namespace SettingsLib
{

    [FolderName( "LocalVidHost" )]
    public class MainSettings : SettingsBase
    {
        public static MainSettings Instance { get; } = new MainSettings();

        static MainSettings()
        {
            Instance.Load();
            Instance.LoadSubfile( "debug" );
        }

        private MainSettings() { }

        public string MoviePath { get; set; }

        public string GetMovieCompletePath( string moviePath )
        {
            string result = Path.Combine( MoviePath, moviePath );
            result = Path.GetFullPath( result );

            if ( !result.StartsWith( MoviePath, StringComparison.OrdinalIgnoreCase ) )
            {
                return MoviePath;
            }

            return result;
        }

        private string IncludeTrailingDirectorySeperator( string path )
        {
            char seperator1 = Path.DirectorySeparatorChar;
            char seperator2 = Path.AltDirectorySeparatorChar;

            if ( path.EndsWith( seperator1 ) || path.EndsWith( seperator2 ) )
            {
                return path;
            }

            if ( path.Contains( seperator2 ) )
            {
                return path + seperator2;
            }

            return path + seperator1;
        }

        protected override void AfterImport()
        {
            MoviePath = IncludeTrailingDirectorySeperator( MoviePath.Trim() );
        }
    }
}
