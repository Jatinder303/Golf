using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Golf
{
    public partial class Golf_database : Form
    {
        //connection string to connect to database
        private string connectionString = @"Data Source=DPKASTG-05\SQLEXPRESS;Initial Catalog=golf;Integrated Security=True";
        SqlConnection Con = new SqlConnection();
        DataTable GolfTable = new DataTable();




        public Golf_database()
        {
            InitializeComponent();
            Con.ConnectionString = connectionString;
            loaddb();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {

        }

        public void loaddb()
        {
            //Load columns in DataTable
            datatablecolumns();

            //Wrap code in using statement to dispose of it later
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string QueryString = @"SELECT * FROM Golf order by ID";

                //Open connection
                connection.Open();

                SqlCommand Command = new SqlCommand(QueryString, connection);

                //Start Database reader
                SqlDataReader reader = Command.ExecuteReader();

                while (reader.Read())
                {
                    //add each row in datatable
                    GolfTable.Rows.Add(
                                reader["ID"],
                                reader["Title"],
                                reader["FirstName"],
                                reader["Surname"],
                                reader["Gender"],
                                reader["DOB"],
                                reader["Street"],
                                reader["Suburb"],
                                reader["City"],
                                reader["Available Week Days"],
                                reader["Handicap"]
                                );

                }
                //Close Database reader
                reader.Close();
                //Close Connection
                connection.Close();
                //add datatable to the Data Grid View
                dgvGolf.DataSource = GolfTable;
            }
        }

        public void datatablecolumns()
        {
            //Clear old data
            GolfTable.Clear();

            //add column titles in the datatable
            try
            {
                GolfTable.Columns.Add("ID");
                GolfTable.Columns.Add("Title");
                GolfTable.Columns.Add("FirstName");
                GolfTable.Columns.Add("Surname");
                GolfTable.Columns.Add("Gender");
                GolfTable.Columns.Add("DOB");
                GolfTable.Columns.Add("Street");
                GolfTable.Columns.Add("Suburb");
                GolfTable.Columns.Add("City");
                GolfTable.Columns.Add("Available Week Days");
                GolfTable.Columns.Add("Handicap");
            }
            catch
            {
                MessageBox.Show("Data Table not loaded");
            }

        }

        private void DgvGolf_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string newvalue = dgvGolf.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                this.Text = "Row : " + e.RowIndex.ToString() + " Col : " + e.ColumnIndex.ToString() + " Value = " + newvalue;
                txtID.Text = dgvGolf.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTitle.Text = dgvGolf.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtFirstname.Text = dgvGolf.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSurname.Text = dgvGolf.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtGender.Text = dgvGolf.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtDOB.Text = dgvGolf.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtStreet.Text = dgvGolf.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtSuburb.Text = dgvGolf.Rows[e.RowIndex].Cells[7].Value.ToString();
                txtCity.Text = dgvGolf.Rows[e.RowIndex].Cells[8].Value.ToString();
                txtAvailable.Text = dgvGolf.Rows[e.RowIndex].Cells[9].Value.ToString();
                txtHandicap.Text = dgvGolf.Rows[e.RowIndex].Cells[10].Value.ToString();
            }
            catch
            {

            }
        }
    }
}
