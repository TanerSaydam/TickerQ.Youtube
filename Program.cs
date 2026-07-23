using Microsoft.EntityFrameworkCore;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.Customizer;
using TickerQ.EntityFrameworkCore.DependencyInjection;
using TickerQ.WebAPI;
using TickerQ.WebAPI.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer("Data Source=DESKTOP-UOERPMR\\SQLEXPRESS;Initial Catalog=TickerQDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30");
});

builder.Services.AddTickerQ(options =>
{
    options.AddOperationalStore(ef =>
        ef.UseApplicationDbContext<ApplicationDbContext>(ConfigurationType.UseModelCustomizer)
    );
    options.AddDashboard(cf =>
    {
        cf.SetBasePath("/tickerq");
        cf.WithBasicAuth("admin", "1");
    });
});
builder.Services.MapTicker<MyBackground>()
    .WithCron("*/10 * * * * *")
    .WithMaxConcurrency(1);

var app = builder.Build();

app.UseTickerQ();

app.Run();