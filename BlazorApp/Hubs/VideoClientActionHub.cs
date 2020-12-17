using BlazorApp.Player;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Hubs
{
    public class VideoClientActionHub : Hub
    {
        private readonly BlazorApp.Connection.Client.ClientPage RemoteReceiver = null;

        public async Task RequestUpdatePlaystateAsync( Playstate state )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.OnPlaystateChanged ), state );
        }

        public async Task RequestUpdatePositionAsync( double value )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.OnPositionChanged ), value );
        }

        public async Task RequestUpdateSourceAsync( string resourceLocation )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.OnSourceChanged ), resourceLocation );
        }

        public async Task RequestSetInitialInfoAsync( string connectionId, string resourceLocation, Playstate playstate, double position )
        {
            var client = Clients.Client( connectionId );
            await client?.SendAsync( nameof( RemoteReceiver.SetInitialInfo ), resourceLocation, playstate, position );
        }

    }
}
