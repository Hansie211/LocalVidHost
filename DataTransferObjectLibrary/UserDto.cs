using System;
using System.Collections.Generic;


namespace DataTransferObjectLibrary
{
    public class UserDto
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<MovieMetadataDto> MovieMetadatas { get; set; }
    }
}
