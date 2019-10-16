# hostel
For run application, you need to 
1) configure the connection string in hotel/Administration/Administration.API/appsettings.json
2) run update-database to two contexts AdministrationContext and AppIdentityContext
3) run application by iis express
4) register user by api
5) sign in and copy response token to swagger authorization like "Bearer {token}"
