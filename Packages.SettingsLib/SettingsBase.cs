using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Packages.SettingsLib
{
    public abstract class SettingsBase
    {
        private IEnumerable<PropertyInfo> ListProperties()
        {
            var properties = this.GetType().GetProperties();
            foreach ( var property in properties )
            {

                if ( property.IsDefined( typeof(NotMappedAttribute) ) )
                {
                    continue;
                }

                TypeCode typeCode = Type.GetTypeCode( property.PropertyType );
                switch ( typeCode )
                {
                    case TypeCode.Empty:
                    case TypeCode.Object:
                    case TypeCode.DBNull:

                        continue;
                }

                yield return property;
            }
        }

        private object Convert( JsonElement elem, TypeCode typeCode )
        {
            if ( elem.ValueKind == JsonValueKind.Null )
            {
                return null;
            }

            switch ( typeCode )
            {
                case TypeCode.Boolean:
                    return elem.GetBoolean();

                case TypeCode.Char:
                    return elem.GetString()?.First();

                case TypeCode.SByte:
                    return elem.GetSByte();

                case TypeCode.Byte:
                    return elem.GetByte();

                case TypeCode.Int16:
                    return elem.GetInt16();

                case TypeCode.UInt16:
                    return elem.GetUInt16();

                case TypeCode.Int32:
                    return elem.GetInt32();

                case TypeCode.UInt32:
                    return elem.GetUInt32();

                case TypeCode.Int64:
                    return elem.GetInt64();

                case TypeCode.UInt64:
                    return elem.GetUInt64();

                case TypeCode.Single:
                    return elem.GetSingle();

                case TypeCode.Double:
                    return elem.GetDouble();

                case TypeCode.Decimal:
                    return elem.GetDecimal();

                case TypeCode.DateTime:
                    return elem.GetDateTime();

                case TypeCode.String:
                    return elem.GetString();


                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.DBNull:
                default:
                    return null;
            }
        }

        internal void LogException( Exception exp )
        {
            Console.Error.WriteLine( "!!!ERROR!!!" );
            Console.Error.WriteLine( exp.ToString() );
        }

        internal void Clear()
        {
            foreach ( var property in ListProperties() )
            {
                property.SetValue( this, null );
            }
        }

        internal void ImportValues( Dictionary<string, JsonElement> values )
        {
            foreach ( var property in ListProperties() )
            {
                if ( !values.ContainsKey( property.Name ) )
                {
                    continue;
                }

                try
                {
                    TypeCode typeCode   = Type.GetTypeCode( property.PropertyType );
                    object value        = Convert( values[property.Name], typeCode );

                    property.SetValue( this, value );
                }
                catch ( Exception exp )
                {
                    LogException( exp );
                    continue;
                }
            }

            AfterImport();
        }

        internal Dictionary<string, object> ExportValues()
        {
            var result = new Dictionary<string, object>();
            foreach ( var property in ListProperties() )
            {
                object value = property.GetValue( this );
                if ( value is null )
                {
                    continue;
                }

                result.Add( property.Name, value );
            }

            return result;
        }

        protected virtual void AfterImport()
        {

        }
    }
}
