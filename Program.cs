var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();

// Register MongoDB Service
builder.Services.AddSingleton<Electric__.Services.MongoDBService>();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
app.Run();
