using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.SettingsLib.Attributes
{
    public class FolderNameAttribute : ValueAttribute
    {
        public FolderNameAttribute( string value ) : base( value )
        {
        }
    }
}
