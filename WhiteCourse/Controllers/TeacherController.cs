using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteCourse.Data;

namespace WhiteCourse.Controllers;
public class TeacherController : Controller
{
    private readonly DataContext _dataContext;
    public TeacherController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IActionResult> List()
    {
        List<Teacher> Teachers = await _dataContext.Teachers.ToListAsync();
        return View(Teachers);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Teacher teacher)
    {
        _dataContext.Teachers.Add(teacher);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("List", "Teacher");
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id != null)
        {
            var value = await _dataContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);
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
    public async Task<IActionResult> Edit(Teacher teacher)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _dataContext.Teachers.Update(teacher);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("List", "Teacher");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dataContext.Teachers.Any(x => x.TeacherId == teacher.TeacherId))
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
            var value = await _dataContext.Teachers.FirstOrDefaultAsync(x => x.TeacherId == id);
            _dataContext.Teachers.Remove(value);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("List", "Teacher");
        }
        return NotFound();
    }
}