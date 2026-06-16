using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DemoCompany.DAL.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public string? Location { get; set; }

    public decimal? Budget { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
