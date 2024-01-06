using System.Reflection;
using Application;

// TODO: Debug XML documentation not showing
const string allowClientCorsPolicy = "_allow_client";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowClientCorsPolicy,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200","https://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    }
    );

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors(allowClientCorsPolicy);

app.UseAuthorization();


app.MapControllers();

app.Logger.LogInformation("Environment is: " + app.Environment.EnvironmentName);
// When the app and database are both running in docker, we need to wait
// a few seconds to make sure the database is started.


app.Run();
