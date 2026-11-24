using Instructor_Information_System.Data;
using Instructor_Information_System.Models;
using Instructor_Information_System.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Instructor_Information_System.Controllers;

[Authorize]
public class InstructorController : Controller
{
    private readonly ApplicationDbContext _context;

    public InstructorController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? search)
    {
        var query = _context.Instructors
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();
            query = query.Where(instructor => instructor.Name.Contains(search)
                || instructor.Email.Contains(search)
                || instructor.ContactNo.Contains(search)
                || instructor.Majorship.Contains(search)
                || instructor.Designation.Contains(search)
                || instructor.BachelorDegree.Contains(search));
        }

        var instructors = await query
            .OrderBy(instructor => instructor.Name)
            .ToListAsync();
        ViewData["Search"] = search;
        return View(instructors);
    }

    public IActionResult Create() => View(new Instructor());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("InstructorID,Name,Gender,Age,Email,Address,Majorship,Designation,ContactNo,BachelorDegree")] Instructor instructor)
    {
        if (await _context.Instructors.AnyAsync(item => item.InstructorID == instructor.InstructorID))
        {
            ModelState.AddModelError(nameof(instructor.InstructorID), "That instructor ID is already assigned.");
        }

        if (await _context.Instructors.AnyAsync(item => item.Email == instructor.Email))
        {
            ModelState.AddModelError(nameof(instructor.Email), "That email is already assigned to an instructor.");
        }

        if (!ModelState.IsValid)
        {
            return View(instructor);
        }

        instructor.LegacyPasswordHash = PasswordService.Hash(Guid.NewGuid().ToString("N"));
        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(string? id)
    {
        var instructor = await FindInstructorAsync(id);
        return instructor is null ? NotFound() : View(instructor);
    }

    public async Task<IActionResult> Edit(string? id)
    {
        var instructor = await FindInstructorAsync(id);
        return instructor is null ? NotFound() : View(instructor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("InstructorID,Name,Gender,Age,Email,Address,Majorship,Designation,ContactNo,BachelorDegree")] Instructor instructor)
    {
        if (id != instructor.InstructorID)
        {
            return NotFound();
        }

        if (await _context.Instructors.AnyAsync(item => item.Email == instructor.Email && item.InstructorID != id))
        {
            ModelState.AddModelError(nameof(instructor.Email), "That email is already assigned to an instructor.");
        }

        if (!ModelState.IsValid)
        {
            return View(instructor);
        }

        var existingInstructor = await _context.Instructors.FindAsync(id);
        if (existingInstructor is null)
        {
            return NotFound();
        }

        try
        {
            existingInstructor.Name = instructor.Name;
            existingInstructor.Gender = instructor.Gender;
            existingInstructor.Age = instructor.Age;
            existingInstructor.Email = instructor.Email;
            existingInstructor.Address = instructor.Address;
            existingInstructor.Majorship = instructor.Majorship;
            existingInstructor.Designation = instructor.Designation;
            existingInstructor.ContactNo = instructor.ContactNo;
            existingInstructor.BachelorDegree = instructor.BachelorDegree;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Instructors.AnyAsync(item => item.InstructorID == id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string? id)
    {
        var instructor = await FindInstructorAsync(id);
        return instructor is null ? NotFound() : View(instructor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor is not null)
        {
            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private Task<Instructor?> FindInstructorAsync(string? id) =>
        _context.Instructors.AsNoTracking().SingleOrDefaultAsync(item => item.InstructorID == id);
}