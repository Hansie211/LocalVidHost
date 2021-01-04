using Database.Entities;
using System;


namespace DataTransferObjectLibrary
{
    public class GenreDto : IDataTransferObject<Genre>
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        void IDataTransferObject<Genre>.FromEntity( Genre entity )
        {
            ID      = entity.ID;
            Name    = entity.Name;
        }

        void IDataTransferObject<Genre>.ToEntity( Genre entity )
        {
            entity.ID   = ID;
            entity.Name = Name;
        }
    }
}
