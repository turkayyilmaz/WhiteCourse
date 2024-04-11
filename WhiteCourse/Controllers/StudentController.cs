using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteCourse.Data;

namespace WhiteCourse.Controllers;
public class StudentController : Controller
{
    private readonly DataContext _dataContext;
    public StudentController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IActionResult> List()
    {
        List<Student> students = await _dataContext.Students.ToListAsync();
        return View(students);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Student student)
    {
        _dataContext.Students.Add(student);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("List", "Student");
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id != null)
        {
            var value = await _dataContext.Students.Include(x => x.CourseRegisters).ThenInclude(x => x.Course).FirstOrDefaultAsync(x => x.StudentId == id);
            if (value != null)
            {
                return View(value);
            }
            else
            {
                return NotFound();
            }
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Student student)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _dataContext.Students.Update(student);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("List", "Student");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dataContext.Students.Any(x => x.StudentId == student.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        return NotFound();
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            var value = await _dataContext.Students.FirstOrDefaultAsync(x => x.StudentId == id);
            _dataContext.Students.Remove(value);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("List", "Student");
        }
        return NotFound();
    }
}