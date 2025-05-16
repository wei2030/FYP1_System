using FYP1_System___Individual.Data;
using FYP1_System___Individual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FYP1_System___Individual.Controllers
{
    public class CommitteesController : Controller
    {
        private readonly FYP1_System_Context _context;

        public CommitteesController(FYP1_System_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> AssignDomain()
        {
            var lecturers = await _context.Lecturers.Include(l => l.Program).ToListAsync();
            return View(lecturers);
        }
        [HttpPost]
        public async Task<IActionResult> AssignDomain(int lecturerId, DomainType domain)
        {
            var lecturer = await _context.Lecturers.FindAsync(lecturerId);
            if (lecturer != null)
            {
                lecturer.Domain = domain;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("AssignDomain");
        }

        public async Task<IActionResult> ManageProposals(string? semester = null)
        {
            var proposals = await _context.Proposals
                .Include(p => p.Student)
                .Include(p => p.Supervisor)
                .Include(p => p.Evaluator1)
                .Include(p => p.Evaluator2)
                .Where(p => semester == null || p.Semester == semester)
                .ToListAsync();

            ViewBag.Semester = semester;
            return View(proposals);
        }

        public async Task<IActionResult> ProposalDetails(int id)
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
    }
}
