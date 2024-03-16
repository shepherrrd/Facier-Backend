using AttendanceCapture.Application.Identity;
using AttendanceCapture.Application.Lecturer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceCapture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturerController : ControllerBase
    {
        private readonly ISender _sender;
        public LecturerController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            var response = await _sender.Send(request);
            if (!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCourse([FromBody]CreateCourseRequest request)
        {
            var response = await _sender.Send(request);
            if(!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CaptureAttendance([FromForm]CaptureAttendanceRequest request)
        {
            var response = await _sender.Send(request);
            if(!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> VIewAttendanceByDate([FromForm]ViewAttendanceByDateRequest request)
        {
            var response = await _sender.Send(request);
            if(!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ViewAllClasses([FromForm]ViewAllCoursesRequest request)
        {
            var response = await _sender.Send(request);
            if(!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ViewCLassByID([FromForm]ViewCourseByIDRequest request)
        {
            var response = await _sender.Send(request);
            if(!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AssignStudentToClass([FromForm]AssignStudentToClassRequest request)
        {
            var response = await _sender.Send(request);
            if(!response.Status)
                return BadRequest(response);
            return Ok(response);
        }

        
    }
}
