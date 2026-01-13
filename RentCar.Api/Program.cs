using Microsoft.EntityFrameworkCore;
using RentCar.Api.Models;
using RentCar.Api.Services;
using RentCar.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddDbContext<RentCarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRentalService, RentalService>();  
builder.Services.AddScoped<CarService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", b =>
    {
        b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<RentCarDbContext>();
        context.Database.EnsureCreated();
        Console.WriteLine("Database Ready!");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Gagal inisialisasi database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty; // Biar swagger muncul di halaman utama
});

// --- BAGIAN YANG TADI TERPOTONG/HILANG ---
app.UseStaticFiles();
app.UseCors("AllowAll"); // Aktifkan izin buat Frontend
app.UseAuthorization();
app.MapControllers(); // Penting biar route /api/Cars bisa diakses

app.Run(); // Menjalankan server agar port 5000 aktif

