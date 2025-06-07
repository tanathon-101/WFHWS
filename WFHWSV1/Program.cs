using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((context, services) =>
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var jobKey = new JobKey("TmpFileCleanup");
            q.AddJob<DeleteOldTmpFilesJob>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("TmpFileCleanup-trigger")
                .WithCronSchedule("0 0 2 * * ?")); // ทุกวันตอนตี 2
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    })
    .Build()
    .Run();
