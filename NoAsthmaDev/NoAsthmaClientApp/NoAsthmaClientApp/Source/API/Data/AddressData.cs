using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using NoAsthmaClientApp.Source.Models; //Data Model
using NoAsthmaClientApp.Source.API.Helper;
using System.Data;
using System.Windows.Forms;

namespace NoAsthmaClientApp.Source.API.Data
{
    public class AddressData : BaseData
    {
        public AddressData()
        {
            TableName = "ADDRESS";
        }

        public enum AddressSearch
        {
            ID,
            STREET,
            CITY,
            ZIP,
            STATE
        }

        //New Address(es)
        //INSERT INTO `ADDRESS`(`AddressID`,`Street`,`City`,`Zip`,`State`) VALUES (1001,'','',0,'');
        public int CreateNewAddress(List<AddressModel> address)
        {
            int affected = 0;
            
            string commandString ="INSERT INTO `ADDRESS`(`Street`,`City`,`Zip`,`State`) " +
                "VALUES (@p1,@p2,@p3,@p4);";

            SQLiteConnection connection = null;
            SQLiteTransaction transaction = null;
            SQLiteCommand comm = null;

            try
            {
                //Open a new connection
                connection = SQLiteHelper.OpenConn();
                //Start transaction (ATOMICITY)
                transaction =  connection.BeginTransaction();

                //Start a new query and assign the transaction 
                comm = connection.CreateCommand();
                comm.Transaction = transaction;

                //Start inserting items
                foreach (AddressModel item in address)
                {
                    //String format allows for SQL injection attacks
                    //comm.CommandText = String.Format(commandString, item.Street, item.City, item.Zip, item.State);

                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.String).Value = item.Street;
                    comm.Parameters.Add("@p2", DbType.String).Value = item.City;
                    comm.Parameters.Add("@p3", DbType.Int32).Value = item.Zip;
                    comm.Parameters.Add("@p4", DbType.String).Value = item.State;

                    affected += comm.ExecuteNonQuery();

                }

                //Commit transaction
                transaction.Commit();
                
                //Close connection
                SQLiteHelper.CloseConn();
                
            }
            catch(Exception e)
            {
                //Transaction Rollback Failure
                if(transaction != null)
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

        public List<AddressModel> GetAllAddresses()
        {
            //Store raw query results to data table
            DataTable dt = new DataTable();

            //SQLite stuff
            SQLiteCommand comm = null;
            SQLiteConnection connection = null;

            string query = "SELECT * FROM ADDRESS;";

            //useable Data after conversion to be returned
            List<AddressModel> addressList = new List<AddressModel>();

            try
            {
                connection = SQLiteHelper.OpenConn();

                comm = new SQLiteCommand(query, connection);

                //Execute the command and load data to table
                SQLiteDataReader reader = comm.ExecuteReader();
                dt.Load(reader);

                //Closes reader stream then connection
                reader.Close();
                SQLiteHelper.CloseConn();

                //Use Datamapper to map selected results to objects
                DataNamesMapper<AddressModel> mapper = new DataNamesMapper<AddressModel>();

                addressList = mapper.Map(dt).ToList();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                SQLiteHelper.CloseConn();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return addressList;
        }

        //Get Address by column 
        public List<AddressModel> GetAddressByData(string searchFor, AddressSearch columnName)
        {
            //Store raw query results to data table
            DataTable dt = new DataTable();

            //SQLite stuff
            SQLiteCommand comm = null;
            SQLiteConnection connection = null;

            string query = "SELECT * FROM ADDRESS WHERE ADDRESS.@C LIKE '%@param%';";
            
            //useable Data after conversion to be returned
            List<AddressModel> addressList = new List<AddressModel>();
            
            try
            {
                connection = SQLiteHelper.OpenConn();
                
                //comm = new SQLiteCommand(query, connection);
                switch (columnName)
                {
                    case AddressSearch.ID:
                        query = query.Replace("@C", "AddressId");
                        comm = new SQLiteCommand(query, connection);
                        int idRes;
                        if (!int.TryParse(searchFor, out idRes))
                        {
                            throw new Exception("Address ID search is restricted to numbers");
                        }
                        comm.Parameters.Add("@param", DbType.Int32).Value = idRes;
                        break;
                    case AddressSearch.STREET:
                        query = query.Replace("@C", "Street");
                        comm = new SQLiteCommand(query, connection);
                        comm.Parameters.Add("@param", DbType.String).Value = searchFor;
                        break;
                    case AddressSearch.CITY:
                        query = query.Replace("@C", "City");
                        comm = new SQLiteCommand(query, connection);
                        comm.Parameters.Add("@param", DbType.String).Value = searchFor;
                        break;
                    case AddressSearch.ZIP:
                        query = query.Replace("@C", "Zip");
                        comm = new SQLiteCommand(query, connection);
                        int results;
                        if(!int.TryParse(searchFor, out results))
                        {
                            throw new Exception("ZIP code is restricted to numbers");
                        }
                        comm.Parameters.Add("@param", DbType.Int32).Value = results;
                        break;
                    case AddressSearch.STATE:
                        query = query.Replace("@C", "State");
                        comm = new SQLiteCommand(query, connection);
                        comm.Parameters.Add("@param", DbType.String).Value = searchFor;
                        break;
                }
                
                //Execute the command and load data to table
                SQLiteDataReader reader = comm.ExecuteReader();
                dt.Load(reader);

                //Closes reader stream then connection
                reader.Close();
                SQLiteHelper.CloseConn();

                //Use Datamapper to map selected results to objects
                DataNamesMapper<AddressModel> mapper = new DataNamesMapper<AddressModel>();
                
                addressList = mapper.Map(dt).ToList();

                //foreach (DataRow item in dt.Rows)
                //{
                //    Console.WriteLine(item["Street"].ToString()); //BREAKTHRUUUUUUUUU
                    
                //}
                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                SQLiteHelper.CloseConn();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            return addressList;
        }

        //Update Address
        public int UpdateAddress(List<AddressModel> addressList)
        {

            int affected = 0;

            string commandString = "UPDATE `ADDRESS` SET `Street` = @p1 ,`City` = @p2,`Zip` = @p3,`State` = @p4 " +
                "WHERE AddressId = @p5;";

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

                foreach (AddressModel address in addressList)
                {
                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.String).Value = address.Street;
                    comm.Parameters.Add("@p2", DbType.String).Value = address.City;
                    comm.Parameters.Add("@p3", DbType.Int32).Value = address.Zip;
                    comm.Parameters.Add("@p4", DbType.String).Value = address.State;
                    comm.Parameters.Add("@p5", DbType.Int32).Value = address.AddressID;

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

        //Delete
        public int DeleteAddress(List<AddressModel> addressList)
        {
            int affected = 0;

            string commandString = "DELETE FROM `ADDRESS` WHERE ADDRESS.AddressId = @p1;";

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

                foreach (AddressModel address in addressList)
                {
                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.Int32).Value = address.AddressID;

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
