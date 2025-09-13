using DependencyInjection.Services;
using System;


RandomNumberService randomNumberService;
AdditionService additionService;
MultiplicationService multiplicationService;

#region No Dependency Injection
randomNumberService = new RandomNumberService();
additionService = new AdditionService();
multiplicationService = new MultiplicationService();
#endregion

#region All Initializations in 1 place
//randomNumberService = new RandomNumberService(new ConsoleLogger());
//additionService = new AdditionService(new ConsoleLogger());
//multiplicationService = new MultiplicationService(new ConsoleLogger());
#endregion

#region Dependency Injection Basics
//var serviceCollection = new ServiceCollection();
//serviceCollection.AddSingleton<ConsoleLogger>();

//var serviceProvider = serviceCollection.BuildServiceProvider();

//randomNumberService = new RandomNumberService(serviceProvider.GetRequiredService<ConsoleLogger>());
//additionService = new AdditionService(serviceProvider.GetRequiredService<ConsoleLogger>());
//multiplicationService = new MultiplicationService(serviceProvider.GetRequiredService<ConsoleLogger>());
#endregion

#region FileLogger
//var serviceCollection = new ServiceCollection();
//serviceCollection.AddSingleton<ILogger, ConsoleLogger>();
////serviceCollection.AddSingleton<ILogger>(new FileLogger("output.log"));

//var serviceProvider = serviceCollection.BuildServiceProvider();

//randomNumberService = new RandomNumberService(serviceProvider.GetRequiredService<ILogger>());
//additionService = new AdditionService(serviceProvider.GetRequiredService<ILogger>());
//multiplicationService = new MultiplicationService(serviceProvider.GetRequiredService<ILogger>());
#endregion

#region Resolving properly
//var serviceCollection = new ServiceCollection();
//serviceCollection.AddSingleton<ILogger, ConsoleLogger>();
//serviceCollection.AddSingleton<RandomNumberService>();
//serviceCollection.AddSingleton<AdditionService>();
//serviceCollection.AddSingleton<MultiplicationService>();

//var serviceProvider = serviceCollection.BuildServiceProvider();

//randomNumberService = serviceProvider.GetRequiredService<RandomNumberService>();
//additionService = serviceProvider.GetRequiredService<AdditionService>();
//multiplicationService = serviceProvider.GetRequiredService<MultiplicationService>();
#endregion

#region Lifetime management
//var serviceCollection = new ServiceCollection();
//serviceCollection.AddTransient<ILogger, ConsoleLogger>();
//serviceCollection.AddSingleton<RandomNumberService>();
//serviceCollection.AddSingleton<AdditionService>();
//serviceCollection.AddSingleton<MultiplicationService>();

//var serviceProvider = serviceCollection.BuildServiceProvider();

//randomNumberService = serviceProvider.GetRequiredService<RandomNumberService>();
//additionService = serviceProvider.GetRequiredService<AdditionService>();
//multiplicationService = serviceProvider.GetRequiredService<MultiplicationService>();
#endregion

#region Scrutor
//var serviceCollection = new ServiceCollection();
//serviceCollection.AddSingleton<ILogger, ConsoleLogger>();
//serviceCollection.Scan(scan => scan
//    .FromAssemblyOf<Program>()
//    .AddClasses(classes => classes.AssignableTo<IService>())
//    .AsSelf()
//    .WithSingletonLifetime());

//var serviceProvider = serviceCollection.BuildServiceProvider();

//randomNumberService = serviceProvider.GetRequiredService<RandomNumberService>();
//additionService = serviceProvider.GetRequiredService<AdditionService>();
//multiplicationService = serviceProvider.GetRequiredService<MultiplicationService>();
#endregion

#region Castle Windsor
//var serviceCollection = new ServiceCollection();
//serviceCollection.AddSingleton<ILogger, ConsoleLogger>();
//serviceCollection.Scan(scan => scan
//    .FromAssemblyOf<Program>()
//    .AddClasses(classes => classes.AssignableTo<IService>())
//    .AsSelf()
//    .WithSingletonLifetime());

//var serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(new WindsorContainer(), serviceCollection);

//randomNumberService = serviceProvider.GetRequiredService<RandomNumberService>();
//additionService = serviceProvider.GetRequiredService<AdditionService>();
//multiplicationService = serviceProvider.GetRequiredService<MultiplicationService>();
#endregion

var generatedNumbers = randomNumberService.Generate(10);
var additionResult = additionService.Add(generatedNumbers);
var multiplicationResult = multiplicationService.Multiply(generatedNumbers);

Console.WriteLine($"Addition result: {additionResult}");
Console.WriteLine($"Multiplication result: {multiplicationResult}");
