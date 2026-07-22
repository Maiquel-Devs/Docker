using DockerComposeApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura o DbContext com Retry automático pra conexões instáveis de container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null);
    }));

var app = builder.Build();

// Tenta criar o banco aguardando a subida do container do SQL Server
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Tenta até 5 vezes criar o banco com intervalo de 3 segundos
    int retries = 5;
    while (retries > 0)
    {
        try
        {
            db.Database.EnsureCreated();
            break; 
        }
        catch (Exception ex)
        {
            retries--;
            if (retries == 0) throw;
            Console.WriteLine($"Aguardando SQL Server iniciar... Tentativas restantes: {retries}");
            Thread.Sleep(3000); 
        }
    }
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();