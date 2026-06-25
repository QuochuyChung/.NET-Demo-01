using System;
using System.Collections.Generic;

namespace DemoCompany.DAL.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly WorkDate { get; set; }

    public TimeOnly? CheckIn { get; set; }

    public TimeOnly? CheckOut { get; set; }

    public string? Note { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
