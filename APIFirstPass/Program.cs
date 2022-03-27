using APIMethods;
using FileTools;
using Relation;
using People;
using Company;
using User;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

#region app builder
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
#endregion

#region variable loading
//keyvault stuff that doesnt work.
//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
string azureUri = builder.Configuration["AzureCosmos:URI"];
string azureKey = builder.Configuration["AzureCosmos:PrimaryKey"];
#endregion

#region dataloading
FileTool fileTool = new FileTool(azureUri, azureKey);
LoadTool loadTool = await fileTool.ReadLoadTool(new Guid("3f53a424-601c-4c7e-a19d-3ead86aa15dc"));
//LoadTool loadTool = new();
//loadTool.RelationshipCacheId = new Guid("4938643c-31b8-4e8c-9ff8-0816c09904da");
#endregion

Boolean NewData = false;

#region User Loading
UserCache userCache;
if (NewData)
{
    userCache = new UserCache();
    loadTool.UserCacheId = userCache.id;
}
else userCache = await fileTool.ReadUsers(loadTool.UserCacheId);

#endregion

#region Citizen Loading
CitizenCache citizenCache;
if (NewData)
{
    citizenCache = new CitizenCache(100);
    Console.WriteLine($"femalecitizens has: {citizenCache.FemaleCitizens.Count} items.\nThe first female is:\n{citizenCache.FemaleCitizens[0].DescribeCitizen()}");
    Console.WriteLine($"malecitizens has: {citizenCache.MaleCitizens.Count} items.\nThe first male is:\n{citizenCache.MaleCitizens[0].DescribeCitizen()}");
    Console.WriteLine($"nbcitizens has: {citizenCache.NBCitizens.Count} items.\nThe first non-binary is:\n{citizenCache.NBCitizens[0].DescribeCitizen()}");
    loadTool.CitizenCacheId = citizenCache.id;
}
else citizenCache = await fileTool.ReadCitizens(loadTool.CitizenCacheId);
#endregion

#region Company Loading
CompanyCache companyCache;
PlayerCompany playerCompany;
if (NewData)
{
    List<Citizen> advisors = new List<Citizen>();
    for (int i = 0; i < 7; i++)
        advisors.Add(citizenCache.GetRandomCitizen());
    Citizen master = citizenCache.GetRandomCitizen();
    playerCompany = new("testcompany", master, advisors);
    companyCache = new();
    companyCache.PlayerCompanies[playerCompany.id] = playerCompany;
    loadTool.CompanyCacheId = companyCache.id;
}
else companyCache = await fileTool.ReadCompanies(loadTool.CompanyCacheId);
#endregion

#region Relationship Loading
RelationshipCache relationshipcache;
if (NewData)
{
    relationshipcache = new RelationshipCache();
    loadTool.RelationshipCacheId = relationshipcache.id;
}
else relationshipcache = await fileTool.ReadRelationshipCache(loadTool.RelationshipCacheId);
#endregion

#region Save Data
await fileTool.StoreLoadTool(loadTool);
if (NewData)
{
    await fileTool.StoreCitizens(citizenCache);
    await fileTool.StoreCompanies(companyCache);
    await fileTool.StoreRelationshipCache(relationshipcache);
    await fileTool.StoreUsers(userCache);
}
#endregion

#region APImapping
app.MapGet("/test", () => companyCache.PlayerCompanies[new Guid("00d9631a-f81a-4578-8565-db6176fff695")].Describe());
//app.MapGet("/test", () => CitizenDB.ReturnTest());
//app.MapGet("/test", () => CitizenDB.ReturnTest(AzureStorageAccessKey));
//app.MapGet("/test", () => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/company", () => playercompany.Describe());//() => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/company/citizen/{id}", (int id) => CitizenDB.ReturnCitizenFromCompany(playercompany, id));//() => CitizenDB.ReturnCitizen(citizens));
#endregion

app.Run();