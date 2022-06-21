using Microsoft.AspNetCore.Mvc;

namespace PS_Api2.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly int _readinessLatencyInSec, _startupLatencyInSec;
        public HomeController(IConfiguration configuration)
        {
            _readinessLatencyInSec = configuration.GetSection("Timeouts").GetValue<int>("readinessLatencyInSec");
            _startupLatencyInSec = configuration.GetSection("Timeouts").GetValue<int>("startupLatencyInSec");
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpGet("readiness")]
        public IActionResult Readiness()
        {
            Thread.Sleep(_readinessLatencyInSec * 1000);
            return Ok();
        }

        [HttpGet("liveness")]
        public IActionResult Liveness()
        {
            return Ok();
        }

        [HttpGet("startup")]
        public IActionResult Startup()
        {
            Thread.Sleep(_startupLatencyInSec * 1000);
            return Ok();
        }

        [HttpGet("getsettings")]
        public IActionResult Settings()
        {
            return Ok(new
            {
                ReadinessLatencyInSeconds = _readinessLatencyInSec,
                StartupLatencyInSeconds = _startupLatencyInSec
            }
            );
        }


    }
}
