using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TmoTask.Interfaces;

namespace TmoTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var sellerReport = await _branchService.GetBranchesAsync();
            return Ok(sellerReport);
        }
    }
}
