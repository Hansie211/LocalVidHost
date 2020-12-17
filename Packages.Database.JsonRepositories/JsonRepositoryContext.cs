using Database.General.Interfaces;
using Database.General.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Packages.Database.JsonRepositories.Generic;

namespace Packages.Database.JsonRepositories
{
    public class JsonRepositoryContext : IGeneralRepositoryContext
    {
        private Dictionary<Type, JsonRepository> Repositories { get; }

        private static Dictionary<Guid, Dictionary<PropertyInfo, object>> LoadRepositoryRawEx<TEntity>( JsonRepository<TEntity> repository ) where TEntity : class, IDatabaseRecord, new()
        {
            return repository.LoadRaw();
        }

        private static Dictionary<Guid, Dictionary<PropertyInfo, object>> LoadRepositoryRaw( Type entityType, JsonRepository repository )
        {
            var genericLoadRepositoryMethod = typeof(JsonRepositoryContext).GetMethod( nameof(LoadRepositoryRawEx), BindingFlags.NonPublic | BindingFlags.Static );
            var method                      = genericLoadRepositoryMethod.MakeGenericMethod( entityType );

            return (Dictionary<Guid, Dictionary<PropertyInfo, object>>)method.Invoke( null, new object[] { repository } );
        }

        private static List<T> CastListEx<T>( IEnumerable<object> objs )
        {
            var result = objs.Cast<T>().ToList();
            return result;
        }

        private static object CastList( object objs, Type type )
        {
            var genericCastMethod   = typeof(JsonRepositoryContext).GetMethod( nameof(CastListEx), BindingFlags.NonPublic | BindingFlags.Static );
            var method              = genericCastMethod.MakeGenericMethod( type );

            return method.Invoke( null, new object[]{ objs }  );
        }

        public JsonRepositoryContext()
        {
            Repositories = new Dictionary<Type, JsonRepository>();

            CreateLocalRepositories();
            LoadRepositories();
        }

        private void CreateLocalRepositories()
        {
            var localRepositoryProperties = this.GetType().GetProperties( BindingFlags.Public | BindingFlags.Instance ).Where(
                (o) => {
                    return typeof(JsonRepository).IsAssignableFrom( o.PropertyType ) && o.CanWrite && o.SetMethod != null;
                }
            );
            foreach ( var localRepositoryProperty in localRepositoryProperties )
            {
                JsonRepository repository = (JsonRepository)Activator.CreateInstance( localRepositoryProperty.PropertyType, new object[] { this } );
                localRepositoryProperty.SetValue( this, repository );

                Repositories.Add( localRepositoryProperty.PropertyType.GetGenericArguments().First(), repository );
            }
        }

        private void LoadRepositories()
        {
            var rawReferences = GetRawReferences();
            foreach ( var rawReferenceData in rawReferences )
            {
                var repo = (IRepository)GetRepository( rawReferenceData.Key );
                foreach ( var data in rawReferenceData.Value )
                {
                    var entity = repo.Get( data.Key );
                    foreach ( var prop in data.Value )
                    {
                        var propertyType = prop.Key.PropertyType;
                        if ( typeof( IEnumerable<IDatabaseRecord> ).IsAssignableFrom( propertyType ) )
                        {
                            propertyType = propertyType.GetGenericArguments().First();
                        }

                        var otherRepo = (IRepository)GetRepository( propertyType );

                        if ( prop.Value is IEnumerable<Guid> )
                        {
                            var guids = (IEnumerable<Guid>)prop.Value;
                            var list  = CastList( guids.Select( o => otherRepo.Get(o) ).Where( o => o != null ), propertyType );

                            prop.Key.SetValue( entity, list );
                        }
                        else
                        {
                            var guid = (Guid)prop.Value;
                            prop.Key.SetValue( entity, otherRepo.Get( guid ) );
                        }
                    }
                }
            }
        }

        private Dictionary<Type, Dictionary<Guid, Dictionary<PropertyInfo, object>>> GetRawReferences()
        {
            var result = new Dictionary<Type, Dictionary<Guid, Dictionary<PropertyInfo, object>>>();
            foreach ( var entityType in Repositories.Keys )
            {
                var raw = LoadRepositoryRaw( entityType, Repositories[ entityType ] );
                result.Add( entityType, raw );
            }

            return result;
        }

        public JsonRepository<T> GetRepository<T>() where T : class, IDatabaseRecord, new()
        {
            return (JsonRepository<T>)GetRepository( typeof( T ) );
        }

        public JsonRepository GetRepository( Type t )
        {
            try
            {
                return Repositories[ t ];
            }
            catch ( KeyNotFoundException )
            {
                return null;
            }
        }

        public async Task SaveAsync()
        {
            foreach ( var repo in Repositories.Values )
            {
                await repo.SaveAsync();
            }
        }
    }
}
