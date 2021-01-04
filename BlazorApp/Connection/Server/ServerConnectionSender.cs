using BlazorApp.Player;
using Packages.SignalR.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Connection.Server
{
    public class ServerConnectionSender : ConnectionSender
    {
        private readonly BlazorApp.Hubs.VideoClientActionHub RemoteHub = null;

        public ServerConnectionSender( Uri uri ) : base( uri )
        {
        }

        public async Task UpdatePlaystateAsync( Playstate playState )
        {
            await ExecuteRemoteAsync( nameof( RemoteHub.RequestUpdatePlaystateAsync ), playState );
        }

        public async Task RequestUpdatePosition( double value )
        {
            await ExecuteRemoteAsync( nameof( RemoteHub.RequestUpdatePositionAsync ), value );
        }

        public async Task RequestUpdateDuration( double value )
        {
            await ExecuteRemoteAsync( nameof(RemoteHub.RequestUpdateDurationAsync), value );
        }

        public async Task UpdateSource( string moviePath )
        {
            await ExecuteRemoteAsync( nameof( RemoteHub.RequestUpdateSourceAsync ), moviePath );
        }

        public async Task ServeInitialInfo( string connectionId, string moviePath, Playstate playstate, double position, double duration )
        {
            await ExecuteRemoteAsync( nameof( RemoteHub.RequestSetInitialInfoAsync ), connectionId, moviePath, playstate, position, duration );
        }
    }
}
