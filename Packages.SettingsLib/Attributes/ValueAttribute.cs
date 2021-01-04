using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.SettingsLib.Attributes
{
    [AttributeUsage( validOn: AttributeTargets.Class, AllowMultiple = false, Inherited = true )]
    public class ValueAttribute : Attribute
    {
        public string Value { get; set; }

        public ValueAttribute( string value )
        {
            Value = value;
        }
    }
}
