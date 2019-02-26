using System.Collections.Generic;
using System.Linq;

namespace Polliade
{
    internal class AppSettings
    {
        public static string Tenant = "";
        public static string TentantRedirectUrl = "";
        public static string ClientID = "";
        public static string PolicyX = "";
        public static IEnumerable<string> Scopes = Enumerable.Empty<string>();
    }
}
