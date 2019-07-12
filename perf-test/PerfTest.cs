using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfTest
{
    public class PerfTest
    {
        HttpClient client;
        IEnumerable<string> endPointsToTest;

        public PerfTest(HttpClient client)
        {
            this.client = client;
            endPointsToTest = new List<string>
            {
                "/api/users",
                "/api/users/1",
                "/api/users/2",
                "/api/products",
                "/api/products/1",
                "/api/products/2"
            };
        }

        public async Task<TestResult> StartPerfTest()
        {
            TestResult testResult = new TestResult();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<Task<EndpointCallsResult>> tests = new List<Task<EndpointCallsResult>>();
            foreach (var endpoint in endPointsToTest)
            {
                tests.Add(TestEndpoint(endpoint));
            }
            var results = await Task.WhenAll(tests);
            foreach (var result in results)
            {
                testResult.EndpointResults.Add(result);
            }
            stopWatch.Stop();
            testResult.TotalTestTime = stopWatch.Elapsed;
            return testResult;
        }

        public async Task<EndpointCallsResult> TestEndpoint(string endpoint)
        {
            EndpointCallsResult result = new EndpointCallsResult(endpoint);
            List<TimeSpan> times = new List<TimeSpan>();
            int requestErrors = 0;
            Stopwatch stopWatch = new Stopwatch();
            for (int i = 0; i < Constants.REQUESTS_PER_ENDPOINT; i++)
            {
                stopWatch.Restart();
                try
                {
                    await client.GetStringAsync(endpoint);
                }
                catch (HttpRequestException ex)
                {
                    requestErrors++;
                    Console.WriteLine($"Error response in endpoint {endpoint} request number {ex}");
                }
                stopWatch.Stop();
                times.Add(stopWatch.Elapsed);
            }
            result.RequestErrors = requestErrors;
            result.MinRequestTime = times.Min();
            result.MaxRequestTime = times.Max();
            result.TotalTime = TimeSpan.FromTicks(times.Sum(t => t.Ticks));
            result.AverageRequestTime = TimeSpan.FromTicks(result.TotalTime.Ticks / Constants.REQUESTS_PER_ENDPOINT);
            result.RequestPerSecond = Constants.REQUESTS_PER_ENDPOINT / result.TotalTime.TotalSeconds;
            return result;
        }
    }
}
