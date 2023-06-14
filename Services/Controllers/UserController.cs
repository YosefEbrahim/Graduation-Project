using Microsoft.AspNetCore.Mvc;

namespace Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //[HttpPost("AddConnection")]
        //public async Task<IActionResult> AddConnection(string UserId, string ContextId)
        //{

        //    if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(ContextId))
        //    {
        //        return Ok(new { key = 1, data = false });
        //    }

        //    bool IsSaved = await _Chat.AddConnection(UserId, ContextId, IsOpen);

        //    return Ok(new { key = 1, data = IsSaved });
        //}


    }
}
