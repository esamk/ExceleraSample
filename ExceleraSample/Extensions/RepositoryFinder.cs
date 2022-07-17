using Microsoft.Extensions.DependencyInjection;
using SampleDB.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExceleraSample.Extensions
{
    public static class RepositoryFinder
    {
        public static void AddRepositories(
            this IServiceCollection serviceCollection,
            Assembly assembly)
        {
            var types = assembly
                .GetTypes()
                .Where(ti => ti.GetInterfaces().Contains(typeof(IRepository)) &&
                             ti.IsClass == true &&
                             ti.IsAbstract == false);
                
            foreach (var ti in types)
            {
                var interfaces = ti.GetInterfaces();
                var iRepo = interfaces.FirstOrDefault(i => 
                                !i.Name.Contains(nameof(IRepository)) &&
                                !i.Name.Contains("IReadOnlyRepository") &&
                                !i.Name.Contains("IReadWriteRepository"));
                serviceCollection.AddTransient(iRepo, ti);
            }
        }
    }
}
