using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoAsthmaClientApp.Source.Models;
using NoAsthmaClientApp.Source.API.Helper;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace NoAsthmaClientApp.Source.API.Data
{
    public class AssemblyGroupData : BaseData
    {
        public AssemblyGroupData()
        {
            TableName = "ASSEMBLY_GROUP";
        }

        public enum AssemblySearch
        {
            ID,
            PARTSID,
            QUANTITY            
        }

        //Add new Assembly Group
        public int CreateNewAssemblyGroup(List<AssemblyGroupModel> assembly)
        {
            int affected = 0;

            string commandString = "INSERT INTO `ASSEMBLY_GROUP`(`AssemblyId`,`PartsId`,`PartsQuantity`) " +
                "VALUES (@p1,@p2,@p3);";

            SQLiteConnection connection = null;
            SQLiteTransaction transaction = null;
            SQLiteCommand comm = null;

            try
            {
                //Open a new connection
                connection = SQLiteHelper.OpenConn();
                //Start transaction (ATOMICITY)
                transaction = connection.BeginTransaction();

                //Start a new query and assign the transaction 
                comm = connection.CreateCommand();
                comm.Transaction = transaction;

                //Start inserting items
                foreach (AssemblyGroupModel item in assembly)
                {

                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.Int32).Value = item.AssemblyID;
                    comm.Parameters.Add("@p2", DbType.Int32).Value = item.PartsID;
                    comm.Parameters.Add("@p3", DbType.Int32).Value = item.PartsQuantity;

                    affected += comm.ExecuteNonQuery();

                }

                //Commit transaction
                transaction.Commit();

                //Close connection
                SQLiteHelper.CloseConn();

            }
            catch (Exception e)
            {
                //Transaction Rollback Failure
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                        affected = 0;
                    }
                    catch (SQLiteException sqlEx)
                    {
                        MessageBox.Show("Transaction rollback Failure: " + sqlEx.Message);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                Console.WriteLine(e.Message);
                SQLiteHelper.CloseConn();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                affected = -1;
            }
            return affected;
        }

        //Get Assembly by Data
        public List<AssemblyGroupModel> GetAssemblyByData(string searchFor, AssemblySearch columnName)
        {
            //Store raw query results to data table
            DataTable dt = new DataTable();

            //SQLite stuff
            SQLiteCommand comm = null;
            SQLiteConnection connection = null;

            string query = String.Format("SELECT * FROM {0} WHERE {0}.@C LIKE '%@param%';", TableName);

            //useable Data after conversion to be returned
            List<AssemblyGroupModel> assemblyList = new List<AssemblyGroupModel>();

            try
            {
                connection = SQLiteHelper.OpenConn();

                //comm = new SQLiteCommand(query, connection);
                switch (columnName)
                {
                    case AssemblySearch.ID:
                        query = query.Replace("@C", "AssemblyId");
                        comm = new SQLiteCommand(query, connection);
                        int idRes;
                        if (!int.TryParse(searchFor, out idRes))
                        {
                            throw new Exception("Assembly ID search is restricted to numbers");
                        }
                        comm.Parameters.Add("@param", DbType.Int32).Value = idRes;
                        break;
                    case AssemblySearch.PARTSID:
                        query = query.Replace("@C", "PartsId");
                        comm = new SQLiteCommand(query, connection);
                        int partIdRes;
                        if (!int.TryParse(searchFor, out partIdRes))
                        {
                            throw new Exception("Parts ID search is restricted to numbers");
                        }
                        comm.Parameters.Add("@param", DbType.Int32).Value = partIdRes;
                        break;
                    case AssemblySearch.QUANTITY:
                        query = query.Replace("@C", "PartsQuantity");
                        comm = new SQLiteCommand(query, connection);
                        int quantity;
                        if (!int.TryParse(searchFor, out quantity))
                        {
                            throw new Exception("Parts ID search is restricted to numbers");
                        }
                        comm.Parameters.Add("@param", DbType.Int32).Value = quantity;
                        break;

                }

                //Execute the command and load data to table
                SQLiteDataReader reader = comm.ExecuteReader();
                dt.Load(reader);

                //Closes reader stream then connection
                reader.Close();
                SQLiteHelper.CloseConn();

                //Use Datamapper to map selected results to objects
                DataNamesMapper<AssemblyGroupModel> mapper = new DataNamesMapper<AssemblyGroupModel>();

                assemblyList = mapper.Map(dt).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                SQLiteHelper.CloseConn();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            return assemblyList;
        }

        //Update Assembly
        public int UpdateAssembly(List<AssemblyGroupModel> assemblyList)
        {

            int affected = 0;

            string commandString = "UPDATE `ASSEMBLY_GROUP` SET `PartsId` = @p1 ,`PartsQuantity` = @p2 " +
                "WHERE AssemblyId = @p3;";

            SQLiteConnection connection = null;
            SQLiteTransaction transaction = null;
            SQLiteCommand comm = null;

            try
            {
                //Open a new connection
                connection = SQLiteHelper.OpenConn();
                //Start transaction (ATOMICITY)
                transaction = connection.BeginTransaction();

                //Start a new query and assign the transaction 
                comm = connection.CreateCommand();
                comm.Transaction = transaction;

                foreach (AssemblyGroupModel item in assemblyList)
                {
                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.Int32).Value = item.PartsID;
                    comm.Parameters.Add("@p2", DbType.Int32).Value = item.PartsQuantity;
                    comm.Parameters.Add("@p3", DbType.Int32).Value = item.AssemblyID;


                    affected += comm.ExecuteNonQuery();
                }

                //Commit transaction
                transaction.Commit();

                //Close connection
                SQLiteHelper.CloseConn();

            }
            catch (Exception e)
            {
                //Transaction Rollback Failure
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                        affected = 0;
                    }
                    catch (SQLiteException sqlEx)
                    {
                        MessageBox.Show("Transaction rollback Failure: " + sqlEx.Message);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                Console.WriteLine(e.Message);
                SQLiteHelper.CloseConn();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                affected = -1;
            }
            return affected;
        }

        //Delete Assembly
        public int DeleteAssembly(List<AssemblyGroupModel> assemblyList)
        {
            int affected = 0;

            string commandString = "DELETE FROM `ASSEMBLY_GROUP` WHERE ASSEMBLY_GROUP.AssemblyId = @p1;";

            SQLiteConnection connection = null;
            SQLiteTransaction transaction = null;
            SQLiteCommand comm = null;

            try
            {
                //Open a new connection
                connection = SQLiteHelper.OpenConn();
                //Start transaction (ATOMICITY)
                transaction = connection.BeginTransaction();

                //Start a new query and assign the transaction 
                comm = connection.CreateCommand();
                comm.Transaction = transaction;

                foreach (AssemblyGroupModel item in assemblyList)
                {
                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.Int32).Value = item.AssemblyID;

                    affected += comm.ExecuteNonQuery();
                }

                //Commit transaction
                transaction.Commit();

                //Close connection
                SQLiteHelper.CloseConn();

            }
            catch (Exception e)
            {
                //Transaction Rollback Failure
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                        affected = 0;
                    }
                    catch (SQLiteException sqlEx)
                    {
                        MessageBox.Show("Transaction rollback Failure: " + sqlEx.Message);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
                Console.WriteLine(e.Message);
                SQLiteHelper.CloseConn();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                affected = -1;
            }
            return affected;
        }
    }
}
