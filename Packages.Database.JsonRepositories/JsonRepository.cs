using Database.General.Interfaces;
using Database.General.Interfaces.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Packages.Database.JsonRepositories
{
    public abstract class JsonRepository
    {
        protected readonly JsonRepositoryContext ctx;
        protected readonly List<IDatabaseRecord> Entities;

        public JsonRepository( JsonRepositoryContext _ctx )
        {
            ctx      = _ctx;
            Entities = new List<IDatabaseRecord>();
        }

        public bool Contains( Guid id )
        {
            return ( id != null && id != Guid.Empty && Entities.Any( o => o?.ID == id ) );
        }

        public abstract Task SaveAsync();
        public abstract void Insert( IDatabaseRecord entity );
        public abstract void Update( IDatabaseRecord entity );
        public abstract void Delete( IDatabaseRecord entity );
    }
}
