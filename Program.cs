using NewsItems.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// builder
var builder = WebApplication.CreateBuilder(args);

// DbContext db
builder.Services.AddDbContext<NewsItemsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NewsItemsContext") ?? throw new InvalidOperationException("Connection string 'NewsItemsContext' not found.")));

// services
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ensure single instance of repo
builder.Services.AddSingleton<INewsMessageRepository, NewsMessageRepository>();

// build app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// security
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors();

// enable static files & server index.html at '/'
app.UseStaticFiles();
app.MapGet("/", async app =>
{
    app.Response.StatusCode = 200;
    app.Response.ContentType = "text/html";
    await app.Response.SendFileAsync(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"));
});

// enable endpoints
// add CORS policy to allow index.html on localhost to contact api
app.MapControllers().RequireCors(myAllowSpecificOrigins);

// start
app.Run();
