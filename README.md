# Prerequirements

In `appsettings.json` :

You must set `ConnectionStrings.MSSQL_Connection`'s value to Microsoft SQL Server conncetion string. For Example: 
```
"ConnectionStrings": {
    "MSSQL_Connection": "Server=YOUR_SERVER;Database=Spaceship;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
  }
```

You must set `WeatherServiceGuid`'s value as the unique id you got from `https://webhook.site/`. For Example if your request address is `https://webhook.site/#!/848b5364-302b-4816-af3b-585c6626a30f`, then the value should look like below:
```
"WeatherServiceGuid": "848b5364-302b-4816-af3b-585c6626a30f"
```

You can change database default timeout with `DatabaseTimeoutSeconds`. The default value is 1:
```
"DatabaseTimeoutSeconds": 1
```

You can change http calls default timeout with `HttpTimeoutSeconds`. The default value is 2:
```
"HttpTimeoutSeconds": 2
```

# How to run
Go to the root of the project and run `dotnet run` command from cmd.
