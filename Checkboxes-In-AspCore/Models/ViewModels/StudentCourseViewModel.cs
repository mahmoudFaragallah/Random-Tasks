using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkboxes_In_AspCore.ViewModels
{
    public class StudentCourseViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string RollNumber { get; set; }
        public List<CheckBoxItem> AvailableCourses { get; set; }

    }
}
