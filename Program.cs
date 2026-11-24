using Microsoft.EntityFrameworkCore;
using wt_lab8_8razor_postgresql_glebko.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем Razor Pages
builder.Services.AddRazorPages();

// 2. Настройка строки подключения (БД)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrWhiteSpace(databaseUrl))
{
    if (databaseUrl.StartsWith("postgres://") || databaseUrl.StartsWith("postgresql://"))
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');
        var dbPort = uri.Port > 0 ? uri.Port : 5432; // Используем dbPort, чтобы не конфликтовать с портом приложения
        connectionString = $"Host={uri.Host};Port={dbPort};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
    }
    else
    {
        connectionString = databaseUrl;
    }
}

// 3. Регистрация DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// 4. Автоматическое применение миграций при запуске
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// 5. Конвейер обработки запросов
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

// 6. Запуск приложения
var appPort = Environment.GetEnvironmentVariable("PORT") ?? "10000"; // appPort, чтобы не конфликтовать
app.Run($"http://0.0.0.0:{appPort}");