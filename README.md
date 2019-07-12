# api-gateway-tests

Performance tests for ocelot and krakend api gateways. To do the performance test the project consist of:

* Two ASP.NET core 2.2 Web API backends with simple GET endpoints: /api/users, /api/users/{userId}, /api/products and /api/products/{productId}
  
* One simple ocelot API gateway in ASP.NET core 2.2 that "proxies" to User backend or Product backend dependening on the endpoint prefix

* One simple krakend.json configuration that does the proxy to the backends

* One docker compose file to build and create the docker images and executes the docker containers

* One performance test client in .NET core to test the ocelot and krakend API gateways

* One JMeter file already configured to do the performance test and get a pretty dashboard using jmeter

* One bash script that executes the docker-compose, executes the jmeter performance tests and executes the .NET core performance tests.

## how to run the performance tests

### Requirements

* docker & docker-compose
* .NET core 2.2 sdk (to run the .NET core performance test)
* JMeter

### Run the script

Execute start-perf-test.bat/sh passing the jmeter as a full path (the following command lines has been executed in a windows machine):

````
start-perf-test.bat C:\apache-jmeter\bin\jmeter.bat
````

````
sh start-perf-test.sh /c/apache-jmeter/bin/jmeter.bat
````