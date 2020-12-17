using Database.Entities.Interfaces;
using Database.General.Interfaces;
using Database.General.Interfaces.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository
{
    public class MovieDatabaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDatabaseRecord, new()
    {
        private DbSet<TEntity> Entities { get; }

        public MovieDatabaseRepository( DbSet<TEntity> entities )
        {
            Entities = entities;
        }

        public bool Contains( Guid id )
        {
            if ( id == Guid.Empty )
            {
                return false;
            }

            return (( IEnumerable<TEntity>)this).Any( o => o.ID == id );
        }

        public void Delete( TEntity entity )
        {
            Entities.Remove( entity );
        }

        public TEntity Get( Guid id )
        {
            return this.Single( o => o.ID == id );
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this;
        }

        public void Insert( TEntity entity )
        {
            Entities.Add( entity );
        }

        void IRepository<TEntity>.Update( TEntity entity )
        {
            Entities.Update( entity );
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)Entities).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
