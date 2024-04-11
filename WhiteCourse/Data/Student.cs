using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WhiteCourse.Data;
public class Student
{
    [Key]
    public int StudentId { get; set; }
    [DisplayName("Name")]
    public string StudentName { get; set; }
    [DisplayName("Surname")]
    public string StudentSurname { get; set; }
    [DisplayName("Email")]
    public string StudentFullName
    {
        get
        {
            return this.StudentName + " " + this.StudentSurname;
        }
    }
    public string StudentEmail { get; set; }
    [DisplayName("Phone")]
    public string StudentPhone { get; set; }
    //one to many, every student can join much courses
    public ICollection<CourseRegister> CourseRegisters{ get; set; }
}