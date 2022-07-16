using SignalR_TableDependency.Hubs;
using SignalR_TableDependency.MiddlewareExtensions;
using SignalR_TableDependency.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//signalR
builder.Services.AddSignalR();

// DI
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();
builder.Services.AddSingleton<SubscribeSaleTableDependency>();
builder.Services.AddSingleton<SubscribeCustomerTableDependency>();


var app = builder.Build();
var connectionString = app.Configuration.GetConnectionString("DefaultConnection");


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
//hubs
app.MapHub<DashboardHub>("/dashboardHub");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

/*
 * we must call SubscribeTableDependency() here
 * we create one middleware and call SubscribeTableDependency() method in the middleware
 */

app.UseSqlTableDependency<SubscribeProductTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeSaleTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeCustomerTableDependency>(connectionString);

app.Run();
