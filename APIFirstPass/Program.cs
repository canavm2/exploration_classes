using APIMethods;
using FileTools;
using Relation;
using People;
using Company;
using Users;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Microsoft.OpenApi.Models;

#region app builder
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "This is effectively the client for now.",
    Title = "Exploration Game",
    Version = "v1",
    Contact = new OpenApiContact()
    {
        Name = "anuraj",
        Url = new Uri("https://dotnetthoughts.net")
    }
}));
var app = builder.Build();
// Configure the HTTP request pipeline.
string azureUri = builder.Configuration["AzureCosmos:URI"];
string azureKey = builder.Configuration["AzureCosmos:PrimaryKey"];
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    azureUri = builder.Configuration["AzureCosmos:URI"];
    azureKey = builder.Configuration["AzureCosmos:PrimaryKey"];
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    builder.Configuration.AddAzureKeyVault(new Uri("https://explorationgametesting.vault.azure.net/"), new DefaultAzureCredential());
    azureUri = builder.Configuration["azureUri"];
    azureKey = builder.Configuration["PrimaryKey"];
    //I used to use these to manually supply the access info
    //azureUri = "REPLACE";
    //azureKey = "REPLACE";
}
app.UseHttpsRedirection();
#endregion

#region variable loading
//keyvault stuff that doesnt work.  I think it will only work in the development env, and isnt working in the deployment, but it works above
//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
#endregion

#region dataloading
FileTool fileTool = new FileTool(azureUri, azureKey);
LoadTool loadTool = await fileTool.ReadLoadTool(new Guid("3f53a424-601c-4c7e-a19d-3ead86aa15dc"));
//LoadTool loadTool = new();
//loadTool.RelationshipCacheId = new Guid("4938643c-31b8-4e8c-9ff8-0816c09904da");
#endregion

Boolean NewData = false;

#region Citizen Loading
CitizenCache citizenCache;
if (NewData)
{
    citizenCache = new CitizenCache(100);
    Console.WriteLine($"femalecitizens has: {citizenCache.FemaleCitizens.Count} items.\nThe first female is:\n{citizenCache.FemaleCitizens[0].Describe()}");
    Console.WriteLine($"malecitizens has: {citizenCache.MaleCitizens.Count} items.\nThe first male is:\n{citizenCache.MaleCitizens[0].Describe()}");
    Console.WriteLine($"nbcitizens has: {citizenCache.NBCitizens.Count} items.\nThe first non-binary is:\n{citizenCache.NBCitizens[0].Describe()}");
    loadTool.CitizenCacheId = citizenCache.id;
}
else citizenCache = await fileTool.ReadCitizens(loadTool.CitizenCacheId);
#endregion

#region User Loading
UserCache userCache;
if (NewData)
{
    userCache = new();
    loadTool.UserCacheId = userCache.id;
}
else userCache = await fileTool.ReadUsers(loadTool.UserCacheId);
#endregion

#region Company Loading
CompanyCache companyCache;
if (NewData)
{
    companyCache = new();
    loadTool.CompanyCacheId = companyCache.id;
}
else companyCache = await fileTool.ReadCompanies(loadTool.CompanyCacheId);
#endregion

#region Relationship Loading
RelationshipCache relationshipCache;
if (NewData)
{
    relationshipCache = new RelationshipCache();
    loadTool.RelationshipCacheId = relationshipCache.id;
}
else relationshipCache = await fileTool.ReadRelationshipCache(loadTool.RelationshipCacheId);
#endregion

#region Save Data
await fileTool.StoreLoadTool(loadTool);
if (NewData)
{
    await fileTool.StoreCitizens(citizenCache);
    await fileTool.StoreCompanies(companyCache);
    await fileTool.StoreRelationshipCache(relationshipCache);
    await fileTool.StoreUsers(userCache);
}
#endregion

#region APImapping
app.MapGet("/advancesave", () => APICalls.AdvanceSave(fileTool,citizenCache,userCache,companyCache,relationshipCache));
app.MapGet("/createuser/{username}", (string username) => APICalls.CreateUser(username,userCache,citizenCache,companyCache));
app.MapGet("/company/{username}", (string username) => APICalls.StandardInfo(username, userCache, companyCache));
app.MapGet("/company/{username}/advisor/{role}", (string username, string role) => companyCache.PlayerCompanies[userCache.Users[username].CompanyId].Advisors[role].Describe());
app.MapGet("/company/{username}/spendtp/{tp}", (string username, double tp) => APICalls.SpendTp(username, tp, userCache, companyCache));
//app.MapGet("/test", () => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/company/citizen/{id}", (int id) => CitizenDB.ReturnCitizenFromCompany(playercompany, id));//() => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/test", () => companyCache.PlayerCompanies[new Guid("00d9631a-f81a-4578-8565-db6176fff695")].Describe());
#endregion

app.Run();