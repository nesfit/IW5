# Example continuous integration/delivery

This example shows how to use large application configuration using .net, nuget packages, powershell and msbuild.

* nuget.config - for general nuget configuraion
* Directory.Packages.props - example global packages versioning
* Build directory - for commandline build scripts using dotnet cli
* *.csproj files - Project dependencies, target framework and package references
* Console\ConsoleCalculator\Properties\PublishProfiles\ - publish profiles for console application with platform properties and selfcontained package


## General notes

* Teh concrete application source code is not relevant for our examples
* Visual studio has to be installed on the build agent like usually and have to correspond with the build solution file step and also .net inspections step tooling.
* The driver servers for each browsers also have to be available in output directory, we reference them as links in the test project file
* Nunit SingleThread apartment has to be defined using assembly attribute, because test runner doesn't have to load values from app.config.
* To be able get the Acceptance tests running the build agent has to be started using batch file not as a service to get full access to currently logged on user UI. How make it run, if not logged on? no way. (The agent service has to be configured to run under user account - not enough, it doesn't get full UI)
* To be able collect code coverage using dotCover, .Net 3.5 has to be installed.
* The coverage filter rules: has to start with +: ,provided name has to be without path and extension
* The release MVC output doesn't contain all libraries like in debug: The test project has to reference MVC nuget package
* nice post about automatic deployment http://www.troyhunt.com/2010/11/you-deploying-it-wrong-teamcity_26.html
http://www.asp.net/web-forms/overview/deployment/web-deployment-in-the-enterprise/deploying-web-packages
* the web application has to be configured on the target IIS server and its application poll has to be configured with .net v4.0
* Because of solution split, Project in main solution are customized to don't reference engine as project, instead as an compiled assembly from output directory. Solution dependencies and order were adjusted to keep the correct build order.
* To Split the solution the nuget repository was redirected to the top level directory. See https://docs.nuget.org/consume/nuget-config-file
