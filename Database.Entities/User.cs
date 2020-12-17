using Database.General;
using System;
using System.Collections.Generic;


namespace Database.Entities
{
    public class User : DatabaseRecord
    {
        public string Name { get; set; }

        public List<MovieMetadata> MovieMetadatas { get; set; }
    }
}
