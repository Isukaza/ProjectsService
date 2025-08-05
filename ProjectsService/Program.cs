using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using DAL.Models.Enums;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using ProjectsService.Managers;
using ProjectsService.Managers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestPath
                            | HttpLoggingFields.RequestBody
                            | HttpLoggingFields.ResponseBody
                            | HttpLoggingFields.Duration
                            | HttpLoggingFields.ResponseStatusCode;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoClient = new MongoClient(builder.Configuration.GetConnectionString("MongoDb"));
var mongoDatabase = mongoClient.GetDatabase("projectsdb");

BsonSerializer.RegisterSerializer(new EnumSerializer<IndicatorName>(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new EnumSerializer<Symbol>(MongoDB.Bson.BsonType.String));
BsonSerializer.RegisterSerializer(new EnumSerializer<Timeframe>(MongoDB.Bson.BsonType.String));

builder.Services.AddSingleton(mongoDatabase);

builder.Services.AddScoped<IProjectManager, ProjectManager>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();