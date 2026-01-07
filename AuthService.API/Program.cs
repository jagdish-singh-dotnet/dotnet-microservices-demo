using AuthService.Application.Features.Auth.Commands;
using AuthService.Application.Features.Auth.Queries;
using AuthService.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Infrastructure + Application dependencies
builder.Services.AddAuthInfrastructure(builder.Configuration);

// Register Commands & Queries
builder.Services.AddScoped<RegisterUserCommand>();
builder.Services.AddScoped<LoginUserQuery>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// Health check (professional touch)
app.MapGet("/health", () => "AuthService is running");

app.Run();
