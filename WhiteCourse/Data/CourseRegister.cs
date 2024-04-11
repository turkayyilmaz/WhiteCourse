using System.ComponentModel.DataAnnotations;

namespace WhiteCourse.Data;
public class CourseRegister
{
    //many to many relationship
    [Key]
    public int CourseRegisterId { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime RegisterDate { get; set; }

}