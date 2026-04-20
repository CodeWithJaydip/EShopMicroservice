
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(typeof(Program).Assembly));
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapCarter();

app.Run();
