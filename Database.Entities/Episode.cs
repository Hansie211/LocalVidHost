using System;


namespace Database.Entities
{
    public class Episode : Movie
    {
        public int Number { get; set; }
        public int Season { get; set; }
    }
}
