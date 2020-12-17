using BlazorApp.Components;
using BlazorApp.Connection.Client;
using BlazorApp.Player;
using Catalogus.Movie;
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
        public string RemoteResource { get; private set; }
        public double RemotePosition { get; private set; }

        [Inject]
        public MovieCatalogus MovieCatalogus { get; set; }

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
        public void OnSourceChanged( string resourceLocation )
        {
            RemoteResource = resourceLocation;
            StateHasChanged();
        }

        [CallableMethod]
        public void OnPositionChanged( double value )
        {
            RemotePosition = value;
            StateHasChanged();
        }

        [CallableMethod]
        public void SetInitialInfo( string resourceLocation, Playstate playstate, double position )
        {
            RemoteResource  = resourceLocation;
            RemotePlaystate = playstate;
            RemotePosition  = position;

            StateHasChanged();
        }
    }
}
