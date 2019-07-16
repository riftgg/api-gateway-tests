using System;
using System.Collections.Generic;
using System.Text;

namespace PerfTest
{
    public class Constants
    {
        public const string OCELOT_BASE_URL = "http://localhost:8081";
        public const string KRAKEND_BASE_URL = "http://localhost:8082";
        public const string ENVOY_BASE_URL = "http://localhost:8083";

        public const int REQUESTS_PER_ENDPOINT = 5000;
    }
}
