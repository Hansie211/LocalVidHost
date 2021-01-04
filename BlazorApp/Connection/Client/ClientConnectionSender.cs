using Packages.SignalR.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Connection.Client
{
    public class ClientConnectionSender : ConnectionSender
    {
        private readonly BlazorApp.Hubs.VideoServerActionHub RemoteHub = null;

        public ClientConnectionSender( Uri uri ) : base( uri )
        {
        }

        public async Task RequestPlayResourceAsync( string moviePath )
        {
            await ExecuteRemoteAsync( nameof(RemoteHub.RequestPlayResourceAsync), moviePath );
        }

        public async Task RequestChangePosition( double position )
        {
            await ExecuteRemoteAsync( nameof(RemoteHub.RequestChangePositionAsync), position );
        }

        public async Task RequestChangePlaystate( Player.Playstate playstate )
        {
            await ExecuteRemoteAsync( nameof(RemoteHub.RequestChangePlaystateAsync), playstate );
        }

        public async Task RequestServeInitalInfoAsync( string responseId )
        {
            await ExecuteRemoteAsync( nameof( RemoteHub.RequestServeInitalInfoAsync ), responseId );
        }
    }
}
