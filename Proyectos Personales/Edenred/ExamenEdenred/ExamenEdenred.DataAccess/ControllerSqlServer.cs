namespace ExamenEdenred.DataAccess
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections.Generic;

    public class ControllerSqlServer
    {
        public static DataTable ExecuteDataTable(string sqlConnectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(sqlConnectionString)) throw new ArgumentNullException("sqlConnectionString");
            try
            {
                // Create & open a SqlConnection, and dispose of it after we are done
                using (var connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                    // Call the overload that takes a connection in place of the connection string
                    var dt = ExecuteDataTable(connection, commandType, commandText, commandParameters);
                    return dt;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Errors);
                return null;
            }
        }

        public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            var cmd = new SqlCommand();
            var dt = new DataTable();
            try
            {
                bool mustCloseConnection;
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
                // Create the DataAdapter & DataSet
                using (var da = new SqlDataAdapter(cmd))
                {

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(dt);
                    // Detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    if (mustCloseConnection)
                    {
                        connection.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Errors);
                return null;
            }

            return dt;
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
        {
            // Pass through the call providing null for the set of SqlParameters
            return ExecuteNonQuery(connectionString, commandType, commandText, null);
        }

        public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
            try
            {
                // Create & open a SqlConnection, and dispose of it after we are done
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Call the overload that takes a connection in place of the connection string
                    return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Errors);
                return -1;
            }
        }

        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");
            var cmd = new SqlCommand();
            bool mustCloseConnection;
            int retval;

            try
            {
                // Create a command and prepare it for execution                
                PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out mustCloseConnection);
                // Finally, execute the command
                retval = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Errors);
                return -1;
            }
            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            if (mustCloseConnection)
                connection.Close();
            return retval;
        }

        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (var p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
    }
}