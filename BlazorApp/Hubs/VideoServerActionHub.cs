using BlazorApp.Player;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Hubs
{
    public class VideoServerActionHub : Hub
    {
        private readonly BlazorApp.Connection.Server.ServerPage RemoteReceiver = null;

        public async Task RequestPlayResourceAsync( string moviePath )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.PlayResource ), moviePath );
        }

        public async Task RequestChangePlaystateAsync( Playstate state )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.ChangePlaystate ), state );
        }

        public async Task RequestChangePositionAsync( double value )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.ChangePosition ), value );
        }

        public async Task RequestServeInitalInfoAsync( string connectionId )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.ServeInitialInfo ), connectionId );
        }
    }
}
