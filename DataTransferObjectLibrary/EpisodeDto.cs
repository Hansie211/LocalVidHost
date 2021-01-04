using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTransferObjectLibrary
{
    public class EpisodeDto : MovieDto, IDataTransferObject<Episode>
    {
        public int Number { get; set; }
        public int Season { get; set; }

        void IDataTransferObject<Episode>.FromEntity( Episode entity )
        {
            ((IDataTransferObject<Movie>)this).FromEntity( entity as Movie );

            Number = entity.Number;
            Season = entity.Season;
        }

        void IDataTransferObject<Episode>.ToEntity( Episode entity )
        {
            ( (IDataTransferObject<Movie>)this ).ToEntity( entity as Movie );

            entity.Season = Season;
            entity.Number = Number;
        }
    }
}
