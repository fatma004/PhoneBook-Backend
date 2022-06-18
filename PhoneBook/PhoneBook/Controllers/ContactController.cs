using BL.AppServices;
using BL.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactAppService _contactAppService;
        public ContactController(ContactAppService contactAppService)
        {
            this._contactAppService = contactAppService;
        }

        [HttpGet("GtAllContactsOfUser/{UserId}")]
        public IActionResult GetAll(string UserId)
        {
            return Ok(_contactAppService.GetAllContactsOfUserByUserId(UserId));
        }
        [HttpPost]
        public IActionResult Create(ContactModel contactModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _contactAppService.SaveNewContact(contactModel);

                return Created("Created", contactModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_contactAppService.GetContactById(id));
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id, ContactModel contactModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _contactAppService.UpdateContact(contactModel);
                return Ok(contactModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _contactAppService.DeleteContact(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchByName/{UserId}/{keyWord}")]
        public IActionResult SearchByName(string UserId, string keyWord)
        {
            return Ok(_contactAppService.SearchByName(UserId, keyWord));
        }
    }
}
