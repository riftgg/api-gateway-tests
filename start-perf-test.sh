#!/bin/sh
echo "Cleaning old resources"
rm results-krakend
rm results-ocelot
rm -rf perf-dashboard-ocelot
rm -rf perf-dashboard-krakend
echo "Executing docker-compose in detach mode"
docker-compose up --build --detach
echo "starting ocelot performance test using jmeter"
$1 -n -t api-gateway-perf-test.jmx -l results-ocelot -e -o perf-dashboard-ocelot -JPort=8081
echo "you can view ocelot results in perf-dashboard-ocelot folder"
echo "starting krakend performance test using jmeter"
$1 -n -t api-gateway-perf-test.jmx -l results-krakend -e -o perf-dashboard-krakend -JPort=8082
echo "you can view krakend results in perf-dashboard-krakend folder"
echo "\n\nStarting .net core perf test"
dotnet run --project perf-test/PerfTest.csproj
echo "stopping docker compose"
docker-compose stop