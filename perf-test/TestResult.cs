using System;
using System.Collections.Generic;
using System.Text;

namespace PerfTest
{
    public class TestResult
    {
        public List<EndpointCallsResult> EndpointResults { get; set; } = new List<EndpointCallsResult>();

        public TimeSpan TotalTestTime { get; set; }
    }

    public class EndpointCallsResult
    {
        public EndpointCallsResult(string endpoint)
        {
            Endpoint = endpoint;
        }

        public string Endpoint { get; set; }
        public int RequestErrors { get; set; }

        public TimeSpan AverageRequestTime { get; set; }

        public TimeSpan MaxRequestTime { get; set; }

        public TimeSpan MinRequestTime { get; set; }

        public TimeSpan TotalTime { get; set; }

        public double RequestPerSecond { get; set; }
    }
}
