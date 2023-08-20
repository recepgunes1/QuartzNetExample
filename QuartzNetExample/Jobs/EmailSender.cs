using Quartz;
using QuartzNetExample.Models;

namespace QuartzNetExample.Jobs;

public class EmailSender : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        try
        {
            var credentials = (EmailCredentials)context.JobDetail.JobDataMap.Get("EmailCredentials");
            Console.WriteLine(
                "==========================================================================================");
            Console.WriteLine($"{credentials.Message} was send from {credentials.Sender} to {credentials.Receiver}");
            Console.WriteLine($"JobKey: {context.Trigger.JobKey} TriggerKey: {context.Trigger.Key}");
            Console.WriteLine(
                "==========================================================================================");
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                "==========================================================================================");
            Console.WriteLine(ex.Message);
            Console.WriteLine(
                "==========================================================================================");
        }

        return Task.CompletedTask;
    }
}