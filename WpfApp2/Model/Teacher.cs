using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Model;

namespace StudentTaskSystem.Model
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

        public List<Student> students { get; set; }

    }
}
