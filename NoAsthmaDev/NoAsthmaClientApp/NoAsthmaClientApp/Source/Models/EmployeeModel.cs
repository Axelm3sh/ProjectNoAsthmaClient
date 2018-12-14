using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoAsthmaClientApp.Source.Models
{
    public class EmployeeModel
    {
        [DataNames("EmpUserName")]
        public string EmpUserName { get; set; }
        [DataNames("EmpFName")]
        public string EmpFName { get; set; }
        [DataNames("EmpLName")]
        public string EmpLName { get; set; }
        [DataNames("EmpPassword")]
        public string EmpPassword { get; set; }
        [DataNames("IsManager")]
        public bool IsManager { get; set; }
    }

}
