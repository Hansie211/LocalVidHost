using Database.General;
using System;
using System.Collections.Generic;


namespace Database.Entities
{
    public class Movie : DatabaseRecord
    {
        public string Title { get; set; }

        public string Filename { get; set; }
        public string ThumbnailFilename { get; set; }

        public List<Genre> Genres { get; set; }
        public Language Language { get; set; }

        public string IMDBIndex { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
