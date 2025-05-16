using FYP1_System___Individual.Data;
using FYP1_System___Individual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FYP1_System___Individual.Controllers
{
    public class LecturersController : Controller
    {
        private readonly FYP1_System_Context _context;

        public LecturersController(FYP1_System_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lecturer = await _context.Lecturers.Include(l => l.Program).ToListAsync();
            return View(lecturer);
        }

        public IActionResult Create()
        {
            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Email, Password, ProgramId")] Lecturer lecturer)
        {
            if (ModelState.IsValid)
            {
                _context.Lecturers.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name", lecturer.ProgramId);
            return View(lecturer);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(x => x.Id == id);
            if(lecturer == null) return NotFound();

            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name", lecturer.ProgramId);
            return View(lecturer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Email, Password, ProgramId")] Lecturer lecturer)
        {
            if(id != lecturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name", lecturer.ProgramId);
            return View(lecturer);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lecturer = await _context.Lecturers.Include(l => l.Program).FirstOrDefaultAsync(x => x.Id == id);
            if (lecturer == null) return NotFound();
            return View(lecturer);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer != null)
            {
                _context.Lecturers.Remove(lecturer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
