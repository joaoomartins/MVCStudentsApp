using MVCStudentsApp.PeopleModels;

namespace MVCStudentsApp.Models
{
    public class ClassDetailsLinq
    {
        public Person person { get; set; }
        public Role role { get; set; }

        public ClassDetail classDetail { get; set; }
    }
}
