
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProiectMedii_Anime___Manga_Tracking_.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Developer-friendly EF Core error pages in development
object dbDeveloperExceptionFilter  = builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Use a single connection string name — ensure this exists in appsettings.json
const string connectionName = "DefaultConnection";
var connectionString = builder.Configuration.GetConnectionString(connectionName)
    ?? throw new InvalidOperationException($"Connection string '{connectionName}' not found.");

// Register DbContext
builder.Services.AddDbContext<ProiectMedii_Anime___Manga_Tracking_Context>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Apply pending EF Core migrations at startup (logged). Catch and log errors so the app can fail fast with a useful message.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var db = services.GetRequiredService<ProiectMedii_Anime___Manga_Tracking_Context>();
        db.Database.Migrate();
        logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying database migrations.");
        throw;
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Provide EF Core detailed errors in development
    object migrationsEndpoint = app.UseMigrationsEndPoint(); // This requires the above using directive
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS dbDeveloperExceptionFilter is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// If you add authentication/identity later, call app.UseAuthentication() here.
app.UseAuthorization();

// Map MVC controllers (you have controllers in the project) and Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
