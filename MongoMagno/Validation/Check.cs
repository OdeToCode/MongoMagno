using System;
using System.Web.Caching;

namespace MongoMagno
{
    public static class Check
    {       
        public static void ArgNotNull(object @object, string name)
        {
            if (@object == null)
            {
                throw new ArgumentNullException(String.Format("{0} cannot be null", name));
            }
        }

        public static bool NotNull(object @object, string name)
        {
            if (@object == null)
            {
                throw new InvalidOperationException(String.Format("{0} cannot be null", name));
            }
            return true;
        }
    }
}