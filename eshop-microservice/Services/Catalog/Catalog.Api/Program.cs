using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddProblemDetails();
builder.Services.AddCarter(new DependencyContextAssemblyCatalog(typeof(Program).Assembly));
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
    configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


builder.Services.AddMarten(options =>
{
    options.Connection(
        builder.Configuration.GetConnectionString("Database")!
    );

    options.DatabaseSchemaName = "app";
})
.UseLightweightSessions();


var app = builder.Build();
app.UseExceptionHandler();
app.MapCarter();
app.Run();
