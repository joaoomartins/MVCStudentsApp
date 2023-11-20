using System;
using System.Collections.Generic;

namespace MVCStudentsApp.PeopleModels;

public partial class Role
{
    public int Id { get; set; }

    public string LabelRole { get; set; } = null!;

    public string DescriptionRole { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();
}
