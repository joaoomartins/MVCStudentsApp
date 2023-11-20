using System;
using System.Collections.Generic;

namespace MVCStudentsApp.PeopleModels;

public partial class Objective
{
    public int Id { get; set; }

    public int FkCurricularUnits { get; set; }

    public virtual CurricularUnit FkCurricularUnitsNavigation { get; set; } = null!;
}
