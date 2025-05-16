﻿using FYP1_System___Individual.Data;
using FYP1_System___Individual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FYP1_System___Individual.Controllers
{
    public class SupervisorsController : Controller
    {
        private readonly FYP1_System_Context _context;

        public SupervisorsController(FYP1_System_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> MyStudents(int supervisorId, string? semester = null, string? session = null)
        {
            IQueryable<Proposal> query = _context.Proposals
                .Include(p => p.Student)
                .Include(p => p.Supervisor)
                .Where(p => p.SupervisorId == supervisorId);

            if (!string.IsNullOrEmpty(semester))
            {
                query = query.Where(p => p.Semester == semester);
            }

            if (!string.IsNullOrEmpty(session))
            {
                query = query.Where(p => p.Session == session);
            }

            var proposals = await query.ToListAsync();

            ViewBag.SupervisorId = supervisorId;
            ViewBag.Semester = semester;
            ViewBag.Session = session;

            return View(proposals);
        }

        public async Task<IActionResult> ViewProposal(int id)
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

        [HttpPost]
        public async Task<IActionResult> CommentOnProposal(int id, string supervisorComment)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null) return NotFound();

            proposal.SupervisorComment = supervisorComment;
            await _context.SaveChangesAsync();

            return RedirectToAction("ViewProposal", proposal.SupervisorId);
        }
    }
}
