using Microsoft.AspNetCore.Mvc;
using Quartz;
using QuartzNetExample.Jobs;
using QuartzNetExample.Models;

namespace QuartzNetExample.Controllers;

[ApiController]
[Route("[controller]")]
public class JobsController : ControllerBase
{
    private readonly IScheduler _scheduler;

    public JobsController(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    [HttpGet]
    [Route("/start_simple_job/{interval:int:min(0)}/{count:int:min(0)}")]
    public async Task<IActionResult> StartSimpleJob(int interval, int count)
    {
        var job = JobBuilder.Create<EmailSender>()
            // for primitive data types 
            // .UsingJobData("Receiver", "admin@recep.com")
            // .UsingJobData("Sender", "editor@gunes.com")
            // .UsingJobData("Message", "this is a test message with quartz")
            .WithIdentity("email-sender-job-1", "email-sender-job-group-1")
            .Build();

        //for complex data types
        job.JobDataMap.Put("EmailCredentials",
            new EmailCredentials("admin@recep.com", "editor@gunes.com", "this is a test message with quartz"));

        var trigger = TriggerBuilder.Create()
            .WithIdentity("email-sender-trigger-1", "email-sender-trigger-group-1")
            .ForJob("email-sender-job-1", "email-sender-job-group-1")
            .WithSimpleSchedule(opt => { opt.WithRepeatCount(count).WithIntervalInSeconds(interval); })
            .StartNow()
            .Build();

        await _scheduler.ScheduleJob(job, trigger);

        return Ok(new { message = "Job was created" });
    }

    [HttpGet]
    [Route("/start_simple_job_at/{interval:int:min(0)}")]
    public async Task<IActionResult> StartJobWithDateTime(int interval)
    {
        Console.WriteLine(interval);
        var job = JobBuilder.Create<EmailSender>()
            // for primitive data types 
            // .UsingJobData("Receiver", "admin@recep.com")
            // .UsingJobData("Sender", "editor@gunes.com")
            // .UsingJobData("Message", "this is a test message with quartz")
            .WithIdentity("email-sender-job-2", "email-sender-job-group-2")
            .Build();

        //for complex data types
        job.JobDataMap.Put("EmailCredentials",
            new EmailCredentials("admin@recep.com", "editor@gunes.com", "this is a test message with quartz"));

        var trigger = TriggerBuilder.Create()
            .WithIdentity("email-sender-trigger-2", "email-sender-trigger-group-2")
            .ForJob("email-sender-job-2", "email-sender-job-group-2")
            //change with others
            .StartAt(DateTimeOffset.Now.AddSeconds(interval))
            .Build();

        await _scheduler.ScheduleJob(job, trigger);

        return Ok(new { message = "Job was created" });
    }
}