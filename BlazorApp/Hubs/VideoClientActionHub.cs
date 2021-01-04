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

        public async Task RequestUpdateDurationAsync( double value )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.OnDurationChanged), value );
        }

        public async Task RequestUpdateSourceAsync( string moviePath )
        {
            await Clients.All.SendAsync( nameof( RemoteReceiver.OnSourceChanged ), moviePath );
        }

        public async Task RequestSetInitialInfoAsync( string connectionId, string moviePath, Playstate playstate, double position, double duration )
        {
            var client = Clients.Client( connectionId );
            await client?.SendAsync( nameof( RemoteReceiver.SetInitialInfo ), moviePath, playstate, position, duration );
        }

    }
}
