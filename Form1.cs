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
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
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
    }
}
