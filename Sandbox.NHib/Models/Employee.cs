using System;

namespace Sandbox.NHib.Models
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime DateOfHire { get; set; }
        public virtual string SSN { get; set; }
        public virtual Department CurrentDepartment { get; set; }
    }
}
