using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sample.Reflection
{
    public static class Program
    {
        public static HashSet<Assembly> GetAllAssemblies()
        {
            var assemblies = new HashSet<Assembly>();
            var queue = new Queue<Assembly>();
            queue.Enqueue(Assembly.GetExecutingAssembly());

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                assemblies.Add(current);
                var references = current.GetReferencedAssemblies()
                    .Select(Assembly.Load);
                foreach (var reference in references)
                {
                    queue.Enqueue(reference);
                }
            }
            return assemblies;
        }

        public static string GetMemberKind(MemberInfo info)
        {
            switch(info)
            {
                case Type type:
                    return "type";
                case PropertyInfo property:
                    return "property";
                case ConstructorInfo constructor:
                    return "constructor";
                case MethodInfo method:
                    return "method";
                case FieldInfo field:
                    return "field";
                case EventInfo @event:
                    return "event";
                default:
                    return "unknown";
            }
        }

        public static IEnumerable<string> SearchMembers(IEnumerable<Assembly> assemblies, string searchTerm)
        {
            var results = new HashSet<string>();

            var types = assemblies.SelectMany(a => a.GetTypes())
                .ToHashSet();
            foreach (var type in types)
            {
                if (type.FullName.Contains(searchTerm))
                {
                    results.Add($"type: {type.FullName}");
                }

                var members = type.GetMembers()
                    .Where(t => t.Name.Contains(searchTerm));
                foreach (var member in members)
                {
                    results.Add($"{GetMemberKind(member)}: {member.DeclaringType.FullName}.{member.Name}");
                }
            }
            return results;
        }

        public static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: netapi <SEARCH_TERM>");
                return 1;
            }
            var assemblies = GetAllAssemblies();
            var results = SearchMembers(assemblies, args[0])
                .OrderBy(s => s);
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
            return 0;
        }
    }
}
