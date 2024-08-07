using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Instructor : BaseModel //:User
    {
        public int UserId { get; set; }
        public User User { get; set; }

        [JsonIgnore]
        public List<Exam>? Exams { get; set; }
        [JsonIgnore]
        public List<Course>? Courses { get; set; }

    }
}
