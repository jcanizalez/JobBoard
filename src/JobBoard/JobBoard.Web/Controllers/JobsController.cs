using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Model;
using JobBoard.Web.Services;

namespace JobBoard.Web.Controllers
{
    public class JobsController : Controller
    {
       
        private readonly IJobsService _service;

        public JobsController(IJobsService service)
        {
            _service = service;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllJobs());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _service.GetJobById(id.Value);
               
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,CreatedAt,ExpiresAt")] Job job)
        {
            if (ModelState.IsValid)
            {
              
               await _service.CreateJob(job);
               return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _service.GetJobById(id.Value);
            if (job == null)
            {
                return NotFound();
            }
            
            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,CreatedAt,ExpiresAt")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.EditJob(job);
                  
                }
                catch 
                {
                    if (!JobExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _service.DeleteJob(id.Value);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _service.GetJobById(id);
            await _service.DeleteJob(job.Id);
           
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _service.GetJobById(id) != null;
        }
    }
}
