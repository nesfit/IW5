# Cvičení 04

## Použití Scrutoru:
    1. Nainstalovat do všech projektů NuGet Scrutor
    2. Upravit `BLInstaller` a `DALInstaller`, aby používali metodu `Scan`

Kód pro `BLInstaller`
```csharp
serviceCollection.Scan(selector =>
    selector.FromCallingAssembly()
            .AddClasses(classes => classes.AssignableTo<IAppFacade>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime());
```