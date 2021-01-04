using Database.Entities;
using System;
using System.Collections.Generic;


namespace DataTransferObjectLibrary
{
    public class UserDto : IDataTransferObject<User>
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public List<MovieMetadataDto> MovieMetadatas { get; set; }

        void IDataTransferObject<User>.FromEntity( User entity )
        {
            ID      = entity.ID;
            Name    = entity.Name;
        }

        void IDataTransferObject<User>.ToEntity( User entity )
        {
            entity.ID   = ID;
            entity.Name = Name;
        }
    }
}
