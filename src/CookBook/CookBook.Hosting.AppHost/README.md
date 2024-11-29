# .NET Aspire AppHost

## Install / Restore
```sh
dotnet workload install aspire
dotnet tool install -g aspirate
```

```sh
dotnet workload update
dotnet tool restore
```

## Generate output
Based on already existing `aspirate-state.json` this command will generate a Docker Compose file along with program images.
These will be stored locally.

```shell
aspirate generate
```
