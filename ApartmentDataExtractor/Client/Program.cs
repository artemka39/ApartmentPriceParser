using Client.AppConfiguration;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureApp();
var app = builder.Build();
app.Run();


