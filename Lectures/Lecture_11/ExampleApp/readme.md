# Example continuous integration/delivery

This example shows how to use large application configuration using .net, nuget packages, powershell and msbuild.

* nuget.config - for general nuget configuraion
* Directory.Packages.props - example global packages versioning
* Build directory - for commandline build scripts using dotnet cli
* *.csproj files - Project dependencies, target framework and package references
* Console\ConsoleCalculator\Properties\PublishProfiles\ - publish profiles for console application with platform properties and selfcontained package


## General notes

* Teh concrete application source code is not relevant for our examples
* Dotnet Sdk needs to be installed in the build agent.
* The Chrome driver needs to be up to date to reflect currently installed version
* Nunit SingleThread apartment has to be defined using assembly attribute, because test runner doesn't have to load values from app.config.
* To be able get the Acceptance tests running the build agent has to be started using batch file not as a service to get full access to currently logged on user UI. How make it run, if not logged on? no way. (The agent service has to be configured to run under user account - not enough, it doesn't get full UI)
* The release MVC output doesn't contain all libraries like in debug: The test project has to reference MVC nuget package
* nice post about automatic deployment http://www.troyhunt.com/2010/11/you-deploying-it-wrong-teamcity_26.html
http://www.asp.net/web-forms/overview/deployment/web-deployment-in-the-enterprise/deploying-web-packages
* Because of solution split, Project in main solution are customized to don't reference engine as project, instead as an compiled assembly from output directory. Solution dependencies and order were adjusted to keep the correct build order.
