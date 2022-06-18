using BL.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly TypeAppService _typeAppService;

        public TypeController(TypeAppService typeAppService)
        {
            this._typeAppService = typeAppService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_typeAppService.GetAll());
        }
    }
}
