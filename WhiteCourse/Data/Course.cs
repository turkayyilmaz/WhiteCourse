using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WhiteCourse.Data;
public class Course
{
    [Key]
    public int CourseId { get; set; }
    [DisplayName("Name")]
    public string CourseName { get; set; }
    [DisplayName("Description")]
    public string CourseDescription { get; set; }
    [DisplayName("Image(example.jpg)")]
    public string CourseImageUrl { get; set; }
    //many to many, courses can have a lot of students
    public ICollection<CourseRegister> CourseRegisters { get; set; }
    //one to many relationship
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

}