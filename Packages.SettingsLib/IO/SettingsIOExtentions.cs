using Packages.SettingsLib.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Packages.SettingsLib.IO
{
    public static class SettingsIOExtentions
    {
        #region Paths
        static string DEFAULT_FILENAME { get => "settings.json"; }
        private static string DEFAULT_FOLDERNAME { get => Assembly.GetEntryAssembly().GetName().Name; }

        private static string GetAttributeValue<TAttr>( Type settingsType, string defaultValue ) where TAttr : ValueAttribute
        {
            var attribute = settingsType.GetCustomAttributes<TAttr>( true ).FirstOrDefault();
            return string.IsNullOrEmpty( attribute?.Value ) ? defaultValue : attribute.Value;
        }

        private static string GetFileName( Type settingsType )
        {
            return GetAttributeValue<FileNameAttribute>( settingsType, DEFAULT_FILENAME );
        }

        private static string GetFolderName( Type settingsType )
        {
            return GetAttributeValue<FolderNameAttribute>( settingsType, DEFAULT_FOLDERNAME );
        }

        private static string GetFolderPath( Type settingsType )
        {
            return Path.Join( Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData ), GetFolderName( settingsType ) );
        }

        #region expressions
        private static string GetFileName<TSetting>() where TSetting : SettingsBase => GetFileName( typeof( TSetting ) );
        private static string GetFileName( SettingsBase self ) => GetFileName( self.GetType() );

        private static string GetFolderName<TSetting>() where TSetting : SettingsBase => GetFolderName( typeof( TSetting ) );
        private static string GetFolderName( SettingsBase self ) => GetFolderName( self.GetType() );

        private static string GetFolderPath<TSetting>() where TSetting : SettingsBase => GetFolderPath( typeof( TSetting ) );
        private static string GetFolderPath( SettingsBase self ) => GetFolderPath( self.GetType() );
        #endregion

        #endregion

        public static bool LoadFromFile( this SettingsBase settings, string filePath, bool clean = true )
        {
            if ( clean )
            {
                settings.Clear();
            }

            if ( !File.Exists( filePath ) )
            {
                return false;
            }

            try
            {
                var values = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>( File.ReadAllText( filePath ) );
                settings.ImportValues( values );

                return true;
            }
            catch ( Exception exp )
            {
                settings.LogException( exp );
                return false;
            }
        }

        public static bool Load( this SettingsBase settings, string fileName, bool clean = true )
        {
            return LoadFromFile( settings, Path.Combine( GetFolderPath( settings ), fileName ), clean );
        }

        public static bool Load( this SettingsBase settings, bool clean = true )
        {
            return Load( settings, GetFileName( settings ), clean );
        }

        public static bool LoadSubfile( this SettingsBase settings, string subIdent )
        {
            string[] filenameParts = GetFileName( settings ).Split('.');
            StringBuilder sb = new StringBuilder();

            if ( filenameParts.Length == 1 )
            {
                sb.Append( filenameParts[ 0 ] );
                sb.Append( "." );
                sb.Append( subIdent );
            }
            else
            {
                for ( int i = 0; i < filenameParts.Length - 1; i++ )
                {
                    sb.Append( filenameParts[ i ] );
                    sb.Append( "." );
                }

                sb.Append( subIdent );
                sb.Append( "." );

                sb.Append( filenameParts[ filenameParts.Length - 1 ] );
            }

            return Load( settings, sb.ToString(), false );
        }

        public static bool SaveToFile( this SettingsBase settings, string filePath )
        {
            if ( string.IsNullOrWhiteSpace( filePath ) )
            {
                return false;
            }

            string folder = Path.GetDirectoryName( filePath );
            Directory.CreateDirectory( folder );

            var values = settings.ExportValues();

            string data = JsonSerializer.Serialize( values, new JsonSerializerOptions(){ WriteIndented = true } );
            try
            {
                File.WriteAllText( filePath, data );
            }
            catch ( Exception exp )
            {
                settings.LogException( exp );
                return false;
            }

            return true;
        }

        public static bool Save( this SettingsBase settings, string fileName )
        {
            return SaveToFile( settings, Path.Combine( GetFolderPath( settings ), fileName ) );
        }

        public static bool Save( this SettingsBase settings )
        {
            return Save( settings, GetFileName( settings ) );
        }
    }
}
