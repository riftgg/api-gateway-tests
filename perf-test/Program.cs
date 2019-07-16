using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PerfTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Starting Performance Tests!");
            Console.WriteLine("Starting Ocelot performance test");
            var ocelotTestResult = await StartPerfTest(Constants.OCELOT_BASE_URL);
            Console.WriteLine("Ocelot test finished");
            Console.WriteLine("Starting Krakend performance test");
            var krakendTestResult = await StartPerfTest(Constants.KRAKEND_BASE_URL);
            Console.WriteLine("Krakend test finished");
            Console.WriteLine("Starting Envoy performance test");
            var envoyTestResult = await StartPerfTest(Constants.KRAKEND_BASE_URL);
            Console.WriteLine("Envoy test finished");
            Console.WriteLine("\n\n");
            Console.WriteLine("OCELOT RESULTS:");
            WriteTestsResults(ocelotTestResult);
            Console.WriteLine($"\n\nKRAKEND RESULTS:");
            WriteTestsResults(krakendTestResult);
            Console.WriteLine($"\n\nENVOY RESULTS:");
            WriteTestsResults(envoyTestResult);
        }

        static void WriteTestsResults(TestResult result)
        {
            Console.WriteLine($"\nTotalTime: {result.TotalTestTime.TotalSeconds} seconds");
            foreach (var endpointResult in result.EndpointResults)
            {
                Console.WriteLine($"\nResults for endpoint {endpointResult.Endpoint}");
                Console.WriteLine($"\tTotal: {endpointResult.TotalTime.ToString(@"mm\:ss\.ffff")}");
                Console.WriteLine($"\tSlowest: {endpointResult.MaxRequestTime.ToString(@"ss\.ffff")}");
                Console.WriteLine($"\tFastest: {endpointResult.MinRequestTime.ToString(@"ss\.ffff")}");
                Console.WriteLine($"\tAverage: {endpointResult.AverageRequestTime.ToString(@"ss\.ffff")}");
                Console.WriteLine($"\tRequests/sec: {endpointResult.RequestPerSecond}");
                Console.WriteLine($"\tRequest errors: {endpointResult.RequestErrors}");
            }
        }

        static async Task<TestResult> StartPerfTest(string baseurl)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            var perfTest = new PerfTest(client);
            return await perfTest.StartPerfTest();
        }
    }
}
