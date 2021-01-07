using Packages.TSV.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Packages.TSV.Generics
{
    public class TSVDatabaseFile<TEntity> : TSVDatabaseFile, IEnumerable<TEntity> where TEntity : new()
    {
        private static Dictionary<Type, Func<string, object>> Conversions { get; }

        private PropertyInfo[] ColumnIndexes { get; }

        static TSVDatabaseFile()
        {
            Conversions = new Dictionary<Type, Func<string, object>>();

            Conversions.Add( typeof( string ), ( x ) => TSVDatabaseFile.ParseString( x ) );
            Conversions.Add( typeof( bool? ), ( x ) => TSVDatabaseFile.ParseBool( x ) );
            Conversions.Add( typeof( int? ), ( x ) => TSVDatabaseFile.ParseInt( x ) );
            Conversions.Add( typeof( double? ), ( x ) => TSVDatabaseFile.ParseDouble( x ) );
            Conversions.Add( typeof( IEnumerable<string> ), ( x ) => TSVDatabaseFile.ParseCollection( x ) );
        }

        private static string GetColumnName( PropertyInfo info )
        {
            if ( !info.IsDefined( typeof( ColumnNameAttribute ) ) )
            {
                return null;
            }

            return info.GetCustomAttribute<ColumnNameAttribute>().Name;
        }

        private static PropertyInfo[] GetColumnIndexes( Type entityType, string[] columnNames )
        {
            var result      = new PropertyInfo[ columnNames.Length ];
            var properties  = entityType.GetProperties( BindingFlags.Public | BindingFlags.Instance );

            var pairs = properties.Where( o => o.IsDefined( typeof(ColumnNameAttribute) ) ).Select( o => new KeyValuePair<string, PropertyInfo>( GetColumnName( o ), o ) );
            for ( int i = 0; i < result.Length; i++ )
            {
                result[ i ] = pairs.First( x => x.Key.Equals( columnNames[ i ] ) ).Value;
            }

            return result;
        }

        private static Func<string, object> GetConversionFunc( Type type )
        {
            if ( Conversions.ContainsKey( type ) )
            {
                return Conversions[ type ];
            }

            var parentType = Conversions.Keys.First( k => k.IsAssignableFrom(type) );
            return Conversions[ parentType ];
        }

        private static object Convert( Type type, string arg )
        {
            var convertFunc = GetConversionFunc( type );
            return convertFunc( arg );
        }

        public TSVDatabaseFile( Stream stream ) : base( stream )
        {
            ColumnIndexes = GetColumnIndexes( typeof( TEntity ), ColumnNames );
        }

        public TSVDatabaseFile( string filepath ) : base( filepath )
        {
            ColumnIndexes = GetColumnIndexes( typeof( TEntity ), ColumnNames );
        }

        public TSVDatabaseFile( byte[] buffer ) : base( buffer )
        {
            ColumnIndexes = GetColumnIndexes( typeof( TEntity ), ColumnNames );
        }

        private TEntity CreateEntity( string[] row )
        {
            var entity  = new TEntity();
            for ( int i = 0; i < row.Length; i++ )
            {
                ColumnIndexes[ i ].SetValue( entity, Convert( ColumnIndexes[ i ].PropertyType, row[ i ] ) );
            }

            return entity;
        }

        public new IEnumerable<TEntity> GetAll()
        {
            foreach ( var row in base.GetAll() )
            {
                yield return CreateEntity( row );
            }
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
