using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace HangfireExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public UserController(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost]
        public IActionResult StartTask(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                return BadRequest("Email cannot be empty.");
            }

            // Enqueue a background job
            _backgroundJobClient.Enqueue<YourJob>(job => job.ExecuteTask(userEmail));
            return Ok("Task started!");
        }
    }
}
