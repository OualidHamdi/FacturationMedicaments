using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace VARIABLE
{
    class ADO
    {
        public SqlConnection con = new SqlConnection(@"Data Source=HAMDI\SQLEXPRESS;Initial Catalog=MEDICAMENT;Integrated Security=True;Pooling=False"); 
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dr; 
        public DataTable data = new DataTable();

        public DataRow ligne;
        public DataSet ds = new DataSet();
        public SqlDataAdapter dap = new SqlDataAdapter();
        public SqlCommandBuilder bc;


        public void Connecter()
        {
            if (con.State == ConnectionState.Closed || con.State == ConnectionState.Broken)
            {
                con.ConnectionString = @"Data Source=HAMDI\SQLEXPRESS;Initial Catalog=MEDICAMENT;Integrated Security=True;Pooling=False";
                con.Open();
            }
        }
        
        public void Deconnecter()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
