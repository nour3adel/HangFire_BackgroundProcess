using System;
using System.IO;
using HangfireExample.Models;
namespace HangfireExample.Services
{
    public class ReportGenerator
    {

        private readonly AppDbContext _dbContext;

        public ReportGenerator(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string GenerateReport(string userEmail, string message)
        {
            string reportContent = $"User: {userEmail}\nMessage: {message}\nCompletion Time: {DateTime.Now}\n";
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "reports");
            Directory.CreateDirectory(directoryPath); // Ensure the directory exists

            string reportPath = Path.Combine(directoryPath, $"Report_{Guid.NewGuid()}.txt");
            File.WriteAllText(reportPath, reportContent);
            return reportPath; // Return the path to the generated report
        }
        // Method to save report details in a file
        public async Task SaveReportAsync(Report report)
        {
            await _dbContext.Reports.AddAsync(report);
            await _dbContext.SaveChangesAsync();
        }

    }
}
