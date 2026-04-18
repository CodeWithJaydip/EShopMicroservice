var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(typeof(Program).Assembly));
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(
        builder.Configuration.GetConnectionString("Database")!
    );

    options.DatabaseSchemaName = "app";
})
.UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.Run();
