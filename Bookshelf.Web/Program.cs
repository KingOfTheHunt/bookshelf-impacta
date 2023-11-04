using Bookshelf.Web;
using Bookshelf.Web.Services;

var builder = WebApplication.CreateBuilder(args);

Configuration.Api = builder.Configuration.GetValue<string>("Api");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddHttpClient<AccountService>(options =>
{
    options.BaseAddress = new Uri(Configuration.Api);
});
builder.Services.AddHttpClient<BookService>(options =>
{
    options.BaseAddress = new Uri(Configuration.Api);
});
builder.Services.AddHttpClient<ReadingService>(options =>
{
    options.BaseAddress = new Uri(Configuration.Api);
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
