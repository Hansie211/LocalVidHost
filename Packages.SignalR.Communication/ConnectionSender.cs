using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Packages.SignalR.Communication
{
    public abstract class ConnectionSender : SignalRConnection
    {
        #region SendAsync Reflection

        private const int MAX_SENDASYNC_PARAMS = 11;
        private static readonly MethodInfo[] SendAsyncMethods = new MethodInfo[ MAX_SENDASYNC_PARAMS ];

        static ConnectionSender()
        {
            for ( int i = 0; i < MAX_SENDASYNC_PARAMS; i++ )
            {
                SendAsyncMethods[ i ] = GetSendMethod( i );
            }
        }

        private static MethodInfo GetSendMethod( int paramCount )
        {
            var types = new Type[ 3 + paramCount ];
            for ( int i = 2; i < types.Length - 1; i++ )
            {
                types[ i ] = typeof( object );
            }

            types[ 0 ]  = typeof( HubConnection );
            types[ 1 ]  = typeof( string );
            types[ ^1 ] = typeof( System.Threading.CancellationToken );

            return typeof( HubConnectionExtensions ).GetMethod( nameof( HubConnectionExtensions.SendAsync ), BindingFlags.Static | BindingFlags.Public, null, types, null );
        }

        #endregion

        private static async Task SendAsync( HubConnection connection, string methodName, params object[] args )
        {
            if ( args.Length > SendAsyncMethods.Length - 1 || SendAsyncMethods[ args.Length ] == null )
            {
                throw new ArgumentException( "Too many arguments!" );
            }

            MethodInfo method       = SendAsyncMethods[ args.Length ];
            var methodParameters    = new object[ 3 + args.Length ];

            methodParameters[ 0 ] = connection;
            methodParameters[ 1 ] = methodName;
            methodParameters[ ^1 ] = default( System.Threading.CancellationToken );

            const int paramOffset = 2;
            for ( int i = paramOffset; i < methodParameters.Length - 1; i++ )
            {
                methodParameters[ i ] = args[ i - paramOffset ];
            }

            var methodResult = method.Invoke( null, methodParameters );
            Task task = (Task)methodResult;

            await task;
        }

        public ConnectionSender( Uri uri ) : base ( uri )
        {
        }

        protected async Task ExecuteRemoteAsync( string methodName, params object[] args )
        {
            await SendAsync( HubConnection, methodName, args );
        }
    }
}
