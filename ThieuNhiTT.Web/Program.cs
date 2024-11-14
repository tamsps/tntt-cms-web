using ThieuNhiTT.Web.DataAccess;
using ThieuNhiTT.Web.Services;

var builder = WebApplication.CreateBuilder(args);

string jsonFilePath = builder.Configuration.GetSection("ThangTienJsonFilePath").Value;
string lessonFilePath = builder.Configuration.GetSection("LessonFilePath").Value;


// Add services to the container.
builder.Services.AddScoped<IBookRepository>(provider => new JsonBookRepository(jsonFilePath, lessonFilePath));
builder.Services.AddScoped<ILessonRepository>(provider => new LessonRepository(jsonFilePath, lessonFilePath));
builder.Services.AddScoped<BookService>();
builder.Services.AddControllersWithViews();
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
