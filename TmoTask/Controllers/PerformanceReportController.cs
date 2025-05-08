using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TmoTask.Interfaces;

namespace TmoTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceReportController : ControllerBase
    {
        private readonly ISellerService _sellerService;
        public PerformanceReportController(ISellerService sellerService) {
            _sellerService = sellerService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAsync(string? branch)
        {
            var sellerReport = await _sellerService.GetTopSellersByMonthAsync(branch);
            return Ok(sellerReport);
        }
    }
}
