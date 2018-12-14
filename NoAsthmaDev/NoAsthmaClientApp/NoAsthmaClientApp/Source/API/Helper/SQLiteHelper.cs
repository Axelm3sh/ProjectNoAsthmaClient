using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace NoAsthmaClientApp.Source.API.Helper
{
    public static class SQLiteHelper
    {

        //This path fails
        //private const string ConnectionString = @"Data Source=Resources\NoAsthmaDB.db"; 

        //Variable for  SQLite database connection
        public static SQLiteConnection dbConnection;

        public static SQLiteConnection GetConnection()
        {
            return dbConnection;
        }

        /// <summary>
        /// Tries and open the default connection to database file
        /// </summary>
        public static SQLiteConnection OpenConn()
        {
            try
            {
                //Set connection string
                //dbConnection = new SQLiteConnection(ConnectionString);
                dbConnection = new SQLiteConnection("Data Source=" + SpitoutPath());

                dbConnection.SetExtendedResultCodes(true); //Spit out extended result codes instead of 30 coarse ones

                //Try and open connection
                dbConnection.Open();
                //Output response to console 
                Console.WriteLine(
                    String.Format("SQLite Connection DbResponse: {0}", 
                    dbConnection.ResultCode().ToString()));

            }
            catch(SQLiteException e)
            {
                //Allow user to handle exception
                DialogResult result = MessageBox.Show(e.Message, "SQLite DB Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                switch(result)
                {
                    case DialogResult.Abort:
                        Application.Exit();
                        break;
                        
                    case DialogResult.Ignore:
                        MessageBox.Show("No database used!");
                        break;

                    case DialogResult.Retry: //In case of Debugging, it's not hooked up to the new instance
                        Application.Exit();
                        Application.Restart();
                        break;
                }
            }

            return dbConnection;
        }

        /// <summary>
        /// Closes connection with the option to cancel the latest operation
        /// </summary>
        /// <param name="cancelLastOp">Abort the last database operation if true</param>
        public static void CloseConn(bool cancelLastOp = false)
        {
            try //Catch all for System errors
            {
                //Check to see if we want to cancel and also connection is valid
                if (cancelLastOp && (dbConnection.State != System.Data.ConnectionState.Closed))
                {
                    //Abort pending database changes
                    dbConnection.Cancel();
                }
                //Close the connection
                dbConnection.Close();

                dbConnection.Dispose();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "SQLite DB Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Returns the state of the connection
        /// </summary>
        /// <returns></returns>
        public static System.Data.ConnectionState GetConnectionState()
        {
            return dbConnection.State;
        }

        /// <summary>
        /// Checks to see if connection is opened
        /// </summary>
        /// <returns>The state of connection if it's opened or not</returns>
        public static bool IsConnectionOpen()
        {
            bool open = false;

            if (GetConnectionState() == System.Data.ConnectionState.Open)
            {
                open = true;
            }

            return open;
        }

        /// <summary>
        /// Checks to see if the current connection is free to write or read from. Should be used in conjunction with IsConnectionOpen
        /// </summary>
        /// <returns>A return False could mean something is reading, writing, or the connection was never opened.</returns>
        public static bool IsConnectionFree()
        {
            bool free = IsConnectionOpen();

            if ((GetConnectionState() == System.Data.ConnectionState.Executing) ||
                (GetConnectionState() == System.Data.ConnectionState.Fetching))
            {
                free = false;
            }

            return free;
        }

        /// <summary>
        /// Absolute path to modified debug directory
        /// </summary>
        /// <returns></returns>
        private static string SpitoutPath()
        {
            string relativePath, currentPath, absolutePath;

            relativePath = @"Resources\NoAsthmaDB.db";
            //currentPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            //currentPath = AppDomain.CurrentDomain.BaseDirectory;
            currentPath = Path.GetDirectoryName(Application.ExecutablePath).Replace(@"bin\Debug", string.Empty);

            absolutePath = Path.Combine(currentPath, relativePath);

            //debug
            Console.WriteLine(String.Format("Rel:{0},  Curr:{1},  Abs:{2}", relativePath, currentPath, absolutePath));

            return absolutePath;
        }


        /// <summary>
        /// DEBUG FOR QUERY
        /// </summary>
        /// <param name="tt"></param>
        public static void DebugCommand(string tt)
        {
            //actual SQL query
            tt = "INSERT INTO `EMPLOYEE`(`EmpUserName`,`EmpFName`,`EmpLName`, `EmpPassword`) VALUES (@param1,@param2,@param3, @param4);";
            // Multiple rows use: VALUES (@param1,@param2,@param3, @param4),(@param1,@param2,@param3, @param4), etc;
            //In the case we assume in code that we used the models to populate date beforehand in the forms
            Models.EmployeeModel employee = new Models.EmployeeModel
            {
                EmpUserName = "omg",
                EmpFName = "dbubugug",
                EmpLName = "",
                EmpPassword = "admin"
            };

            //example
            try
            {
                //New SQLite Command to be executed, passing in string for query and the connection
                SQLiteCommand comm = new SQLiteCommand(tt, dbConnection);

                //the constructor above should have already set this field, but just in case we can do it again but not necessary
                comm.CommandText = tt;
                //Set how the command will be processed
                comm.CommandType = System.Data.CommandType.Text;

                //TEST
                //SQLiteParameter pa = new SQLiteParameter("EmpUserName");

                //Adding Parameters to the string query assuming it is set up correctly and also setting their values
                //comm.Parameters.Add("EmpUserName", System.Data.DbType.String).Value = employee.EmpUserName;
                comm.Parameters.Add("@param1", System.Data.DbType.String).Value = employee.EmpUserName;
                comm.Parameters.Add("@param2", System.Data.DbType.String).Value = employee.EmpFName;
                comm.Parameters.Add("@param3", System.Data.DbType.String).Value = employee.EmpLName;

                //Alternatively
                //SQLiteParameter param = new SQLiteParameter("@param3", System.Data.DbType.String);
                //param.Value = employee.EmpLName;

                //exception thrown
                //comm.Parameters.Add(new SQLiteParameter("@param1", System.Data.DbType.String).Value = employee.EmpUserName);
                //comm.Parameters.Add(new SQLiteParameter("@param2", System.Data.DbType.String).Value = employee.EmpFName);
                //comm.Parameters.Add(new SQLiteParameter("@param3", System.Data.DbType.String).Value = employee.EmpLName);

                //Execute the non-Query(ie: not select) command, function passes back rows affected by the insert in this case
                int rows = comm.ExecuteNonQuery();

                Console.WriteLine("Rows affected {0}", rows);
            }
            catch(SQLiteException ex)
            {
                switch((SQLiteErrorCode)ex.ErrorCode)
                {
                    //DB Constraint errors
                    case SQLiteErrorCode.Constraint:
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                    //File is busy reading or writing by another process
                    case SQLiteErrorCode.Busy:
                        MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        break;
                    //Spit out an error since an exception threw anyways
                    default:
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
            catch(Exception ex) //Everything else
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
            }

        }

    }

}
