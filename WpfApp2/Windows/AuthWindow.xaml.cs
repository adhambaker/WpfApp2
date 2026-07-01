using StudentTaskSystem.Model;
using StudentTaskSystem.Windows;
using System.Windows;
using WpfApp2.Data;
using WpfApp2.Model;

namespace WpfApp2
{
    public partial class AuthWindow : Window
    {
        App_dbContext _db = new App_dbContext();
        public AuthWindow()
        {
            InitializeComponent();
            EnsureTeacherUser();
        }

        private void EnsureTeacherUser()
        {
            if (!_db.Teacher.Any(u => u.Username == "teacher"))
            {
                Teacher teacherUser = new Teacher
                {
                    Username = "teacher",
                    Password = "123",

                };
                _db.Teacher.Add(teacherUser);
                _db.SaveChanges();
            }
        }

        private void LoginStudent_Click(object sender, RoutedEventArgs e)
        {
            var u = TxtUsername.Text;
            var p = PwdPassword.Password;

            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
            {
                MessageBox.Show("Enter username and password.");
                return;
            }

            var user = _db.Students.FirstOrDefault(x => x.Username == u && x.Password == p);

            if (user == null)
            {
                MessageBox.Show("Invalid Username or Password");
                return;

            }
            else
            {
                StudentWindow sw = new StudentWindow(user.StudentId);
                sw.Show();
                this.Close();
                return;
            }
           
        }

        private void TeacherLogin_Click(object sender, RoutedEventArgs e)
        {

            var u = TxtUsername.Text;
            var p = PwdPassword.Password;

            if (string.IsNullOrEmpty(u) || string.IsNullOrEmpty(p))
            {
                MessageBox.Show("Enter username and password.");
                return;
            }

            var user = _db.Teacher.FirstOrDefault(x => x.Username == u && x.Password == p);

            if (user == null)
            {
                MessageBox.Show("Invalid Username or Password");
                return;

            }
            else
            {
                MainWindow sw = new MainWindow();
                sw.Show();
                this.Close();
                return;
            }


        }
    }
}
