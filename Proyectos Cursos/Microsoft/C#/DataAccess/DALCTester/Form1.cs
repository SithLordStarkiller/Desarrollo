using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DALC4NET;
using System.Reflection;
using System.Configuration;
using System.Xml;

namespace DALCTester
{
    public partial class Form1 : Form
    {
        private DBHelper _dbHelper = null;

        public Form1()
        {
            InitializeComponent();
            _dbHelper = new DBHelper();
            
            lblDBName.Text = "Database/ Provider: " +  _dbHelper.Database + "/ " + _dbHelper.Provider;
        }

        private const string _connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Program Files\AccountPlus\Database\AccountPlus.mdb;Jet OLEDB:Database Password=admin;";
        private const string _dbProvider = "System.Data.OleDb";

        

        private void button1_Click(object sender, EventArgs e)
        {
            InsertSQL();
        }

        private DataTable SelectSql()
        {
            DataTable table = new DataTable();      
            string sqlCommand = "SELECT * FROM UserTypes";
            
            try
            {            
                table = _dbHelper.ExecuteDataTable(sqlCommand);                
            }
            catch (Exception err)
            {                
                MessageBox.Show(err.Message);
            }

            return table;
        }

        private object InsertSQL()
        {
            object retValue = null;
            string sqlCommand = "INSERT INTO UserTypes VALUES(@ID, @VALUE)";            
            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(new DBParameter("@ID", 13));
            paramCollection.Add(new DBParameter("@VALUE", "Eleven"));
            IDbTransaction transaction = _dbHelper.BeginTransaction();
            

            try
            {                
                IDataReader objScalar = _dbHelper.ExecuteDataReader(sqlCommand, paramCollection, transaction, CommandType.Text);

                if (objScalar != null)
                {
                    objScalar.Close();
                    objScalar.Dispose();
                }
                _dbHelper.CommitTransaction(transaction);
            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
                MessageBox.Show(err.Message);
            }

            return retValue;
        }

        #region
        private void btnConstructor1_Click(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper();
        }

        private void btnConstructor2_Click(object sender, EventArgs e)
        {
            DBHelper helper = new  DBHelper("con");
        }

        private void btnConstructor3_Click(object sender, EventArgs e)
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\Program Files\AccountPlus\Database\AccountPlus.mdb;Jet OLEDB:Database Password=admin;";
            string providerName = "System.Data.OleDb";

            DBHelper helper = new DBHelper(connectionString, providerName);
        }
        #endregion

        private void btnHelpConstructor1_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Description = "This instance does not require any parameter. This overload creates connection for the connection string name mentions as the default connection. ";
            help.ConstructorInfo = "DBHelper helper = new DBHelper();";
            help.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnExecScalar1_Click(object sender, EventArgs e)
        {
            string sqlCommand = "SELECT Count(1) FROM USERDETAILS";
            object objCont = _dbHelper.ExecuteScalar(sqlCommand);

            MessageBox.Show(objCont.ToString());
        }

        private void btnExecScalar2_Click(object sender, EventArgs e)
        {
            string sqlCommand = "SELECT Count(1) FROM USERDETAILS WHERE FIRSTNAME = @FIRSTNAME";
            object objCont = _dbHelper.ExecuteScalar(sqlCommand, new DBParameter("@FIRSTNAME","ashish"));

            MessageBox.Show(objCont.ToString());
        }

        private void btnExecScalar3_Click(object sender, EventArgs e)
        {
            string sqlCommand = "SELECT Count(1) FROM USERDETAILS WHERE FIRSTNAME = @FIRSTNAME AND LASTNAME = @LASTNAME";
            DBParameter param1 = new DBParameter("@FIRSTNAME", "ashish");
            DBParameter param2 = new DBParameter("@LASTNAME", "tripathi");

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);

            object objCont = _dbHelper.ExecuteScalar(sqlCommand, paramCollection);

            MessageBox.Show(objCont.ToString());
        }

        private void btnExecScalar4_Click(object sender, EventArgs e)
        {           
            object objCont = _dbHelper.ExecuteScalar("PROC_DALC4NET_STORED_PROC_WITHOUT_PARAM", CommandType.StoredProcedure);
            MessageBox.Show(objCont.ToString());
            
        }

        private void btnExecScalar5_Click(object sender, EventArgs e)
        {
            object objCont = _dbHelper.ExecuteScalar("PROC_DALC4NET_EXECUTE_SCALAR_SINGLE_PARAM", new DBParameter("@FIRSTNAME", "ashish"), CommandType.StoredProcedure);
            MessageBox.Show(objCont.ToString());                        
        }

        private void btnExecScalar6_Click(object sender, EventArgs e)
        {
            DBParameter param1 = new DBParameter("@FIRSTNAME", "ashish");
            DBParameter param2 = new DBParameter("@LASTNAME", "tripathi");

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);

            object objCont = _dbHelper.ExecuteScalar("PROC_DALC4NET_EXECUTE_SCALAR_MULTIPLE_PARAM", paramCollection, CommandType.StoredProcedure);
            MessageBox.Show(objCont.ToString());
            
        }

        private void btnExecNonQuery1_Click(object sender, EventArgs e)
        {
            string insertCommand = "INSERT INTO USERDETAILS (FirstName, LastName, Email) VALUES  ('Ashish', 'Tripathi', 'ak.tripathi@yahoo.com')";
            string message = _dbHelper.ExecuteNonQuery(insertCommand) > 0 ? "Record inserted successfully." : "Error in inserting record." ;

            MessageBox.Show(message);
            

        }

        private void btnMisc1_Click(object sender, EventArgs e)
        {
            DBParameter param1 = new DBParameter("@FIRSTNAME", "Yash");
            DBParameter param2 = new DBParameter("@LASTNAME", "Tripathi");
            DBParameter param3 = new DBParameter("@EMAIL", "yash.tripathi@yahoo.com");
            DBParameter outParam = new DBParameter("@USERID", 0, DbType.Int32,  ParameterDirection.Output);

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);
            paramCollection.Add(param3);
            paramCollection.Add(outParam);

            IDbTransaction transaction = _dbHelper.BeginTransaction();
            IDbCommand command = null;            
            object retValue = null;
            try
            {
                command = _dbHelper.GetCommand("PROC_DALC4NET_RETRIEVE_OUTPUT_VALUE", paramCollection, transaction, CommandType.StoredProcedure);
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                retValue = _dbHelper.GetParameterValue(3, command);
                 _dbHelper.CommitTransaction(transaction);                
            }
            catch (Exception err)
            {
                  _dbHelper.RollbackTransaction(transaction);
            }
            finally
            {                                 
                _dbHelper.DisposeCommand(command);                
            }

            MessageBox.Show(retValue != null ? "Returened Value is: " + retValue.ToString() : "null");
                        
        }

        private void btnExecNonQuery3_Click(object sender, EventArgs e)
        {
            DBParameter param1 = new DBParameter("@FIRSTNAME", "Yash");
            DBParameter param2 = new DBParameter("@LASTNAME", "Tripathi");
            DBParameter param3 = new DBParameter("@EMAIL", "yash.tripathi@yahoo.com");

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);
            paramCollection.Add(param3);

            string message = _dbHelper.ExecuteNonQuery("PROC_DALC4NET_EXECUTE_NON_QUERY_STORED_PROC_MULTIPLE_PARAM", paramCollection, CommandType.StoredProcedure) > 0 ? "Record inserted successfully." : "Error in inserting record.";

            MessageBox.Show(message);

        }

        private void btnExecNonQuery4_Click(object sender, EventArgs e)
        {
            DBParameter param1 = new DBParameter("@FIRSTNAME", "Yash");
            DBParameter param2 = new DBParameter("@LASTNAME", "Tripathi");

            DBParameterCollection paramCollection = new DBParameterCollection();
            IDbTransaction transaction = _dbHelper.BeginTransaction();
            paramCollection.Add(param1);
            paramCollection.Add(param2);
            string message = "";
            string insertCommand = "INSERT INTO USERDETAILS (FirstName, LastName, Email)  VALUES (@FIRSTNAME, @LASTNAME, 'ak.tripathi@yahoo.com')";
            try
            {
                message = _dbHelper.ExecuteNonQuery(insertCommand, paramCollection, transaction) > 0 ? "Record inserted successfully." : "Error in inserting record.";
                _dbHelper.CommitTransaction(transaction);
            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
            }
            MessageBox.Show(message);

        }

        private void btnExecNonQuery5_Click(object sender, EventArgs e)
        {
            string message = "";
            int rowsAffected = 0;
            DBParameter param1 = new DBParameter("@FIRSTNAME", "Yash");
            DBParameter param2 = new DBParameter("@LASTNAME", "Tripathi");
            DBParameter param3 = new DBParameter("@EMAIL", "yash.tripathi@yahoo.com");

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);
            paramCollection.Add(param3);

          
            IDbTransaction transaction = _dbHelper.BeginTransaction();

            try
            {                
                rowsAffected = _dbHelper.ExecuteNonQuery("PROC_DALC4NET_EXECUTE_NON_QUERY_STORED_PROC_MULTIPLE_PARAM", paramCollection, transaction, CommandType.StoredProcedure);
                message = rowsAffected > 0 ? "Record inserted successfully." : "Error in inserting record.";
                _dbHelper.CommitTransaction(transaction);
            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
            }
            MessageBox.Show(message);
        }

        private void btnExecNonQuery2_Click(object sender, EventArgs e)
        {
            DBParameter param1 = new DBParameter("@FIRSTNAME", "Yash");
            DBParameter param2 = new DBParameter("@LASTNAME", "Tripathi");

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);

            string insertCommand = "INSERT INTO USERDETAILS (FirstName, LastName, Email)  VALUES (@FIRSTNAME, @LASTNAME, 'ak.tripathi@yahoo.com')";
            string message = _dbHelper.ExecuteNonQuery(insertCommand, paramCollection) > 0 ? "Record inserted successfully." : "Error in inserting record.";

            MessageBox.Show(message);
        }

        
        private void btnExecReader1_Click(object sender, EventArgs e)
        {
            IDbConnection connection = _dbHelper.GetConnObject();
            string message = "";
            IDataReader reader = null;
            try
            {
                reader = _dbHelper.ExecuteDataReader("SELECT * FROM USERDETAILS", connection);
                while (reader.Read())
                {
                    message = message + reader["FirstName"].ToString() + Environment.NewLine;
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }

                if (reader != null)
                {
                    reader.Close();
                    reader.Dispose();
                }
            }

            MessageBox.Show(message);
        }

        private void btnExecDataTable1_Click(object sender, EventArgs e)
        {
            string sqlCommand = "SELECT * FROM USERDETAILS";
            DataTable data = _dbHelper.ExecuteDataTable(sqlCommand);
            
            DataTableRepresentation tableView = new DataTableRepresentation(data);
            tableView.Data = data;
            tableView.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            Help help = new Help();
            help.Description = "This instance does not require any parameter. This overload creates connection for the connection string name mentions as the default connection. ";
            help.ConstructorInfo = "DBHelper helper = new DBHelper('connectionNam');";
            help.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Description = "This instance does not require any parameter. This overload creates connection for the connection string name mentions as the default connection. ";
            help.ConstructorInfo = "DBHelper helper = new DBHelper(connectionName, providerName);";
            help.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

    }
}