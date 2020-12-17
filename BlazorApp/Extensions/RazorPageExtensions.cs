using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BlazorApp.Extensions
{
    public static class RazorPageExtensions
    {

        private static readonly MethodInfo StateHasChangedMethod = typeof(ComponentBase).GetMethod("StateHasChanged", BindingFlags.NonPublic | BindingFlags.Instance );

        public static void UpdateState( this ComponentBase component )
        {
            StateHasChangedMethod.Invoke( component, new object[] { } );
        }

    }
}
