using System;
using System.Linq;
using System.Reflection;

namespace ConfigEx
{
    internal static class AssemblyLocator
    {
        public static Assembly GetEntryAssembly()
        {
            // Entry assembly is null in the ASP.NET projects. In this case we can mark the right assembly
            // with the MainConfigAssemblyAttribute and find it by the attribute.
            return Assembly.GetEntryAssembly() ?? GetAssemblyWithAttribute();
        }

        private static Assembly GetAssemblyWithAttribute()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var entryAssemblies = from assembly in assemblies
                                  let attribute = assembly.GetCustomAttributes(typeof(MainConfigAssemblyAttribute)).SingleOrDefault()
                                  where attribute != null
                                  select assembly;

            return entryAssemblies.SingleOrDefault();
        }
    }
}