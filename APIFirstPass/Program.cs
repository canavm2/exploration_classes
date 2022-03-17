using APIMethods;
using FileTools;
using Relation;
using People;
using Company;

#region shitidontunderstand
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


FileTool fileTool = new FileTool();
//IndexId index = new IndexId(fileTool.ReadIndex());
//RelationshipCache relationshipcache = fileTool.ReadRelationshipCache("relationships");
//CitizenCache citizens = fileTool.ReadCitizens("citizens");
//PlayerCompany playercompany = fileTool.ReadCompany("company");

#region TestMapping
app.MapGet("/test", () => CitizenDB.ReturnTest());

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
