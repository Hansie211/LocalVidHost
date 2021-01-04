using Packages.SignalR.Communication;
using Packages.SignalR.Communication.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Catalogus.Movies;

namespace BlazorApp.Components
{
    public abstract class MasterPage<TSender> : ComponentBase where TSender: ConnectionSender
    {
        protected TSender Sender { get; private set; }
        protected HubConnection ReceiverHub { get; private set; }

        [Inject]
        protected IJSRuntime JS { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected MovieCatalogus Catalogus { get; set; }


        private static void RegisterMethods( MasterPage<TSender> listener )
        {
            var methods = listener.GetType().GetMethods( BindingFlags.Public | BindingFlags.Instance );
            foreach ( var method in methods )
            {
                if ( !method.IsDefined( typeof( CallableMethodAttribute ) ) )
                {
                    continue;
                }

                string name         = method.Name;
                var parameterTypes  = method.GetParameters().Select( o => o.ParameterType ).ToArray();

                listener.ReceiverHub.On( name, parameterTypes, ( parameters, methodArg ) => {

                    MethodInfo info = (MethodInfo)methodArg;
                    var result = info.Invoke( listener, parameters );

                    if ( result is Task task )
                    {
                        return task;
                    }

                    return Task.CompletedTask;

                }, method );
            }
        }

        public MasterPage()
        {
        }

        protected async Task InitAsync( string receiveUrl, string senderUrl )
        {
            Sender      = (TSender)Activator.CreateInstance( typeof( TSender ), new object[] { NavigationManager.ToAbsoluteUri( senderUrl ) } );
            ReceiverHub = new HubConnectionBuilder().WithUrl( NavigationManager.ToAbsoluteUri( receiveUrl ) ).Build();

            RegisterMethods( this );

            await StartAsync();
        }

        protected abstract Task InitAsync();

        protected async Task StartAsync()
        {
            await ReceiverHub.StartAsync();
            await Sender.StartAsync();
        }

        protected override async Task OnAfterRenderAsync( bool firstRender )
        {
            if ( firstRender )
            {
                await JS.InvokeVoidAsync( "page.init", DotNetObjectReference.Create( this ) );
            }

            await base.OnAfterRenderAsync( firstRender );
        }
    }
}
