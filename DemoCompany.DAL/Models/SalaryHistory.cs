using System;
using System.Collections.Generic;

namespace DemoCompany.DAL.Models;

public partial class SalaryHistory
{
    public int SalaryHistoryId { get; set; }

    public int EmployeeId { get; set; }

    public decimal OldSalary { get; set; }

    public decimal NewSalary { get; set; }

    public DateTime EffectiveDate { get; set; }

    public string? Reason { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
