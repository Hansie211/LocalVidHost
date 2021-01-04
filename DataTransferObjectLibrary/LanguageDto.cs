
using Database.Entities;
using System;


namespace DataTransferObjectLibrary
{
    public class LanguageDto : IDataTransferObject<Language>
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        void IDataTransferObject<Language>.FromEntity( Language entity )
        {
            ID          = entity.ID;
            Name        = entity.Name;
        }

        void IDataTransferObject<Language>.ToEntity( Language entity )
        {
            entity.ID   = ID;
            entity.Name = Name;
        }
    }
}
