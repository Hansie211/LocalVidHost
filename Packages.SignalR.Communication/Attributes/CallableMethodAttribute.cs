using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.SignalR.Communication.Attributes
{
    [AttributeUsage( AttributeTargets.Method, AllowMultiple = false, Inherited = true )]
    public class CallableMethodAttribute : Attribute
    {
    }
}
