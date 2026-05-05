using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PROGRAMMING_3_PROJECT
{
    public partial class Form1 : Form
    {
        string connString = @"Data Source=.;Initial Catalog=ProjectDB;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();

            txtFullName.Enter += (s, e) =>
            {
                if (txtFullName.Text == "Full Name")
                {
                    txtFullName.Text = "";
                    txtFullName.ForeColor = System.Drawing.Color.Black;
                }
            };

            txtFullName.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    txtFullName.Text = "Full Name";
                    txtFullName.ForeColor = System.Drawing.Color.Gray;
                }
            };

            txtDept.Enter += (s, e) =>
            {
                if (txtDept.Text == "Department")
                {
                    txtDept.Text = "";
                    txtDept.ForeColor = System.Drawing.Color.Black;
                }
            };

            txtDept.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtDept.Text))
                {
                    txtDept.Text = "Department";
                    txtDept.ForeColor = System.Drawing.Color.Gray;
                }
            };
        }

        void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "SELECT * FROM Students";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvStudents.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "INSERT INTO Students (FullName, Department, EnrollmentDate) VALUES (@name, @dept, @date)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                cmd.Parameters.AddWithValue("@dept", txtDept.Text);
                cmd.Parameters.AddWithValue("@date", dtpEnrollment.Value);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Data added successfully");
                LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvStudents.CurrentRow.Cells["StudentID"].Value);

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "UPDATE Students SET FullName=@name, Department=@dept, EnrollmentDate=@date WHERE StudentID=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@dept", txtDept.Text);
                    cmd.Parameters.AddWithValue("@date", dtpEnrollment.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Data successfully updated");
                    LoadData();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvStudents.CurrentRow.Cells["StudentID"].Value);

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "DELETE FROM Students WHERE StudentID=@id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Removed data successfully");
                    LoadData();
                }
            }
        }
    }

    partial class Form1
    {
        private IContainer components = null;
        internal DataGridView dgvStudents;
        internal TextBox txtFullName;
        internal TextBox txtDept;
        internal DateTimePicker dtpEnrollment;
        internal Button btnInsert;
        internal Button btnUpdate;
        internal Button btnDelete;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.dgvStudents = new DataGridView();
            this.txtFullName = new TextBox();
            this.txtDept = new TextBox();
            this.dtpEnrollment = new DateTimePicker();
            this.btnInsert = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();

            // dgvStudents
            this.dgvStudents.Location = new System.Drawing.Point(12, 12);
            this.dgvStudents.Size = new System.Drawing.Size(560, 200);
            this.dgvStudents.Name = "dgvStudents";

            // txtFullName
            this.txtFullName.Location = new System.Drawing.Point(12, 220);
            this.txtFullName.Size = new System.Drawing.Size(200, 20);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Text = "Full Name";
            this.txtFullName.ForeColor = System.Drawing.Color.Gray;
            // txtDept
            this.txtDept.Location = new System.Drawing.Point(220, 220);
            this.txtDept.Size = new System.Drawing.Size(200, 20);
            this.txtDept.Name = "txtDept";
            this.txtDept.Text = "Department";
            this.txtDept.ForeColor = System.Drawing.Color.Gray;

            // dtpEnrollment
            this.dtpEnrollment.Location = new System.Drawing.Point(12, 250);
            this.dtpEnrollment.Name = "dtpEnrollment";
            this.dtpEnrollment.Size = new System.Drawing.Size(200, 20);

            // btnInsert
            this.btnInsert.Location = new System.Drawing.Point(12, 280);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Text = "Insert";
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // btnUpdate
            this.btnUpdate.Location = new System.Drawing.Point(100, 280);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // btnDelete
            this.btnDelete.Location = new System.Drawing.Point(188, 280);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // Form1
            this.ClientSize = new System.Drawing.Size(584, 321);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtDept);
            this.Controls.Add(this.dtpEnrollment);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Name = "Form1";
            this.Text = "Form1";
        }
    }
}