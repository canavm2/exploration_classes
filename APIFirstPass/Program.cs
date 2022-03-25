using APIMethods;
using FileTools;
using Relation;
using People;
using Company;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;


var builder = WebApplication.CreateBuilder(args);

//keyvault stuff that doesnt work.
//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());



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

string azureUri = builder.Configuration["AzureCosmos:URI"];
string azureKey = builder.Configuration["AzureCosmos:PrimaryKey"];
string citizensId = "59558795-f812-4258-90bd-c4bdd0f9ddf4";
string companyId = "3c29a4b7-ad9d-414d-9124-e7e09ab9f699";
string relationshipId = "4938643c-31b8-4e8c-9ff8-0816c09904da";


FileTool fileTool = new FileTool(azureUri, azureKey);
//RelationshipCache relationshipcache = await fileTool.ReadRelationshipCache(relationshipId);
//CitizenCache citizens = await fileTool.ReadCitizens(citizensId);
//PlayerCompany playerCompany = await fileTool.ReadCompany(companyId);

#region TestMapping
//app.MapGet("/test", () => playerCompany.Describe());
//app.MapGet("/test", () => CitizenDB.ReturnTest());
//app.MapGet("/test", () => CitizenDB.ReturnTest(AzureStorageAccessKey));

#endregion 




//#region HTTPCallMapping
//app.MapGet("/test", () => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/company", () => playercompany.Describe());//() => CitizenDB.ReturnCitizen(citizens));
//app.MapGet("/company/citizen/{id}", (int id) => CitizenDB.ReturnCitizenFromCompany(playercompany, id));//() => CitizenDB.ReturnCitizen(citizens));
//#endregion



app.Run();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//       new WeatherForecast
//       (
//           DateTime.Now.AddDays(index),
//           Random.Shared.Next(-20, 55),
//           summaries[Random.Shared.Next(summaries.Length)]
//       ))
//        .ToArray();
//    return forecast;
//})
//    .WithName("GetWeatherForecast");
