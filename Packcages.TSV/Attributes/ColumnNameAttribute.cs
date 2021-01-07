using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.TSV.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnNameAttribute : Attribute
    {
        public string Name { get; }

        public ColumnNameAttribute( string name )
        {
            Name = name;
        }
    }
}
