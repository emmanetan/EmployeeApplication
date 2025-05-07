using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using EmployeeNamespace;
using Siticone.UI.WinForms;

namespace EmployeeApplication
{
    public partial class frmEmployeeDatabase : Form
    {
        private DataTable table;
        private SiticoneDataGridView dgridView;

        public frmEmployeeDatabase()
        {
            InitializeComponent();
            InitializeDataGridView();
        }

        public void InitializeDataGridView()
        {
            //dataTable
            table = new DataTable();
            table.Columns.Add("id", typeof(long));
            table.Columns.Add("firstName", typeof(string));
            table.Columns.Add("lastName", typeof(string));
            table.Columns.Add("position", typeof(string));

            //layout 
            dgridView = new SiticoneDataGridView
            {
                //Dock = DockStyle.Fill,
                ThemeStyle =
                {
                    AlternatingRowsStyle =
                    {
                        BackColor = Color.FromArgb(238, 239, 249)
                    },
                    RowsStyle =
                    {
                        BackColor = Color.White,
                        ForeColor = Color.FromArgb(71, 69, 94),
                        SelectionBackColor = Color.FromArgb(231, 229, 255),
                        SelectionForeColor = Color.FromArgb(71, 69, 94)
                    },

                    HeaderStyle =
                    {
                        BackColor = Color.FromArgb(94, 148, 255),
     
                    }
                },
                GridColor = Color.FromArgb(94, 148, 255),
                ReadOnly = true,
                Size = new Size(421, 274),
                Location = new Point(248, 80),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoGenerateColumns = true          
            };

            // binding the datatable to the datagridview
            dgridView.DataSource = table;
            this.Controls.Add(dgridView);
        }

        private void ClearFields()
        {
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPosition.Clear();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (string.IsNullOrWhiteSpace(txtEmployeeID.Text) ||
                    string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                    string.IsNullOrWhiteSpace(txtLastName.Text) ||
                    string.IsNullOrWhiteSpace(txtPosition.Text))
                {
                    MessageBox.Show("All fields are required. Please fill in all fields.",    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (!long.TryParse(txtEmployeeID.Text, out long employeeID))
                {
                    MessageBox.Show("Invalid Employee ID. Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string position = txtPosition.Text;

                //creating an employee object
                Employee employee = new Employee(employeeID, firstName, lastName, position);

                //add the employee data to the dataTtable
                table.Rows.Add(employee.EmployeeID, employee.FirstName, employee.LastName, employee.Position);

                //clear input
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
