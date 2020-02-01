## Configuring your console application
Refer to the sample project: `Examples/WorkerServiceConsole`

### Update or add configuration files
Ensure you have appsettings.json and appsettings.development.json in the Configuration/Settings folder with the Application Insights instrumentation key defined.

``appsettings.json``
```
{
  "ApplicationInsights": {
    "InstrumentationKey": "[Enter Key]"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug"
      ...
    }
  }
}

```

### Set your runtime environment

- Under the projects `Properties -> Debug -> Environment Variables`
    - Set the name to 'DOTNET_ENVIRONMENT' and value to 'Development'
