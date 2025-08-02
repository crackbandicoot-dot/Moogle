using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoogleEngine.Utils
{
    internal static class Utils
    {
        public static IEnumerable<string> GetFilesRoutes(string dirRoute)
        {
            var allowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".txt", ".pdf" };

            var filesRoutes = Directory.EnumerateFiles(dirRoute, "*.*", SearchOption.AllDirectories)
                .Where(file => allowedExtensions.Contains(Path.GetExtension(file)));

            return filesRoutes;


        }
    }
}
