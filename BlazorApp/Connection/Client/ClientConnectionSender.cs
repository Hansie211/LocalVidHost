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

        public async Task RequestPlayResourceAsync( string resourceLocation )
        {
            await ExecuteRemoteAsync( nameof(RemoteHub.RequestPlayResourceAsync), resourceLocation );
        }

        public async Task RequestServeInitalInfoAsync( string responseId )
        {
            await ExecuteRemoteAsync( nameof( RemoteHub.RequestServeInitalInfoAsync ), responseId );
        }
    }
}
