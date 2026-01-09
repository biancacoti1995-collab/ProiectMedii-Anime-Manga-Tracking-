using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity; // NECESAR pentru Login
using ProiectMedii_Anime___Manga_Tracking_.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Servicii de bază
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// 2. Conexiune Bază de Date
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<ProiectMedii_Anime___Manga_Tracking_Context>(options =>
    options.UseSqlServer(connectionString));

// 3. CONFIGURARE IDENTITY (Fără asta nu apar butoanele de Login/Register)
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ProiectMedii_Anime___Manga_Tracking_Context>();

var app = builder.Build();

// 4. Aplicare Migrări Automate
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<ProiectMedii_Anime___Manga_Tracking_Context>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Eroare la migrări.");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 5. CRITIC: Ordinea contează aici pentru Login!
app.UseAuthentication(); // Activează verificarea cine ești
app.UseAuthorization();  // Activează permisiunile

// 6. RUTELE (Am schimbat să pornească direct în lista de Anime)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MediaItems}/{action=Index}/{id?}");

app.MapRazorPages(); // Necesar pentru paginile de Identity (Login/Register)

app.Run();