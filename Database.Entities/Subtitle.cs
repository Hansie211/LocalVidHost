using Database.General;
using System;


namespace Database.Entities
{
    public class Subtitle : DatabaseRecord
    {
        public Language Language { get; set; }
        public Movie Movie { get; set; }

        public string Filename { get; set; }
    }
}
