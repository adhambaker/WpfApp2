using System.Windows;
using WpfApp2.Data;
using WpfApp2.Model;

namespace WpfApp2
{
    public partial class AddEditAssignmentWindow : Window
    {
        App_dbContext _db = new App_dbContext();
        int _studentId;
        int? _assignmentId;
        Assignment? checkAssignment;

        public AddEditAssignmentWindow(int studentId, int? assignmentId = null)
        {
            InitializeComponent();
            _studentId = studentId;
            _assignmentId = assignmentId;

            if (_assignmentId.HasValue)
                LoadAssignment();
        }

        private void LoadAssignment()
        {
            checkAssignment = _db.Assignments.Find(_assignmentId);
            if (checkAssignment != null)
            {
                TxtBoxOfTitle.Text = checkAssignment.AssignmentTitle;
                TxtBoxOfDesc.Text = checkAssignment.AssignmentDesc;
                CmbOfStatus.Text = checkAssignment.AssignmentStatus;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (checkAssignment != null)
            {
                checkAssignment.AssignmentTitle = TxtBoxOfTitle.Text;
                checkAssignment.AssignmentDesc = TxtBoxOfDesc.Text;
                checkAssignment.AssignmentStatus = CmbOfStatus.Text;
            }
            else
            {
                _db.Assignments.Add(new Assignment
                {
                    AssignmentTitle = TxtBoxOfTitle.Text,
                    AssignmentDesc = TxtBoxOfDesc.Text,
                    AssignmentStatus = CmbOfStatus.Text,
                    StudentId = _studentId 
                });
            }

            _db.SaveChanges();
            Close();

        }
    }
}
