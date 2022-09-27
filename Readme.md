# OpenFibu

> *Doing it all over again*

## Getting Started

We provide a wpf application and an asp.net core API. Both need the same prerequisites.

The API project is using the Configuration pattern of the GenericHostBuilder, so it allows configuration via appsettings.json and [dotnet user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows).

### Prerequisites

#### Database

Get a RavenDB server from https://ravendb.net/
 - download the server and install yourself
 - use the docker container from https://ravendb.net/docs/article-page/5.4/csharp/start/installation/running-in-docker-container
 - use the cloud service (there is a free plan)
 - if you use the secure version you have to deal with the certificate. for development i recommend the unsecure installation
 - Configure the connection to your database
   - with wpf you can rewrite the configuration in the `DocumentStoreHolder` in the `OpenFibu.Data` project.
   - in the API project you can provide the configuration as appsettings, e.g.
     ```json
     {
       "DocumentStoreSettings" : {
         "DatabaseName": "OpenFibu",
         "Urls": [
           "http://localhost:8080"
         ]
       } 
     }
     ```
 
If you want to try the app without a database you can use the mocks provided in OpenFibu.Data.Mocks.
To use them in the wpf project just comment change the used repo implementations in the App.xaml.cs file.
For the API project you can provide a `UseMockDocumentStore = true` via app configuration.

## Contribute
