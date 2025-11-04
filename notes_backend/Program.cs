using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Ensure automatic model state validation returns RFC 7807 ProblemDetails
        options.SuppressModelStateInvalidFilter = false;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(settings =>
{
    settings.Title = "Simple Notes API";
    settings.Version = "v1";
    settings.Description = "A simple REST API for managing notes with in-memory storage.";
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowCredentials()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Dependency Injection for Clean Architecture
builder.Services.AddSingleton<NotesBackend.Repositories.INoteRepository, NotesBackend.Repositories.InMemoryNoteRepository>();
builder.Services.AddScoped<NotesBackend.Services.INoteService, NotesBackend.Services.NoteService>();

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

// Configure OpenAPI/Swagger
app.UseOpenApi();
app.UseSwaggerUi(config =>
{
    config.Path = "/docs";
});

// Map controllers
app.MapControllers();

// Health check endpoint
app.MapGet("/", () => Results.Json(new { message = "Healthy" }));

app.Run();