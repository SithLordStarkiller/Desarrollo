using System;
using System.Data;
using System.Data.OleDb;

namespace Universidad.RespaldoCatalogosDB.LogicaNegocios
{
    public class ConexionDataBase
    {

        string strAccessConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\BugTypes.MDB";
        string strAccessSelect = "SELECT * FROM Categories";

        public void ConnectDataBase()
        {
 
            // Create the dataset and add the Categories table to it:
            DataSet myDataSet = new DataSet();
            OleDbConnection myAccessConn = null;
            try
            {
                  myAccessConn = new OleDbConnection(strAccessConn);
            }
            catch(Exception ex)
            {
                  Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
                  return;
            }
 
            try
            {
            
                  OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect,myAccessConn);
                  OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
 
                  myAccessConn.Open();
                  myDataAdapter.Fill(myDataSet,"Categories");
 
            }
            catch (Exception ex)
            {
                  Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
                  return;
            }
            finally
            {
                  myAccessConn.Close();
            }
        }


    }
}
