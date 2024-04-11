using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteCourse.Data;

namespace WhiteCourse.Controllers;
public class CourseRegisterController : Controller
{
    private readonly DataContext _dataContext;
    public CourseRegisterController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<ActionResult> List()
    {
        var courseregisters = await _dataContext.CourseRegisters.Include(x => x.Student).Include(x => x.Course).ToListAsync();
        return View(courseregisters);
    }
    public async Task<IActionResult> Create()
    {
        //We take the data from db for the selectbox.
        ViewBag.Students = new SelectList(await _dataContext.Students.ToListAsync(), "StudentId", "StudentFullName");
        ViewBag.Courses = new SelectList(await _dataContext.Courses.ToListAsync(), "CourseId", "CourseName");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CourseRegister model)
    {
        model.RegisterDate = DateTime.Now;
        _dataContext.CourseRegisters.Add(model);
        await _dataContext.SaveChangesAsync();
        return RedirectToAction("List", "CourseRegister");
    }
    

    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            var value = await _dataContext.CourseRegisters.FirstOrDefaultAsync(x => x.CourseRegisterId == id);
            _dataContext.CourseRegisters.Remove(value);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("List", "CourseRegister");
        }
        return NotFound();
    }
}