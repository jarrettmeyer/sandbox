using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Sandbox.NHib
{
    /// <summary>
    /// Some friendly helpers methods to make working with <see cref="NHibernate.ISession"/>
    /// instances just a little bit easier.
    /// </summary>
    public static class ISessionExtensions
    {
        public static IEnumerable<T> All<T>(this ISession session)
        {
            return session.Query<T>().ToArray();
        }
    }
}
