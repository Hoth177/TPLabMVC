using HomeAppStore;
using HomeAppStore.Models; // пространство имен модулей
using Microsoft.EntityFrameworkCore; // пространство имен Entityframework


var builder = WebApplication.CreateBuilder(args);

// Добавляем контекст подключения к локальной базе данных homestoredb
builder.Services.AddDbContext<ProductContext>(options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=homestoredb;Trusted_Connection=True;MultipleActiveResultSets=true"));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	SampleData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
