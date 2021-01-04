using System;
using System.Collections.Generic;

namespace DataTransferObjectLibrary
{
    public class SerieDto
    {
        public string Title { get; set; }
        public List<EpisodeDto> Episodes { get; set; }
    }
}
