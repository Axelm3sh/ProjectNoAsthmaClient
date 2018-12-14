using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class SalesModel
    {
        [DataNames("SalesID")]
        public int SalesID { get; set; }
        [DataNames("InvoiceID")]
        public int InvoiceID { get; set; }
        [DataNames("CustInfoID")]
        public int CustInfoID { get; set; }
        [DataNames("EmpUserName")]
        public string EmpUserName { get; set; }
        [DataNames("Date")]
        public DateTime Date { get; set; }
    }
}
