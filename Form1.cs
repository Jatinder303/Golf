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

                //Clear data from Textboxes

                txtID.Text = "";
                txtTitle.Text = "";
                txtFirstname.Text = "";
                txtSurname.Text = "";
                txtGender.Text = "";
                txtDOB.Text = "";
                txtStreet.Text = "";
                txtSuburb.Text = "";
                txtCity.Text = "";
                txtAvailable.Text = "";
                txtHandicap.Text = "";
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
                //  MessageBox.Show("Data Table not loaded");
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
                //  MessageBox.Show("Something is wrong");
            }
        }

        private void BtnAdd_Click(object sender, System.EventArgs e)
        {
            //Parametered insert query
            string AddQuery = "INSERT INTO Golf (Title, Firstname, Surname, Gender, DOB, Street, Suburb, City, [Available week days], handicap) VALUES (@Title, @Firstname, @Surname, @Gender, @DOB, @Street, @Suburb, @City, @Available, @Handicap)";

            using (SqlCommand Add_Cmd_Obj = new SqlCommand(AddQuery, Con))
            {
                Add_Cmd_Obj.Parameters.AddWithValue("@Title", txtTitle.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@Surname", txtSurname.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@Gender", txtGender.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@DOB", txtDOB.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@Street", txtStreet.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@Suburb", txtSuburb.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@City", txtCity.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@Available", txtAvailable.Text);
                Add_Cmd_Obj.Parameters.AddWithValue("@Handicap", txtHandicap.Text);

                //open Connection
                Con.Open();

                //execute insert query
                Add_Cmd_Obj.ExecuteNonQuery();

                //close connection
                Con.Close();

                MessageBox.Show("Successfully inserted new row");

                //After inserting the new row reload the Data Grid View 
                loaddb();
            }
        }

        private void BtnUpdate_Click(object sender, System.EventArgs e)
        {
            //Parametered insert query
            string UpdateQuery = "Update Golf set Title = @Title , Firstname = @Firstname, Surname =@Surname, Gender =@Gender , DOB =@DOB, Street = @Street, Suburb =@Suburb, City =@City, [Available week days] =@Available, handicap =@Handicap where ID = @ID";

            using (SqlCommand Update_Cmd_Obj = new SqlCommand(UpdateQuery, Con))
            {
                Update_Cmd_Obj.Parameters.AddWithValue("@ID", txtID.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@Title", txtTitle.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@Firstname", txtFirstname.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@Surname", txtSurname.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@Gender", txtGender.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@DOB", Convert.ToDateTime(txtDOB.Text));
                Update_Cmd_Obj.Parameters.AddWithValue("@Street", txtStreet.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@Suburb", txtSuburb.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@City", txtCity.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@Available", txtAvailable.Text);
                Update_Cmd_Obj.Parameters.AddWithValue("@Handicap", txtHandicap.Text);

                //open Connection
                Con.Open();

                //execute insert query
                Update_Cmd_Obj.ExecuteNonQuery();

                //close connection
                Con.Close();

                MessageBox.Show("Successfully inserted new row");

                //After inserting the new row reload the Data Grid View 
                loaddb();
            }
        }
    }
}
