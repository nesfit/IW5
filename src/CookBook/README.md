# CookBook

## Build & Run

### Docker

#### API

##### Build
```bash
docker build -f CookBook.Api.App/Dockerfile . --progress=plain -t cookbook.api.app
```
##### Run

```bash
docker run -p 8081:8080 cookbook.api.app
```

```bash
dotnet publish --os linux --arch x64 /t:PublishContainer -c Release # not tested
dotnet publish --os osx --arch arm64 /t:PublishContainer -c Release # currently broken
```