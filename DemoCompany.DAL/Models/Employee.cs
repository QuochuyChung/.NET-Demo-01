using System;
using System.Collections.Generic;

namespace DemoCompany.DAL.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Position { get; set; }

    public decimal? Salary { get; set; }

    public DateOnly? HireDate { get; set; }

    public int? DepartmentId { get; set; }

    public bool? IsActive { get; set; }

    //Navigation Property
    public virtual Department? Department { get; set; }
}
