using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WhiteCourse.Data;
public class Teacher
{
    [Key]
    public int TeacherId { get; set; }
    [DisplayName("Name")]
    public string TeacherName { get; set; }
    [DisplayName("Surname")]
    public string TeacherSurname { get; set; }
    public string TeacherFullName { get { return this.TeacherName + " " + this.TeacherSurname; } }
    [DisplayName("Email")]
    public string TeacherEmail { get; set; }
    [DisplayName("Phone")]
    public string TeacherPhone { get; set; }
    [DisplayName("Start Date")]
    [DataType(DataType.Date)] //we want to take only th date value not hour
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }
    //one to many
    public ICollection<Course> Courses { get; set; }
}