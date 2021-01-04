using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.SettingsLib.Attributes
{
    public class FileNameAttribute : ValueAttribute
    {
        public FileNameAttribute( string value ) : base( value )
        {
        }
    }
}
