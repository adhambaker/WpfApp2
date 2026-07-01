using StudentTaskSystem.Model;
using System.Collections.Generic;

namespace WpfApp2.Model
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = "";
        public int StudentAge { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";


        public List<Assignment> Assignments { get; set; } = new();
        public List<Teacher> Teachers { get; set; } = new();
    }
}
