using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class CustomerModel
    {
        [DataNames("CustomerID")]
        public int CustomerID { get; set; }
        [DataNames("CustFName")]
        public string CustFName { get; set; }
        [DataNames("CustLName")]
        public string CustLName { get; set; }
        [DataNames("CustEmail")]
        public string CustEmail { get; set; }
    }
}
