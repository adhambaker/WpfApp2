
using Microsoft.EntityFrameworkCore;
using System.Windows;
using WpfApp2.Data;
namespace WpfApp2
{
    public partial class AllAssignmentsWindow : Window
    {
        App_dbContext _db = new App_dbContext();

        public AllAssignmentsWindow()
        {
            InitializeComponent();
            LoadAll();
        }

        private void LoadAll()
        {
            AllAssignGrid.ItemsSource = _db.Assignments.ToList();
               
        }

        private void AllAssignGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Student")
                e.Cancel = true;
        }
    }
}