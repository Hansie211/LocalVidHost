using System;
using Database.General.Interfaces;

namespace Database.General
{
    public abstract class DatabaseRecord
    {
        public Guid ID { get; set; }

        #region Equality Checks

        public override bool Equals( object obj )
        {
            if ( obj is null )
            {
                return false;
            }

            if ( !GetType().IsAssignableFrom( obj.GetType() ) && !obj.GetType().IsAssignableFrom( GetType() ) )
            {
                return false;
            }

            return ID.Equals( ( obj as DatabaseRecord ).ID );
        }

        public static bool operator ==( DatabaseRecord a, DatabaseRecord b )
        {
            if ( a is null )
            {
                return b is null;
            }

            return a.Equals( b );
        }

        public static bool operator !=( DatabaseRecord a, DatabaseRecord b )
        {
            return !( a == b );
        }

        public override string ToString()
        {
            return base.ToString() + " - " + ID.ToString();
        }

        public override int GetHashCode()
        {
            return -1937169414 + ID.GetHashCode();
        }

        #endregion
    }
}
