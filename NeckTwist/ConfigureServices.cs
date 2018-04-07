


services.AddIdentityServer()
.AddInMemoryClients(new List<Client>())
    .AddInMemoryIdentityResources(new List<IdentityResource>())
    .AddInMemoryApiResources(new List<ApiResource>())
    .AddTestUsers(new List<TestUser>())
    .AddDeveloperSigningCredential();