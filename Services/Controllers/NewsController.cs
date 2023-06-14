using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Repository;

namespace Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            this._service = service;
        }

        [HttpGet("{NewsId}")]
        public async Task<IActionResult> Get(string NewsId)
        {
            var result =await _service.GetbyId(NewsId);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetALL();
            return Ok(result);
        }
    }
}
