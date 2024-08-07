using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Student : BaseModel //:User
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public List<Course>? Course { get; set; }
        public List<Exam>? Exams { get; set; }
    }
}
