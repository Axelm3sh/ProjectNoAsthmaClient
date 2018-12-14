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
    public class CustomerData : BaseData
    {
        public CustomerData()
        {
            TableName = "CUSTOMER";
        }

        public enum CustomerSearch
        {
            ID,
            FIRSTNAME,
            LASTNAME,
            EMAIL
        }

        //New Customer
        public int CreateNewCustomer(List<CustomerModel> customer)
        {
            int affected = 0;

            string commandString = "INSERT INTO `CUSTOMER`(`CustFName`,`CustLName`,`CustEmail`) " +
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
                foreach (CustomerModel item in customer)
                {

                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.String).Value = item.CustFName;
                    comm.Parameters.Add("@p2", DbType.String).Value = item.CustLName;
                    comm.Parameters.Add("@p3", DbType.String).Value = item.CustEmail;

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

        //Get customer by Data
        public List<CustomerModel> GetCustomerByData(string searchFor, CustomerSearch columnName)
        {
            //Store raw query results to data table
            DataTable dt = new DataTable();

            //SQLite stuff
            SQLiteCommand comm = null;
            SQLiteConnection connection = null;

            string query = String.Format("SELECT * FROM {0} WHERE {0}.@C LIKE '%'||@param||'%';", TableName); //NEW FIX USE THIS CONCAT

            //useable Data after conversion to be returned
            List<CustomerModel> customerList = new List<CustomerModel>();

            try
            {
                connection = SQLiteHelper.OpenConn();

                //comm = new SQLiteCommand(query, connection);
                switch (columnName)
                {
                    case CustomerSearch.ID:
                        query = query.Replace("@C", "CustomerId");
                        comm = new SQLiteCommand(query, connection);
                        int idRes;
                        if (!int.TryParse(searchFor, out idRes))
                        {
                            throw new Exception("Customer ID search is restricted to numbers");
                        }
                        SQLiteParameter param = new SQLiteParameter("@param", DbType.Int32); //NEW FIX
                        param.Value = idRes;
                        comm.Parameters.Add(param);
                        break;
                    case CustomerSearch.FIRSTNAME:
                        query = query.Replace("@C", "CustFName");
                        comm = new SQLiteCommand(query, connection);
                        SQLiteParameter paramFName = new SQLiteParameter("@param", DbType.String);
                        paramFName.Value = searchFor;
                        comm.Parameters.Add(paramFName);
                        break;
                    case CustomerSearch.LASTNAME:
                        query = query.Replace("@C", "CustLName");
                        comm = new SQLiteCommand(query, connection);
                        SQLiteParameter paramLName = new SQLiteParameter("@param", DbType.String);
                        paramLName.Value = searchFor;
                        comm.Parameters.Add(paramLName);
                        break;
                    case CustomerSearch.EMAIL:
                        query = query.Replace("@C", "CustEmail");
                        comm = new SQLiteCommand(query, connection);
                        SQLiteParameter paramEmail = new SQLiteParameter("@param", DbType.String);
                        paramEmail.Value = searchFor;
                        comm.Parameters.Add(paramEmail);
                        break;

                }

                //Execute the command and load data to table
                SQLiteDataReader reader = comm.ExecuteReader();
                dt.Load(reader);

                //Closes reader stream then connection
                reader.Close();
                SQLiteHelper.CloseConn();

                //Use Datamapper to map selected results to objects
                DataNamesMapper<CustomerModel> mapper = new DataNamesMapper<CustomerModel>();

                customerList = mapper.Map(dt).ToList();

                //foreach (DataRow item in dt.Rows)
                //{
                //    Console.WriteLine(item["Street"].ToString()); //BREAKTHRUUUUUUUUU

                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                SQLiteHelper.CloseConn();
                MessageBox.Show(e.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

            return customerList;
        }

        //Update Customer (by ID)
        public int UpdateCustomer(List<CustomerModel> customerList)
        {

            int affected = 0;

            string commandString = "UPDATE `CUSTOMER` SET `CustFName` = @p1 ,`CustLName` = @p2,`CustEmail` = @p3 " +
                "WHERE CustomerID = @p4;";

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

                foreach (CustomerModel customer in customerList)
                {
                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.String).Value = customer.CustFName;
                    comm.Parameters.Add("@p2", DbType.String).Value = customer.CustLName;
                    comm.Parameters.Add("@p3", DbType.String).Value = customer.CustEmail;
                    comm.Parameters.Add("@p4", DbType.Int32).Value = customer.CustomerID;


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

        //Delete Customer
        public int DeleteCustomer(List<CustomerModel> customerList)
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

                foreach (CustomerModel customer in customerList)
                {
                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.Int32).Value = customer.CustomerID;

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
