using DatabaseServiceContracts;
using Microsoft.EntityFrameworkCore;
using PackageTracker.Server.Database;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

bool? useSwagger = Convert.ToBoolean(Environment.GetEnvironmentVariable("USE_SWAGGER"));
bool useInMemoryDatabase = builder.Configuration.GetValue<bool>("UseInMemoryDatabase");

if (useSwagger == true)
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Package Tracking API",
            Description = "Tracking packages",
            Version = "v1"
        });

    });


builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    if (useInMemoryDatabase)
    {
        opt.UseInMemoryDatabase("PackageList");

    }
    else
    {
        opt.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
    }

});


builder.Services.AddScoped<IDatabaseService, DatabaseLogic>();

var app = builder.Build();

if (app.Environment.IsDevelopment() && useSwagger == true)
    app.UseSwagger();

 
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.EnsureCreated();
}

app.MapControllers();
app.Run();
