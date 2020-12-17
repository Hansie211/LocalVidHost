using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.General.Interfaces.Repository.Generic
{
    public interface IRepository<TEntity> : IEnumerable<TEntity> where TEntity: class, IDatabaseRecord, new()
    {
        public TEntity Get( Guid id );
        public IEnumerable<TEntity> GetAll();

        public bool Contains( Guid id );

        public void Insert( TEntity entity );
        public void Update( TEntity entity );
        public void Delete( TEntity entity );
    }
}
