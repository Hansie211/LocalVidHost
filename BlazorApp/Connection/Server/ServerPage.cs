using BlazorApp.Components;
using BlazorApp.Player;
using Packages.SignalR.Communication.Attributes;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Connection.Server
{
    public class ServerPage : MasterPage<ServerConnectionSender>
    {
        public string ResourceLocation { get; set; }
        public Playstate Playstate { get; set; }
        public double Position { get; set; }

        protected override async Task InitAsync()
        {
            await InitAsync( "/serverActionHub", "/clientActionHub" );
        }

        [CallableMethod]
        public async Task PlayResource( string resourceLocation )
        {
            ResourceLocation = resourceLocation;

            await JS.InvokeAsync<object>( "OnUpdateVideoSource", resourceLocation );
            await ChangePlaystate( Playstate.Play );

            await Sender.UpdateSource( resourceLocation );
        }

        [CallableMethod]
        public async Task ChangePlaystate( Playstate state )
        {
            Playstate = state;

            await JS.InvokeAsync<object>( "SetPlaystate", state );
            await Sender.UpdatePlaystateAsync( state );
        }

        [CallableMethod]
        public async Task ChangePosition( double value )
        {
            Position = value;

            await JS.InvokeAsync<object>( "SetPosition", value );
        }

        [CallableMethod]
        public async Task ServeInitialInfo( string connectionId )
        {
            await Sender.ServeInitialInfo( connectionId, ResourceLocation, Playstate, Position );
        }

        [JSInvokable( "OnUpdatePosition" )]
        public async Task OnUpdatePosition( double value )
        {
            Position = value;

            await Sender.RequestUpdatePosition( value );
        }
    }
}
