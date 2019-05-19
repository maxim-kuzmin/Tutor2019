cd .\bin\Debug\netcoreapp2.2

### Sample 01

SET ASPNETCORE_ENVIRONMENT=Development

dotnet Tutor2019.Apps.NotificationSignalR.dll sample-01-server

dotnet Tutor2019.Apps.NotificationSignalR.dll sample-01-producer

dotnet Tutor2019.Apps.NotificationSignalR.dll sample-01-consumer
