using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Packages.SignalR.Communication
{
    public abstract class SignalRConnection
    {
        public HubConnection HubConnection { get; }
        public bool IsConnected => HubConnection.State == HubConnectionState.Connected;

        public SignalRConnection( Uri uri )
        {
            HubConnection = new HubConnectionBuilder().WithUrl( uri ).Build();
        }

        public async Task StartAsync()
        {
            await HubConnection.StartAsync();
        }
    }
}
