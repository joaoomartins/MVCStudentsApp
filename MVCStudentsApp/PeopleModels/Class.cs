using System;
using System.Collections.Generic;

namespace MVCStudentsApp.PeopleModels;

public partial class Class
{
    public int Id { get; set; }

    public int ClassDetailsId { get; set; }

    public int StudentId { get; set; }

    public virtual ClassDetail ClassDetails { get; set; } = null!;

    public virtual Person Student { get; set; } = null!;
}
