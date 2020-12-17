using System;
using System.Collections.Generic;


namespace DataTransferObjectLibrary
{
    public class MovieDto
    {
        public Guid ID { get; set; }

        public string Title { get; set; }
        public string Filename { get; set; }

        public List<GenreDto> Genres { get; set; }
        public LanguageDto Language { get; set; }

        public string IMDBIndex { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
