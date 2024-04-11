using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteCourse.Data;

namespace WhiteCourse.Controllers;
public class CourseController : Controller
{
    private readonly DataContext _dataContext;
    public CourseController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<IActionResult> List()
    {
        List<Course> courses = await _dataContext.Courses.Include(x => x.Teacher).ToListAsync();
        return View(courses);
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Teachers = new SelectList(await _dataContext.Teachers.ToListAsync(), "TeacherId", "TeacherFullName");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Course course)
    {
        _dataContext.Courses.Add(course);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("List", "Course");
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id != null)
        {
            var value = await _dataContext.Courses.Include(x => x.CourseRegisters).ThenInclude(x => x.Student).FirstOrDefaultAsync(x => x.CourseId == id);
            ViewBag.Teachers = new SelectList(await _dataContext.Teachers.ToListAsync(), "TeacherId", "TeacherFullName");
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
    public async Task<IActionResult> Edit(Course course)
    {
        if(course.TeacherId ==0)
        {
            return View();
        }
        if (ModelState.IsValid)
        {
                _dataContext.Courses.Update(course);
                await _dataContext.SaveChangesAsync();
                return RedirectToAction("List", "Course");
        }
        return View();
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            var value = await _dataContext.Courses.FirstOrDefaultAsync(x => x.CourseId == id);
            _dataContext.Courses.Remove(value);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("List", "Course");
        }
        return NotFound();
    }
 
}