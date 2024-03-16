using AttendanceCapture.Application.Admin;
using AttendanceCapture.Application.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceCapture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ISender _sender;
        public AdminController (ISender sender) => _sender = sender;

        [HttpPost("[action]")]

        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterAdminRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterStudent([FromForm] RegisterStudentRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterLecturer([FromBody] RegisterLecturerRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ViewLogs([FromBody]VIewLogsRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> VIewAllClasses([FromBody]ViewAllClassesAdminRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ViewAllStudents([FromBody]ViewAllStudentsRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ViewAllLecturers([FromBody]ViewAllLecturersRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);

        }
    }
}
