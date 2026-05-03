using Microsoft.EntityFrameworkCore;
using TicketHub.Infrastructure.Data;
using TicketHub.Interfaces.IRepository;
using TicketHub.Interfaces.IServices;
using TicketHub.Repository;
using TicketHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Services of Setores
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
builder.Services.AddScoped<ISetorService, SetorService>();

// Services of Prioridades
builder.Services.AddScoped<IPrioridadeRepository, PrioridadeRepository>();
builder.Services.AddScoped<IPrioridadeService, PrioridadeService>();

// Services of Chamado
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();
builder.Services.AddScoped<IChamadoService, ChamadoService>();

// Services of Chamados


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    var seed = File.ReadAllText("scripts/seed.sql");
    db.Database.ExecuteSqlRaw(seed);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Chamado}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
