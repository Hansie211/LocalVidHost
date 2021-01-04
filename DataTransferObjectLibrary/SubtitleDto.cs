using Database.Entities;
using System;


namespace DataTransferObjectLibrary
{
    public class SubtitleDto : IDataTransferObject<Subtitle>
    {
        public Guid ID { get; set; }

        public LanguageDto Language { get; set; }

        void IDataTransferObject<Subtitle>.FromEntity( Subtitle entity )
        {
            ID          = entity.ID;
            Language    = DTOMapper.ToLanguageDTO( entity.Language );
        }

        void IDataTransferObject<Subtitle>.ToEntity( Subtitle entity )
        {
            entity.ID       = ID;
            entity.Language = DTOMapper.FromLanguageDTO( Language );
        }
    }
}
