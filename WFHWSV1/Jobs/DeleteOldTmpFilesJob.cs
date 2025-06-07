using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;

public class DeleteOldTmpFilesJob : IJob
{
    private readonly ILogger<DeleteOldTmpFilesJob> _logger;
    private readonly IConfiguration _configuration;

    public DeleteOldTmpFilesJob(ILogger<DeleteOldTmpFilesJob> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public Task Execute(IJobExecutionContext context)
    {
        string targetFolder = _configuration["Cleanup:TargetFolder"]!;
        int daysThreshold = int.Parse(_configuration["Cleanup:DaysToKeep"]!);

        if (!Directory.Exists(targetFolder))
        {
            _logger.LogWarning("Directory not found: {targetFolder}", targetFolder);
            return Task.CompletedTask;
        }

        var files = Directory.GetFiles(targetFolder, "*.tmp");
        int deleted = 0;

        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            if (fileInfo.LastWriteTime < DateTime.Now.AddDays(-daysThreshold))
            {
                try
                {
                    fileInfo.Delete();
                    deleted++;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to delete {file}", file);
                }
            }
        }

        _logger.LogInformation("Deleted {count} .tmp files older than {days} days", deleted, daysThreshold);
        return Task.CompletedTask;
    }
}
