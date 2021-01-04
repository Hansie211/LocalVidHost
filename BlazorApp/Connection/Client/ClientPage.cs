using BlazorApp.Components;
using BlazorApp.Connection.Client;
using BlazorApp.Player;
using Catalogus.Movies;
using Microsoft.AspNetCore.Components;
using Packages.SignalR.Communication.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Connection.Client
{
    public class ClientPage : MasterPage<ClientConnectionSender>
    {
        public Playstate RemotePlaystate { get; private set; }
        public string RemoteMovie { get; private set; }
        public double RemotePosition { get; private set; }
        public double RemoteDuration { get; private set; }

        protected override async Task InitAsync()
        {
            await InitAsync( "/clientActionHub", "/serverActionHub" );

            await Sender.RequestServeInitalInfoAsync( ReceiverHub.ConnectionId );
        }

        [CallableMethod]
        public void OnPlaystateChanged( Playstate state )
        {
            RemotePlaystate = state;
            StateHasChanged();
        }

        [CallableMethod]
        public void OnSourceChanged( string moviePath )
        {
            RemoteMovie = moviePath;

            StateHasChanged();
        }

        [CallableMethod]
        public void OnPositionChanged( double value )
        {
            RemotePosition = value;
            StateHasChanged();
        }

        [CallableMethod]
        public void OnDurationChanged( double value )
        {
            RemoteDuration = value;
            StateHasChanged();
        }

        [CallableMethod]
        public void SetInitialInfo( string moviePath, Playstate playstate, double position, double duration )
        {
            RemoteMovie     = moviePath;
            RemoteDuration  = duration;
            RemotePlaystate = playstate;
            RemotePosition  = position;

            StateHasChanged();
        }
    }
}
