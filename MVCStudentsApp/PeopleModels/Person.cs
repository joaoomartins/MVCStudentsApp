using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCStudentsApp.PeopleModels;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public int FkRole { get; set; }

    public virtual ICollection<ClassDetail> ClassDetails { get; set; } = new List<ClassDetail>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Role FkRoleNavigation { get; set; } = null!;
}
