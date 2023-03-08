`appsettings.json` 또는 `appsettings.Development.json`에 로깅 옵션을 설정한다.
```json
  // https://github.com/serilog/serilog-settings-configuration
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console" ],
    "MinimumLevel": "Debug",
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "C:\\EumLog\\test.txt",
          "rollingInterval": "Day",
          "outputTemplate": "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] [{EventId}] {Message}{NewLine}{Exception}"
        }
      }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ],
    "Destructure": [
      {
        "Name": "ToMaximumDepth",
        "Args": { "maximumDestructuringDepth": 4 }
      },
      {
        "Name": "ToMaximumStringLength",
        "Args": { "maximumStringLength": 100 }
      },
      {
        "Name": "ToMaximumCollectionCount",
        "Args": { "maximumCollectionCount": 10 }
      }
    ],
    "Properties": {
      "Application": "Sample"
    }
  },
```


`UseEumLogging()` 확장 함수를 호출해서 로깅 환경을 구성할 수 있다.
```c#
builder.Host.UseEumLogging();
var app = builder.Build();

var logger = app.Services.GetService<ILogger<Program>>();
logger!.LogInformation("Test!!");
```
