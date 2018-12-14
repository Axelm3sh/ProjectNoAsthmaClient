using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class CustomerInfoModel
    {
        [DataNames("CustInfoID")]
        public int CustInfoID { get; set; }

        [DataNames("CustomerID")]
        public int CustomerID
        {
            get
            {
                return CustomerModelInfo.CustomerID;
            }
            set
            {
                CustomerID = value;
            }
            
        }

        [DataNames("AddressID")]
        public int AddressID
        {
            get
            {
                return AddressModelInfo.AddressID;
            }
            set
            {
                AddressID = value;
            }
        }

        public AddressModel AddressModelInfo { get; set; }
        public CustomerModel CustomerModelInfo { get; set; }
    }
}
