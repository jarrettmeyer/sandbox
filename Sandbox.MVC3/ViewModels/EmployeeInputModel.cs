using System;
using System.ComponentModel.DataAnnotations;
using Sandbox.MVC.Lib.Validators;

namespace Sandbox.MVC.ViewModels
{
    public class EmployeeInputModel
    {
        [Display(Name = "Date of Birth")]
        [Required]
        [SqlServerDate]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
    }
}