using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class AddressModel
    {
        [DataNames("AddressID")]
        public int AddressID { get; set; }
        [DataNames("Street")]
        public string Street { get; set; }
        [DataNames("City")]
        public string City { get; set; }
        [DataNames("Zip")]
        public int Zip { get; set; }
        [DataNames("State")]
        public string State { get; set; }
    }
    
}
