using FYP1_System___Individual.Data;
using FYP1_System___Individual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FYP1_System___Individual.Controllers
{
    public class ProposalsController : Controller
    {
        private readonly FYP1_System_Context _context;

        public ProposalsController(FYP1_System_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var proposals = await _context.Proposals
                .Include(p => p.Student)
                .Include(p => p.Supervisor)
                .Include(p => p.Evaluator1)
                .Include(p => p.Evaluator2)
                .ToListAsync();
            return View(proposals);
        }

        //public IActionResult Create()
        //{
        //    ViewBag.Students = new SelectList(_context.Students, "Id", "Name");
        //    ViewBag.Lecturers = new SelectList(_context.Lecturers, "Id", "Name");
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Create([Bind("Id, Title, ProjectType, FilePath, Semester, Session, StudentId, SupervisorId")] Proposal proposal)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        _context.Proposals.Add(proposal);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
        //    ViewBag.Lecturers = new SelectList(_context.Lecturers, "Id", "Name", proposal.SupervisorId);
        //    return View(proposal);

        //}

        public IActionResult Create1()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create2(DomainType projectType)
        {
            ViewBag.Students = new SelectList(_context.Students, "Id", "Name");
            ViewBag.Lecturers = new SelectList(_context.Lecturers.Where(l => l.Domain == projectType), "Id", "Name");

            var proposal = new Proposal { ProjectType = projectType };
            return View(proposal);
        }
        [HttpPost]
        public async Task<IActionResult> Create2([Bind("Id, Title, ProjectType, FilePath, Semester, Session, StudentId, SupervisorId")] Proposal proposal)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
                ViewBag.Lecturers = new SelectList(_context.Lecturers.Where(l => l.Domain == proposal.ProjectType), "Id", "Name", proposal.SupervisorId);

                return View(proposal);
            }

            var supervisor = await _context.Lecturers.FindAsync(proposal.SupervisorId);
            if (supervisor == null || supervisor.Domain != proposal.ProjectType)
            {
                ModelState.AddModelError("SupervisorId", "Selected supervisor does not match the selected project type");
                ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
                ViewBag.Lecturers = new SelectList(_context.Lecturers.Where(l => l.Domain == proposal.ProjectType), "Id", "Name", proposal.SupervisorId);

                return View(proposal);
            }

            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //public async Task<IActionResult> Edit(int id)
        //{
        //    var proposal = await _context.Proposals.FirstOrDefaultAsync(p => p.Id == id);
        //    if(proposal == null) return NotFound();
        //    ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
        //    ViewBag.Lecturers = new SelectList(_context.Lecturers, "Id", "Name", proposal.SupervisorId);
        //    return View(proposal);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(int id, [Bind("Id, Title, ProjectType, FilePath, Semester, Session, StudentId, SupervisorId")] Proposal proposal)
        //{
        //    if(id != proposal.Id) return NotFound();

        //    if(ModelState.IsValid)
        //    {
        //        _context.Update(proposal);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
        //    ViewBag.Lecturers = new SelectList(_context.Lecturers, "Id", "Name", proposal.SupervisorId);
        //    return View(proposal);
        //}

        public async Task<IActionResult> Edit1(int id)
        {
            var proposal = await _context.Proposals.FirstOrDefaultAsync(p => p.Id == id);
            if (proposal == null) return NotFound();
            return View(proposal);
        }
        [HttpGet]
        public async Task<IActionResult> Edit2(int id, DomainType projectType)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null) return NotFound();

            proposal.ProjectType = projectType;

            ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
            ViewBag.Lecturers = new SelectList(_context.Lecturers.Where(l => l.Domain == proposal.ProjectType), "Id", "Name", proposal.SupervisorId);

            return View(proposal);
        }
        [HttpPost]
        public async Task<IActionResult> Edit2(int id, [Bind("Id, Title, ProjectType, FilePath, Semester, Session, StudentId, SupervisorId")] Proposal proposal)
        {
            if(id != proposal.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
                ViewBag.Lecturers = new SelectList(_context.Lecturers.Where(l => l.Domain == proposal.ProjectType), "Id", "Name", proposal.SupervisorId);

                return View(proposal);
            }

            var supervisor = await _context.Lecturers.FindAsync(proposal.SupervisorId);
            if (supervisor == null || supervisor.Domain != proposal.ProjectType)
            {
                ModelState.AddModelError("SupervisorId", "Selected supervisor does not match the selected project type");
                ViewBag.Students = new SelectList(_context.Students, "Id", "Name", proposal.StudentId);
                ViewBag.Lecturers = new SelectList(_context.Lecturers.Where(l => l.Domain == proposal.ProjectType), "Id", "Name", proposal.SupervisorId);

                return View(proposal);
            }

            _context.Update(proposal);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Student)
                .Include(p => p.Supervisor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if(proposal == null) return NotFound();
            return View(proposal);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if(proposal != null)
            {
                _context.Proposals.Remove(proposal);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Student)
                .Include(p => p.Supervisor)
                .Include(p => p.Evaluator1)
                .Include(p => p.Evaluator2)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (proposal == null) return NotFound();
            return View(proposal);
        }

        public async Task<IActionResult> AssignEvaluators(int id)
        {

            var proposal = await _context.Proposals
                .Include(p => p.Student)
                .Include(p => p.Supervisor)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (proposal == null) return NotFound();

            var eligibleLecturers = _context.Lecturers
                .Where(l => l.Domain == proposal.ProjectType && l.Id != proposal.SupervisorId)
                .ToList();
            ViewBag.Lecturers = new SelectList(eligibleLecturers, "Id", "Name");
            return View(proposal);
        }
        [HttpPost]
        public async Task<IActionResult> AssignEvaluators(int id, int evaluator1Id, int evaluator2Id)
        {
            var proposal = await _context.Proposals
                .Include(p => p.Supervisor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proposal == null) return NotFound();

            if(evaluator1Id == evaluator2Id)
            {
                ModelState.AddModelError("", "Evaluator 1 and Evaluator 2 cannot be the same.");
            }

            if(evaluator1Id == proposal.SupervisorId || evaluator2Id == proposal.SupervisorId)
            {
                ModelState.AddModelError("", "Supervisor cannot be assigned as evaluator.");
            }

            if(!ModelState.IsValid)
            {
                var eligibleLecturers = _context.Lecturers
                    .Where(l => l.Domain == proposal.ProjectType && l.Id != proposal.SupervisorId)
                    .ToList();

                ViewBag.Lecturers = new SelectList(eligibleLecturers, "Id", "Name");
                return View(proposal);
            }

            proposal.Evaluator1Id = evaluator1Id;
            proposal.Evaluator2Id = evaluator2Id;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
