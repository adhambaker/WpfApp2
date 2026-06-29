using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using WpfApp2.Data;
using WpfApp2.Model;

namespace WpfApp2
{
    public partial class StudentAssignmentsWindow : Window
    {
        App_dbContext _db = new App_dbContext();
        Assignment? selectedAssignment;
        int _studentId;
       

        public StudentAssignmentsWindow(int studentId)
        {
            InitializeComponent();

            _studentId = studentId;
            LoadStudentName();
            LoadAssignments();
        }

        private void LoadStudentName()
        {
            var s = _db.Students.Find(_studentId);

            if (s != null)

                TxtStudentName.Text = $"Assignments for: {s.StudentName} (ID: {s.StudentId})";
        }

        private void LoadAssignments()
        {

            DgAssignments.ItemsSource = null;

            DgAssignments.ItemsSource = _db.Assignments
                .Where(a => a.StudentId == _studentId)
                .AsNoTracking()
                .ToList();
        }

        private void DgAssignments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedAssignment = DgAssignments.SelectedItem as Assignment;
        }

        private void AddAssignment_Click(object sender, RoutedEventArgs e)
        {


            AddEditAssignmentWindow win = new AddEditAssignmentWindow(_studentId);
            win.Owner = this;
            win.ShowDialog();
            LoadAssignments();
        }

        private void UpdateAssignment_Click(object sender, RoutedEventArgs e)
        {

            if (selectedAssignment == null) return;

            AddEditAssignmentWindow win = new AddEditAssignmentWindow(_studentId, selectedAssignment.AssignmentId);
            win.Owner = this;
            win.ShowDialog();
            LoadAssignments();
        }

        private void DeleteAssignment_Click(object sender, RoutedEventArgs e)
        {

            if (selectedAssignment == null) return;

            _db.Assignments.Remove(selectedAssignment);
            _db.SaveChanges();

            LoadAssignments();
        }

      
        private void DgAssignments_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if(e.PropertyName=="Student" || e.PropertyName=="StudentId" )
            {
                e.Cancel = true;
            }
        }
    }
}