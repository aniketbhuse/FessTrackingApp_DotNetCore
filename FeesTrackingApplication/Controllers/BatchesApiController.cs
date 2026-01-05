using FeesTrackingApplication.Data;
using FeesTrackingApplication.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeesTrackingApplication.Controllers
{
    [Route("api/batches")]
    [ApiController]
    public class BatchesApiController : Controller
    {

        private readonly BatchService _batchService;
        private readonly AppDbContext _context;

        public BatchesApiController( BatchService batchService, AppDbContext context)
        {
            _batchService = batchService;
            _context = context;

        }

        [HttpPost("sync")]
        public async Task<IActionResult> SyncFromAPI()
        {
            await _batchService.FetchAndSaveBatchesAsync();
            return Ok("Data saved successfully");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.Batches.ToList();
            return Ok(data);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
