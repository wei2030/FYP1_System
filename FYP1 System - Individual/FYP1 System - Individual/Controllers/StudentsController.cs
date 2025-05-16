using FYP1_System___Individual.Data;
using FYP1_System___Individual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FYP1_System___Individual.Controllers
{
    public class StudentsController : Controller
    {
        private readonly FYP1_System_Context _context;

        public StudentsController(FYP1_System_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var student = await _context.Students.Include(s => s.Program).ToListAsync();
            return View(student);
        }

        public IActionResult Create()
        {
            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Email, Password, ProgramId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name", student.ProgramId);
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();

            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name", student.ProgramId);
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Email, Password, ProgramId")] Student student)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Programs = new SelectList(_context.AcademicPrograms, "Id", "Name", student.ProgramId);
            return View(student);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.Include(s => s.Program).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();
            return View(student);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
