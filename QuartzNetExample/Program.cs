using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddQuartz();

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.AddScoped<IScheduler>(_ => new StdSchedulerFactory(new NameValueCollection
{
    { "quartz.serializer.type", "json" },
    { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
    { "quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.StdAdoDelegate, Quartz" },
    { "quartz.jobStore.tablePrefix", "QRTZ_" },
    { "quartz.jobStore.dataSource", "myDS" },
    { "quartz.dataSource.myDS.connectionString", "User ID=postgres;Password=Password123.;Host=postgresql;Port=5432;Database=Quartz;" },
    { "quartz.dataSource.myDS.provider", "Npgsql" },
    { "quartz.jobStore.useProperties", "true" },
    
}).GetScheduler().Result);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.MapControllers();

app.Run();