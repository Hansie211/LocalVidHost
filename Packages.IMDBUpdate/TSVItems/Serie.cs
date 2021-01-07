using System;
using System.Collections.Generic;
using System.Text;

namespace Packages.IMDBUpdate.TSVItems
{
    public class Serie : TSVItem
    {
        public IEnumerable<Episode> Episodes { get; set; }
    }
}
