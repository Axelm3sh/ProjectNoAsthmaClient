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
    public class CustomerInfoData : BaseData
    {
        public CustomerInfoData()
        {
            TableName = "CUSTOMER_INFO";
        }

        public enum CustomerInfoSearch
        {
            ID,
            CUSTOMERID,
            ADDRESSID
        }

        public CustomerInfoModel AttachCustomerAndAddress(CustomerModel customer, AddressModel address)
        {
            CustomerInfoModel newModel = new CustomerInfoModel();

            newModel.CustomerModelInfo = customer;
            newModel.AddressModelInfo = address;
            
            
            return newModel;
        }

        //New customer Info (needs Customer Model, and Address Model inside the CustomerInfoModel)
        public int CreateNewCustomerInfo(List<CustomerInfoModel> customerinfo)
        {
            int affected = 0;

            string commandString = "INSERT INTO `CUSTOMER_INFO`(`CustomerId`,`AddressId`) " +
                "VALUES (@p1,@p2);";

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
                foreach (CustomerInfoModel item in customerinfo)
                {

                    comm.CommandText = commandString;
                    comm.Parameters.Add("@p1", DbType.Int32).Value = item.CustomerID;
                    comm.Parameters.Add("@p2", DbType.Int32).Value = item.AddressID;

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

        //Get Customer Info by
        public List<CustomerInfoModel> GetCustomerInfoByData(string searchFor, CustomerInfoSearch columnName)
        {
            //Store raw query results to data table
            DataTable dt = new DataTable();

            //SQLite stuff
            SQLiteCommand comm = null;
            SQLiteConnection connection = null;

            string query = String.Format("SELECT * FROM {0} WHERE {0}.@C LIKE '%@param%';", TableName);

            //useable Data after conversion to be returned
            List<CustomerInfoModel> assemblyList = new List<CustomerInfoModel>();

            try
            {
                connection = SQLiteHelper.OpenConn();

                //comm = new SQLiteCommand(query, connection);
                switch (columnName)
                {
                    case CustomerInfoSearch.ID:
                        query = query.Replace("@C", "CustomerInfoId");
                        comm = new SQLiteCommand(query, connection);
                        int idRes;
                        if (!int.TryParse(searchFor, out idRes))
                        {
                            throw new Exception("Customer Info ID search is restricted to numbers");
                        }
                        comm.Parameters.Add("@param", DbType.Int32).Value = idRes;
                        break;
                    case CustomerInfoSearch.CUSTOMERID:
                        query = query.Replace("@C", "CustomerId");
                        comm = new SQLiteCommand(query, connection);
                        int partIdRes;
                        if (!int.TryParse(searchFor, out partIdRes))
                        {
                            throw new Exception("Customer ID search is restricted to numbers");
                        }
                        comm.Parameters.Add("@param", DbType.Int32).Value = partIdRes;
                        break;
                    case CustomerInfoSearch.ADDRESSID:
                        query = query.Replace("@C", "AddressId");
                        comm = new SQLiteCommand(query, connection);
                        int quantity;
                        if (!int.TryParse(searchFor, out quantity))
                        {
                            throw new Exception("Address ID search is restricted to numbers");
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
                DataNamesMapper<CustomerInfoModel> mapper = new DataNamesMapper<CustomerInfoModel>();

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
        
    }
}
