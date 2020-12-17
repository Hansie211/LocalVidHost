using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.General.Interfaces.Repository
{
    public interface IRepository
    {
        public IDatabaseRecord Get( Guid id );

        public bool Contains( Guid id );

        public void Insert( IDatabaseRecord entity );
        public void Update( IDatabaseRecord entity );
        public void Delete( IDatabaseRecord entity );

        public Task SaveAsync();
    }
}
