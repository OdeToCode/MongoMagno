using System;

namespace MongoMagno
{
    public static class Check
    {
         public static bool ThrowIfNull(this object @object, string name)
         {
             if (@object == null)
             {
                 throw new ArgumentNullException(String.Format("{0} cannot be null", name));
             }
             return true;
         }
    }
}