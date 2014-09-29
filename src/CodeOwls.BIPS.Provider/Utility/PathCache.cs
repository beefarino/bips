using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeOwls.PowerShell.Provider.PathNodes;

namespace CodeOwls.BIPS.Utility
{
    static class PathCache
    {
        static readonly IDictionary<string, IEnumerable<INodeFactory>> ResolvedPaths = new Dictionary<string, IEnumerable<INodeFactory>>(StringComparer.InvariantCultureIgnoreCase);

        public static void Add(string path, IEnumerable<INodeFactory> item)
        {
            ResolvedPaths[path] = item;
        }

        public static IEnumerable<INodeFactory> Get(string path)
        {
            IEnumerable<INodeFactory> o;

            if (!ResolvedPaths.TryGetValue(path, out o))
            {
                return null;
            }

            return o;
        }

        public static void Clear()
        {
            ResolvedPaths.Clear();
        }
    }
}
