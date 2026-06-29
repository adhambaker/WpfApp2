using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp2.Data;
using WpfApp2.Model;

namespace StudentTaskSystem.Windows
{

    public partial class Student_UpdateStatus : Window
    {
        App_dbContext db = new App_dbContext();
        Assignment _selectedAssignemt;

        public Student_UpdateStatus(Assignment selectedAssignment)
        {
            _selectedAssignemt = selectedAssignment;
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (CmbStatus.Text == "")
            {
                MessageBox.Show("Please select a status");
                return;
            }
            _selectedAssignemt.AssignmentStatus = CmbStatus.Text;
            db.Assignments.Update(_selectedAssignemt);
            db.SaveChanges();
            Close();
        }
    }
}
