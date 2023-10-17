using Login_System.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
