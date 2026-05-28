using EWallet.Api.Clients;
using EWallet.Api.Data;
using EWallet.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddHttpClient<IEWalletApiClient, EWalletApiClient>();
builder.Services.AddHttpClient<IEWalletApiClient, EWalletApiClient>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress =new Uri(config["EWallet:BaseUrl"]);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add("x-api-key", config["EWallet:ApiKey"]);
    client.DefaultRequestHeaders.Add("client-id", config["EWallet:ClientId"]);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


builder.Host.UseSerilog((Context, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(Context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
