using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Database.General.Interfaces;
using Database.General.Interfaces.Repository;
using Database.General.Interfaces.Repository.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Packages.Database.JsonRepositories.Generic
{
    public class JsonRepository<TEntity> : JsonRepository, IRepository, IRepository<TEntity> where TEntity : class, IDatabaseRecord, new()
    {
        private static readonly List<PropertyInfo> ReferenceProperties;
        private static readonly string Filepath;

        static JsonRepository()
        {
            var props               = typeof( TEntity ).GetProperties( BindingFlags.Public | BindingFlags.Instance ).Where( o => o.CanRead && o.CanWrite && o.SetMethod != null && o.GetMethod != null );
            ReferenceProperties     = props.Where( o => IsDatabaseRecord( o ) || IsDatabaseRecordList( o ) ).ToList();

            Filepath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), "DB", $"{typeof( TEntity ).Name}.json" );
        }

        public JsonRepository( JsonRepositoryContext _ctx ) : base( _ctx )
        {
        }

        private static bool IsDatabaseRecord( Type type )
        {
            return typeof( IDatabaseRecord ).IsAssignableFrom( type );
        }

        private static bool IsDatabaseRecordList( Type type )
        {
            return typeof( IEnumerable<IDatabaseRecord> ).IsAssignableFrom( type );
        }

        private static bool IsDatabaseRecord( PropertyInfo info )
        {
            return IsDatabaseRecord( info.PropertyType );
        }

        private static bool IsDatabaseRecordList( PropertyInfo info )
        {
            return IsDatabaseRecordList( info.PropertyType );
        }

        private static TEntity CreateEntity( JObject data )
        {
            if ( data.TryGetValue( "$type", out JToken typeToken ) )
            {
                var typeString = typeToken.Value<string>();
                var type = Type.GetType( typeString );
                if ( type != null && typeof( TEntity ).IsAssignableFrom( type ) )
                {
                    return (TEntity)data.ToObject( type );
                }
            }

            return data.ToObject<TEntity>();
        }

        private static IDatabaseRecord GetDatabaseRecord( TEntity entity, PropertyInfo prop )
        {
            return (IDatabaseRecord)prop.GetValue( entity );
        }

        private static IEnumerable<IDatabaseRecord> GetDatabaseRecordList( TEntity entity, PropertyInfo prop )
        {
            return (IEnumerable<IDatabaseRecord>)prop.GetValue( entity );
        }

        private static JObject ConvertEntity( TEntity entity )
        {
            if ( entity is null )
            {
                return null;
            }

            var result = JObject.FromObject( entity );

            foreach( var prop in ReferenceProperties )
            {
                object value;
                string name = prop.Name;

                result.Remove( name );

                if ( IsDatabaseRecordList( prop ) )
                {
                    value = GetDatabaseRecordList( entity, prop ).Select( o => o.ID );
                    name += "_IDS";
                }
                else
                {
                    value = GetDatabaseRecord( entity, prop ).ID;
                    name  += "_ID";
                }

                result.Add( new JProperty( name, value ) );
            }

            result.Add( new JProperty( "$type", entity.GetType().AssemblyQualifiedName ) );

            return result;
        }

        internal Dictionary<Guid, Dictionary<PropertyInfo, object>> LoadRaw()
        {
            var result = new Dictionary<Guid, Dictionary<PropertyInfo, object>>();

            try
            {
                string jsondata = File.ReadAllText( Filepath );
                List<JObject> entityDataList = JsonConvert.DeserializeObject<List<JObject>>( jsondata );

                foreach ( var entityData in entityDataList )
                {
                    if ( !entityData.ContainsKey( nameof( IDatabaseRecord.ID ) ) )
                    {
                        continue; // not an entity
                    }

                    TEntity entity = CreateEntity( entityData );
                    Entities.Add( entity );

                    if ( ReferenceProperties.Count == 0 )
                    {
                        continue;
                    }

                    result.Add( entity.ID, new Dictionary<PropertyInfo, object>() );

                    foreach ( var propInfo in ReferenceProperties )
                    {
                        object data;
                        if ( IsDatabaseRecordList( propInfo ) )
                        {
                            if ( !entityData.TryGetValue( propInfo.Name + "_IDS", out JToken token ) )
                            {
                                continue;
                            }

                            var value = token.Value<IEnumerable<object>>();
                            data = value.Select( o => { Guid.TryParse( o.ToString(), out Guid result ); return result; } ).Where( o => o != null ).ToList();
                        }
                        else
                        {
                            if ( !entityData.TryGetValue( propInfo.Name + "_ID", out JToken token ) )
                            {
                                continue;
                            }

                            var value = token.Value<string>();
                            if ( !Guid.TryParse( value, out Guid guid ) )
                            {
                                continue;
                            }

                            data = guid;
                        }

                        result[ entity.ID ].Add( propInfo, data );
                    }
                }

                return result;
            }
            catch
            {
                return new Dictionary<Guid, Dictionary<PropertyInfo, object>>();
            }
        }

        private void Propagate( TEntity root, Action<JsonRepository, IDatabaseRecord> propagateAction )
        {
            foreach ( var prop in ReferenceProperties )
            {
                var repo = ctx.GetRepository( prop.PropertyType );
                if ( repo is null )
                {
                    continue;
                }

                IEnumerable<IDatabaseRecord> items;
                if ( IsDatabaseRecordList( prop ) )
                {
                    items = GetDatabaseRecordList( root, prop );
                }
                else
                {
                    items = new IDatabaseRecord[ 1 ] { GetDatabaseRecord( root, prop ) };
                }

                foreach ( var item in items )
                {
                    propagateAction( repo, item );
                }
            }
        }

        public TEntity Get( Guid id )
        {
            return (TEntity)Entities.Single( o => o.ID == id );
        }


        public TEntity GetOrDefault( Guid id )
        {
            return (TEntity)Entities.FirstOrDefault( o => o.ID == id );
        }

        public TEntity GetOrDefault( Guid? id )
        {
            if ( id is null )
            {
                return default(TEntity);
            }

            return GetOrDefault( id.Value );
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.Cast<TEntity>();
        }

        public void Insert( TEntity entity )
        {
            if ( Contains( entity.ID ) )
            {
                throw new Exception( "ID already exists." );
            }

            if ( entity.ID == Guid.Empty )
            {
                do
                {
                    entity.ID = Guid.NewGuid();
                } while ( Contains( entity.ID ) );
            }

            Entities.Add( entity );

            // Cascade
            Propagate( entity, ( repo, item ) => {
                if ( !repo.Contains( item.ID ) )
                    repo.Insert( item );
            } );
        }

        public void Update( TEntity entity )
        {
            var olditem = Get( entity.ID );
            Delete( olditem );
            Insert( entity );
        }

        public void Delete( TEntity entity )
        {
            int idx = Entities.IndexOf( entity );
            if ( idx < 0 )
            {
                return;
            }

            entity = (TEntity)Entities[ idx ];
            Entities.RemoveAt( idx );

            // Cascade
            Propagate( entity, ( repo, item ) => repo.Delete( item ) );
        }

        public override async Task SaveAsync()
        {
            var formatedEntities = Entities.Select( o => ConvertEntity( (TEntity)o ) );

            string jsonData = JsonConvert.SerializeObject( formatedEntities, Formatting.Indented );
            try
            {
                await File.WriteAllTextAsync( Filepath, jsonData );
            }
            catch ( Exception exp )
            {
                Console.WriteLine( exp );
                throw;
            }
        }

        #region Generic passtru
        public override void Insert( IDatabaseRecord entity )
        {
            Insert( (TEntity)entity );
        }

        public override void Update( IDatabaseRecord entity )
        {
            Update( (TEntity)entity );
        }

        public override void Delete( IDatabaseRecord entity )
        {
            Delete( (TEntity)entity );
        }
        #endregion

        #region Explicit IRepository Implementation
        IDatabaseRecord IRepository.Get( Guid id )
        {
            return Get( id );
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Entities.Cast<TEntity>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
