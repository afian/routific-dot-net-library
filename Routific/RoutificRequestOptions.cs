using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routific
{
    public class RoutificRequestOptions
    {
        public string ApiKey { get; set; }
        internal string RoutificVersion { get; set; }
    }

    public static class baseConfig
    {

        public static string url = "https://api.routific.com";
        public static int pollDelay = 3000;
        public static int version = 1;

    }

    public class Configuration
    {
        public string Url { get; set; } = "https://api.routific.com";
        public int pollDelay { get; set; } = 1000;
        public int Version { get; set; } = 1;
        public string Token { get; set; }
    }
}
