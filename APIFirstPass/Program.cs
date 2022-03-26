using APIMethods;
using FileTools;
using Relation;
using People;
using Company;
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
LoadTool loadTool = await fileTool.ReadLoadTool("ea2d7e3f-7135-4351-8195-590717b1afdc");
//LoadTool loadTool = new();
//loadTool.CitizensId = "59558795-f812-4258-90bd-c4bdd0f9ddf4";
//loadTool.CompanyId = "3c29a4b7-ad9d-414d-9124-e7e09ab9f699";
//loadTool.RelationshipId = "4938643c-31b8-4e8c-9ff8-0816c09904da";
#endregion

Boolean NewCitizens = false;
Boolean NewCompanies = false;
Boolean NewRelationships = false;

#region Citizen Loading
CitizenCache citizens;
if (NewCitizens)
{
    citizens = new CitizenCache(100);
    Console.WriteLine($"femalecitizens has: {citizens.FemaleCitizens.Count} items.\nThe first female is:\n{citizens.FemaleCitizens[0].DescribeCitizen()}");
    Console.WriteLine($"malecitizens has: {citizens.MaleCitizens.Count} items.\nThe first male is:\n{citizens.MaleCitizens[0].DescribeCitizen()}");
    Console.WriteLine($"nbcitizens has: {citizens.NBCitizens.Count} items.\nThe first non-binary is:\n{citizens.NBCitizens[0].DescribeCitizen()}");
}
else citizens = await fileTool.ReadCitizens(loadTool.CitizensId);
#endregion

#region Company Loading
PlayerCompany playerCompany;
if (NewCompanies)
{
    List<Citizen> advisors = new List<Citizen>();
    for (int i = 0; i < 7; i++)
        advisors.Add(citizens.GetRandomCitizen());
    Citizen master = citizens.GetRandomCitizen();
    playerCompany = new("testcompany", master, advisors);
}
else playerCompany = await fileTool.ReadCompany(loadTool.CompanyId);
#endregion

#region Relationship Loading
RelationshipCache relationshipcache;
if (NewRelationships)
{
    relationshipcache = new RelationshipCache();
} else relationshipcache = await fileTool.ReadRelationshipCache(loadTool.RelationshipId);
#endregion


#region Save Data
await fileTool.StoreLoadTool(loadTool);
#endregion

#region APImapping
app.MapGet("/test", () => playerCompany.Describe());
//app.MapGet("/test", () => CitizenDB.ReturnTest());
//app.MapGet("/test", () => CitizenDB.ReturnTest(AzureStorageAccessKey));
//app.MapGet("/test", () => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/company", () => playercompany.Describe());//() => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/company/citizen/{id}", (int id) => CitizenDB.ReturnCitizenFromCompany(playercompany, id));//() => CitizenDB.ReturnCitizen(citizens));
#endregion

app.Run();