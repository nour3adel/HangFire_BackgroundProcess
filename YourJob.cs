using System.Threading.Tasks;
using HangfireExample.Services;
using HangfireExample.Models;

namespace HangfireExample
{
    public class YourJob
    {
        private readonly EmailService _emailService;
        private readonly ReportGenerator _reportGenerator;

        public YourJob(EmailService emailService, ReportGenerator reportGenerator)
        {
            _emailService = emailService;
            _reportGenerator = reportGenerator;
        }

        public async Task ExecuteTask(string userEmail)
        {
            // Simulate a long-running task (you can replace this with your actual task logic)
            // Simulate a long-running task (replace with your actual task logic)
            await Task.Delay(5000);

            // Generate report with the user email and a message
            string message = "Task Completed Successfully.";
            string reportFilePath = _reportGenerator.GenerateReport(userEmail, message);

            // Create a report object to save it to the database
            Report report = new Report
            {
                UserEmail = userEmail,
                Content = message,
                CreatedAt = DateTime.UtcNow // Use UTC for consistency
            };

            // Save the report to the database
            await _reportGenerator.SaveReportAsync(report);

            // Send email with the report
            await _emailService.SendReportEmail(userEmail, reportFilePath);
        }
    }
}
