using System;
using System.Collections.Generic;

namespace MVCStudentsApp.PeopleModels;

public partial class ClassDetail
{
    public int Id { get; set; }

    public string NameClass { get; set; } = null!;

    public int YearClass { get; set; }

    public int TeacherId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Person Teacher { get; set; } = null!;
}
