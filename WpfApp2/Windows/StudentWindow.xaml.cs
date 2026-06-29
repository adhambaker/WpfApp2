using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp2;
using WpfApp2.Data;
using WpfApp2.Model;

namespace StudentTaskSystem.Windows
{

    public partial class StudentWindow : Window
    {
        App_dbContext _db = new App_dbContext();

        int _studentId;

        Assignment? selectedAssignment;
        public StudentWindow(int studentId)
        {
            _studentId = studentId;
            InitializeComponent();
            LoadAllAssignment();
        }
    

        public void LoadAllAssignment()
        {
            GdStudent.ItemsSource = _db.Assignments
                .Where(a => a.StudentId == _studentId)
                .AsNoTracking()
                .ToList();
        }
        private void GdStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAssignment = GdStudent.SelectedItem as Assignment;
        }

      
        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAssignment == null)
            {
                MessageBox.Show("you didn't select assignment");
                return;
            }
            Student_UpdateStatus su = new Student_UpdateStatus(selectedAssignment);
            
            su.Owner = this;
            su.ShowDialog();
            LoadAllAssignment();
        }

        private void logout_button(object sender, RoutedEventArgs e)
        {
            AuthWindow AW = new AuthWindow();
            AW.Show();
            this.Close();
        }

        private void GdStudent_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if(e.PropertyName== "StudentId" || e.PropertyName == "Student")
            {
                e.Cancel = true;
            }
        }
    }


}
