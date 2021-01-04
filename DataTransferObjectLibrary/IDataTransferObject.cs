using Database.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObjectLibrary
{
    public interface IDataTransferObject<TEntity> where TEntity : DatabaseRecord, new()
    {
        public void ToEntity( TEntity entity );
        public void FromEntity( TEntity entity );
    }
}
