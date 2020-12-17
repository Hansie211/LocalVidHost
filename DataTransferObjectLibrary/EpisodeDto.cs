using System;


namespace DataTransferObjectLibrary
{
    public class EpisodeDto : MovieDto
    {
        public int Number { get; set; }
        public int Season { get; set; }
    }
}
