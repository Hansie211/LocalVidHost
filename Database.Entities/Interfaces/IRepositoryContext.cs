using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Database.General.Interfaces.Repository;
using Database.General.Interfaces.Repository.Generic;

namespace Database.Entities.Interfaces
{
    public interface IRepositoryContext : IGeneralRepositoryContext
    {
        public IRepository<Movie> Movies { get; }
        public IRepository<Genre> Genres { get; }
        public IRepository<Language> Languages { get; }
        public IRepository<MovieMetadata> MovieMetadatas { get; }
        public IRepository<Subtitle> Subtitles { get; }
        public IRepository<User> Users { get; }
        public IRepository<Serie> Series { get; }
    }
}
