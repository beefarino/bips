using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOwls.BIPS.Utility
{
    class PackageCache
    {
        readonly IDictionary<string, PackageDescriptor> _cache;
        readonly BipsDrive _drive;

        public PackageCache(BipsDrive drive)
        {
            _drive = drive;
            _cache = new Dictionary<string, PackageDescriptor>();
        }

        public PackageDescriptor GetPackage(string path)
        {
            path = path.ToLowerInvariant();
            if (!_cache.ContainsKey(path))
            {
                var package = _drive.Application.LoadPackage(path, null);
                var descriptor = new PackageDescriptor(package, path);
                _cache.Add(path, descriptor);
            }
            return _cache[path];
        }

        public string GetKey(FileInfo f)
        {
            return f.FullName;
        }
    }
}
