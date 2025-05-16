using FYP1_System___Individual.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FYP1_System___Individual.Controllers
{
    public class AccountController : Controller
    {
        private readonly FYP1_System_Context _context;

        public AccountController(FYP1_System_Context context)
        {  
            _context = context; 
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.Email == email && s.Password == password);
            if (student != null)
            {
                HttpContext.Session.SetString("UserId", student.Id.ToString());
                HttpContext.Session.SetString("UserRole", "Student");
                return RedirectToAction("Index", "Proposals");
            }

            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(l => l.Email == email && l.Password == password);
            if (lecturer != null)
            {
                HttpContext.Session.SetString("UserId", lecturer.Id.ToString());
                HttpContext.Session.SetString("UserRole", "Lecturer");
                return RedirectToAction("Index", "Proposals");
            }

            ViewBag.Error = "Invalid email and password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
