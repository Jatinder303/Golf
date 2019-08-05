using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Golf
{
    public partial class Golf_database : Form
    {
        //
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
