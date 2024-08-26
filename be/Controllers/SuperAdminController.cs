using Microsoft.AspNetCore.Mvc;
using be.Services.SuperAdminService;
using be.Services.TestDetailService;
using be.DTOs;
using Microsoft.Extensions.Primitives;

namespace be.Controllers
{
    [Route("api/superAdmin")]
    [ApiController]
    public class SuperAdminController : Controller
    {
        private readonly ISuperAdminService _superAdminService;

        public SuperAdminController(ISuperAdminService superAdminService)
        {
            _superAdminService = superAdminService;
        }

        [HttpGet("getAllAdmin")]
        public async Task<ActionResult> GetAllAdmin()
        {
            try
            {
                var result = _superAdminService.GetAllAdmin();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllEmail")]
        public async Task<ActionResult> GetAllEmail()
        {
            try
            {
                var result = _superAdminService.GetAllEmail();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("addAdmin")]
        public async Task<ActionResult> AddAdmin(SuperAdminDTO addAdmin)
        {
            try
            {
                var result = _superAdminService.AddAdmin(addAdmin);
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPost("changeStatus")]
        public async Task<ActionResult> ChageStatusAdmin (int accountId, string status)
        {
            try
            {
                var result = _superAdminService.ChangeStatus(accountId, status);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("updateAdmin")]
        public async Task<ActionResult> UpdateAdmin(UpdateAdminSuperDTO updateAdmin)
        {
            try
            {
                var result = _superAdminService.UpdateAdmin(updateAdmin);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
