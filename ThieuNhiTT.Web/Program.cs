using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Serilog;
using ThieuNhiTT.Web.DataAccess;
using ThieuNhiTT.Web.Fody;
using ThieuNhiTT.Web.Models;
using ThieuNhiTT.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddScoped<IRepository<Book>, JsonBookRepository<Book>>();
builder.Services.AddScoped<IRepository<Lesson>, JsonBookRepository<Lesson>>();
builder.Services.AddScoped<IRepository<News>, JsonBookRepository<News>>();

// Register services for Book and Lesson
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IEmailSender, EmailService>();


builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add<CustomExceptionFilter>();
});

builder.Services.Configure<EmailSenderOptions>(builder.Configuration.GetSection("EmailSender"));
builder.Services.Configure<EmailReceiver>(builder.Configuration.GetSection("EmailReceiver"));
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
