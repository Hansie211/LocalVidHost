using BlazorApp.Components;
using BlazorApp.Player;
using Packages.SignalR.Communication.Attributes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Connection.Server
{
    public class ServerPage : MasterPage<ServerConnectionSender>
    {
        public string MoviePath { get; set; }
        public Playstate Playstate { get; set; }
        public double Position { get; set; }
        public double Duration { get; set; }

        protected override async Task InitAsync()
        {
            await InitAsync( "/serverActionHub", "/clientActionHub" );
        }

        [CallableMethod]
        public async Task PlayResource( string moviePath )
        {
            MoviePath   = moviePath;
            await JS.InvokeAsync<object>( "OnUpdateVideoSource", "/Storage/" + moviePath );
            await ChangePlaystate( Playstate.Play );

            await Sender.UpdateSource( MoviePath );
        }

        [CallableMethod]
        public async Task ChangePlaystate( Playstate state )
        {
            if (string.IsNullOrEmpty(MoviePath) )
            {
                return;
            }

            Playstate = state;

            await JS.InvokeAsync<object>( "SetPlaystate", state );
            await Sender.UpdatePlaystateAsync( state );
        }

        [CallableMethod]
        public async Task ChangePosition( double value )
        {
            Position = Math.Clamp( value, 0, Duration );

            await JS.InvokeAsync<object>( "SetPosition", Position );
        }

        [CallableMethod]
        public async Task ServeInitialInfo( string connectionId )
        {
            await Sender.ServeInitialInfo( connectionId, MoviePath, Playstate, Position, Duration );
        }

        [JSInvokable( "OnUpdatePosition" )]
        public async Task OnUpdatePosition( double value )
        {
            Position = value;

            await Sender.RequestUpdatePosition( value );
        }

        [JSInvokable( "OnUpdateDuration" )]
        public async Task OnUpdateDuration( double value )
        {
            Duration = value;

            await Sender.RequestUpdateDuration( value );
        }
    }
}
