using BlazorApp.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Connection.Client
{
    public class BrowsePage : MasterPage<ClientConnectionSender>
    {
        private const char WebSeperator = '/';
        public string CurrentFolder { get; private set; } = string.Empty;

        protected override async Task InitAsync()
        {
            await InitAsync( "/clientActionHub", "/serverActionHub" );
        }

        public void AscendCurrentFolder( string folder )
        {
            CurrentFolder = CurrentFolder + folder + WebSeperator;
        }

        public string GetFilePath( string movieName )
        {
            return CurrentFolder + movieName;
        }

        public void DescendCurrentFolder()
        {
            if ( string.IsNullOrEmpty( CurrentFolder ) )
            {
                return;
            }

            int p = CurrentFolder.LastIndexOf( WebSeperator, CurrentFolder.Length - 2 );
            if ( p < 0 )
            {
                CurrentFolder = string.Empty;
                return;
            }

            CurrentFolder = CurrentFolder.Substring( 0, p + 1);
        }
    }
}
