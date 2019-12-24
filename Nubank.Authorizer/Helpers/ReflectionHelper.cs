using System;
using System.Linq;
using System.Reflection;

namespace Nubank.Authorizer.Helpers
{
    public class ReflectionHelper
    {
        /// <summary>
        /// Retrieves all (non abstract) implementations of an interface
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IQueryable<Type> GetAllImplementations<T>()
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).AsQueryable();
        }
    }
}
