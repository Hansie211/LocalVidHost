using System;


namespace DataTransferObjectLibrary
{
    public class SubtitleDto
    {
        public Guid ID { get; set; }

        public LanguageDto Language { get; set; }
        public MovieDto Movie { get; set; }

        public string Filename { get; set; }
    }
}
