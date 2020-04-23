using CourseLibrary.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    [CourseTitleMustBeDifferentFromDiscriptionAttribute(ErrorMessage = "Tite and Description cannot be the same.")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public virtual string Description { get; set; }
    }
}
