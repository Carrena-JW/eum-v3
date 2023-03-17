


## Deployment

프로젝트 빌드

```sh
dotnet publish -c Release -o ./publish
```

윈도우즈 서비스 설치

```sh
sc create MyService binPath=C:\EumServer\Eum.gRPC.Server.ServiceDesk.exe
```

