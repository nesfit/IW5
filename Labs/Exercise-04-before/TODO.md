# Cvičení 04

## Použití Scrutoru:
    1. Nainstalovat do všech projektů NuGet Scrutor
    2. Upravit `BLInstaller` a `DALInstaller`, aby používali metodu `Scan`
    3. Spustit aplikaci a ověřit funkčnost

Kód pro `BLInstaller`
```csharp
serviceCollection.Scan(selector =>
    selector.FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo<IAppFacade>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime());
```

## Logování

### Logování samotné
    1. Vytvoření `LoggingController` s API metodou `LogSomething`
    2. Vytvořit konstruktor s parametrem `ILogger<LoggingController>` a ten si uložit do privátního fieldu
    3. Do metody `LogSomething` přidat zalogování informace
