using System;
using System.Linq;
using System.Reflection;

namespace Nubank.Tools
{
    public class ReflectionTools
    {
        public static IQueryable<Type> GetAllImplementations<T>()
        {
            return Assembly.GetAssembly(typeof(T)).GetTypes().Where(t => typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).AsQueryable();
        }
    }
}
