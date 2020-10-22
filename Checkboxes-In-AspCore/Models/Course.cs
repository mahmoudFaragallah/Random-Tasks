using System.Collections.Generic;

namespace Checkboxes_In_AspCore.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<StudentCourse> StudentCourse { get; set; }
    }
}