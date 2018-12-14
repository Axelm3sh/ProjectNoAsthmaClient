using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NoAsthmaClientApp.Source.API.Data; //Backend Code
using NoAsthmaClientApp.Source.API.Helper;
using NoAsthmaClientApp.Source.Models;
using NoAsthmaClientApp.Forms;  //Use other Form Code

namespace NoAsthmaClientApp
{
    public partial class MasterForm : Form
    {
        
        public MasterForm() //Constructor
        {
            InitializeComponent();

            
        }

        private void ButtonGetAll_Click(object sender, EventArgs e)
        {
            //FormHandler.CreateForm(new ChildForm1());
            //SQLiteHelper.SpitoutPath();
            //SQLiteHelper.DebugCommand(null);

            AddressData addressData = new AddressData();

            //addressData.GetAddressByStreet("o");
            List<AddressModel> list = addressData.GetAllAddresses();

            MessageBox.Show(list.Count() + " items in the list");
            
        }

        private void MasterForm_Load(object sender, EventArgs e)
        {
            //SQLiteHelper.OpenConn();
        }

        private void MasterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            //SQLiteHelper.CloseConn(true);
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            AddressModel model = new AddressModel();
            List<AddressModel> newlist = new List<AddressModel>();

            try
            {
                //Input
                model.Street = textBoxStreet.Text;
                model.City = textBoxCity.Text;
                model.State = textBoxState.Text;
                model.Zip = Convert.ToInt32(textBoxZip.Text);

                //Add to list
                newlist.Add(model);

                AddressData addressData = new AddressData();

                //Insert to Database
                if (addressData.CreateNewAddress(newlist) > 0)
                {
                    //Clear old input if successful
                    textBoxStreet.Clear();
                    textBoxCity.Clear();
                    textBoxState.Clear();
                    textBoxZip.Clear();
                }

            }
            catch(Exception ex) //Format Exception or other exceptions
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void buttonSearchFor_Click(object sender, EventArgs e)
        {
            List<CustomerModel> newList = new List<CustomerModel>();
            CustomerData customerData = new CustomerData();

            CustomerData.CustomerSearch swapSearch;

            try
            {
                if(textBoxSearch.Text.Length <= 0)
                {
                    throw new Exception("NOT A VALID SEARCH TERM");
                }

                switch (comboBoxSearchMode.SelectedItem.ToString().ToLower())
                {
                    case "id":
                        swapSearch = CustomerData.CustomerSearch.ID;
                        break;
                    case "firstname":
                        swapSearch = CustomerData.CustomerSearch.FIRSTNAME;
                        break;
                    case "lastname":
                        swapSearch = CustomerData.CustomerSearch.LASTNAME;
                        break;
                    case "email":
                        swapSearch = CustomerData.CustomerSearch.EMAIL;
                        break;
                    default:
                        MessageBox.Show("Select a valid search field");
                        throw new Exception("NO VALID SEARCH FIELD");
                }

                newList = customerData.GetCustomerByData(textBoxSearch.Text, swapSearch);

                textBoxResultSearch.Clear();

                foreach (CustomerModel customers in newList)
                {
                    textBoxResultSearch.Text += String.Format("{0}, {1}, {2}\r\n", 
                        customers.CustFName, customers.CustLName, customers.CustEmail);
                }

            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                textBoxResultSearch.Clear();
                textBoxResultSearch.Text = exc.Message;
            }
        }

        private void comboBoxSearchMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NOT USED
        }

        //Other functions here

    }
}
