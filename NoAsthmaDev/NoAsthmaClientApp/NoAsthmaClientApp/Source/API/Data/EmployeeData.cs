using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using NoAsthmaClientApp.Source.API.Helper;


namespace NoAsthmaClientApp.Source.API.Data
{
    //Helper class for creating/reading/update/deletion of Employee data
    public class EmployeeData : BaseData
    {
        SQLiteConnection Connection; /*= SQLiteHelper.OpenConn();*/

        EmployeeData()
        {
            Connection = SQLiteHelper.OpenConn();
        }

        //Register new Employee

        //Remove Employee

        //Update Employee info

        //Get Employee by username

        //Get Employee(s) by First Name

        //Get Employee(s) by Last Name

    }
}
