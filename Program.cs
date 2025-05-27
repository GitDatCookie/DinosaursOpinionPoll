using MudBlazor.Services;
using AI_Project.Components;
using AI_Project.DBContext;
using Microsoft.EntityFrameworkCore;
using AI_Project.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using AI_Project.Helpers;
using AI_Project.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}
// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Logging
builder.Logging.AddConsole();

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//controllers
builder.Services.AddControllers();

//services
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<CustomAuthenticationService>();
builder.Services.AddScoped<IUserService, UserService>(); // Ensure UserService is still available
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAIService, AIService>();
builder.Services.AddScoped<ITextService, TextService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IEventNotificationService, EventNotificationService>();
builder.Services.AddScoped<IComponentFactory, ComponentFactory>();


builder.Services.AddHttpClient("DefaultClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7239/"); // Use your server's base URL
});

builder.Services.AddServerSideBlazor()
        .AddCircuitOptions(options =>
        {
            options.DetailedErrors = true;
        });

builder.Services.AddDbContext<AI_ProjectDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapControllers();

app.Run();
