using Database.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Entities
{
    public class Serie : DatabaseRecord
    {
        public string Title { get; set; }
        public List<Episode> Episodes { get; set; }
    }
}
