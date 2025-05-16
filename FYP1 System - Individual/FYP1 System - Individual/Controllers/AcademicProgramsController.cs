﻿using Microsoft.AspNetCore.Mvc;
using FYP1_System___Individual.Models;
using FYP1_System___Individual.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace FYP1_System___Individual.Controllers
{
    public class AcademicProgramsController : Controller
    {
        private readonly FYP1_System_Context _context;

        public AcademicProgramsController(FYP1_System_Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var programs = await _context.AcademicPrograms.ToListAsync();
            return View(programs);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name")] AcademicProgram programs)
        {
            if (ModelState.IsValid)
            {
                _context.AcademicPrograms.Add(programs);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(programs);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var program = await _context.AcademicPrograms.FirstOrDefaultAsync(x => x.Id == id);
            return View(program);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] AcademicProgram program)
        {
            if(id != program.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(program);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var program = await _context.AcademicPrograms.FirstOrDefaultAsync(x => x.Id == id);
            if(program == null) return NotFound();
            return View(program);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = await _context.AcademicPrograms.FindAsync(id);
            if(program != null)
            {
                _context.AcademicPrograms.Remove(program);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
