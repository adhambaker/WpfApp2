
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using WpfApp2.Data;
using WpfApp2.Model;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        App_dbContext _db = new App_dbContext();
        Student? selectedStudent;


        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DgStudent.ItemsSource = _db.Students.ToList();
        }

        private void ClearInputs()
        {
            TextBoxOfName.Text = "";
            TextBoxOfAge.Text = "";
            TextBoxUser.Text = "";
            TextBoxPass.Text = "";
            selectedStudent = null;
            DgStudent.SelectedItem = null;
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var name = TextBoxOfName.Text;
            var ageText = TextBoxOfAge.Text;
            var username = TextBoxUser.Text;
            var password = TextBoxPass.Text;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(ageText) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter name,  age, username and password");
                return;
            }

            if (_db.Students.Any(u => u.Username == username))
            {
                MessageBox.Show("Username already exists");
                return;
            }

            var student = new Student
            {
                StudentName = name,
                StudentAge = int.TryParse(ageText, out var a) ? a : 0,
                Username = username,
                Password = password
            };

            _db.Students.Add(student);
            _db.SaveChanges();

            LoadData();
            ClearInputs();
        }

        private void DgStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStudent = DgStudent.SelectedItem as Student;
            if (selectedStudent != null)
            {
                TextBoxOfName.Text = selectedStudent.StudentName;
                TextBoxOfAge.Text = selectedStudent.StudentAge.ToString();
                TextBoxUser.Text = selectedStudent.Username;
                TextBoxPass.Text = selectedStudent.Password;


            }
        }

        private void Update_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudent is null)
            {
                MessageBox.Show("Please select student first");
                return;
            }

            var user = _db.Students.FirstOrDefault(u => u.StudentId == selectedStudent.StudentId);



            if (!string.IsNullOrWhiteSpace(TextBoxUser.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxPass.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxOfName.Text) &&
                !string.IsNullOrWhiteSpace(TextBoxOfAge.Text))
            {
                selectedStudent.StudentName = TextBoxOfName.Text;
                int.TryParse(TextBoxOfAge.Text, out int age);
                selectedStudent.StudentAge = age;
                selectedStudent.Username = TextBoxUser.Text;
                selectedStudent.Password = TextBoxPass.Text;


            }
            else
            {
                MessageBox.Show("Can not anyfield be empty");
                return;
            }

            _db.SaveChanges();
            LoadData();
            ClearInputs();
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudent == null) 
            { MessageBox.Show("Select a student first");
                return; 
            }

            _db.Students.Remove(selectedStudent);
            _db.SaveChanges();
            LoadData();
            ClearInputs();
        }

        private void ShowAssignments_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudent == null)
            {
                MessageBox.Show("Select a student first");
                return;
            }

            StudentAssignmentsWindow win = new StudentAssignmentsWindow(selectedStudent.StudentId);
            win.Owner = this;
            win.ShowDialog();
            LoadData();
        }

        private void ShowAllAssignments_Click(object sender, RoutedEventArgs e)
        {
            AllAssignmentsWindow win = new AllAssignmentsWindow();
            win.Owner = this;
            win.ShowDialog();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow auth = new AuthWindow();
            auth.Show();
            this.Close();
        }

        private void DgStudent_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Assignments")
                e.Cancel = true;
        }
    }
}
