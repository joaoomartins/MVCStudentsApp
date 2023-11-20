using System;
using System.Collections.Generic;

namespace MVCStudentsApp.PeopleModels;

public partial class CurricularUnit
{
    public int Id { get; set; }

    public string NameUnits { get; set; } = null!;

    public virtual ICollection<Objective> Objectives { get; set; } = new List<Objective>();
}
