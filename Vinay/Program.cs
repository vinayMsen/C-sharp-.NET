
using System;
using Vinay.Data;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.Data.SqlClient;
using Vinay.Models;
namespace Vinay
{


    // models are collection of related funcationalities that are encapsulated with a single unit,
    // typically a library or a namespace 
    
    class Program
    {

        static void Print(object x)
        {
            Console.WriteLine(x);
        }
        // static string? Userinput()
        // {
        //     Console.ReadLine();
        // }
        static void Main( string [] args)
        {
            // using DataContextDapper to connect to database and perform operations
            DataContextDapper dapper= new DataContextDapper();
            

          string sqlcommand= "Select GETDATE()";
       DateTime rightnow= dapper.LoadDataSignle<DateTime>(sqlcommand);

        //    DateTime rightnow= dbConnection.QuerySingle<DateTime>(sqlcommand);
           Print(rightnow);


            Vinay.Models.Computer mycomputer = new Vinay.Models.Computer()
            {
                Price =978.56m,
                Motherboard= "z690",
                HasLTE= false,
                HasWifi= true,
                Videocard= "RTX 2060",
                Releasedate = DateTime.Now,
                CPUcores=8
                
            };
            string sqlinsert = @"INSERT INTO Computer(Price, Motherboard, HasLTE, HasWifi, Videocard, Releasedate, CPUcores)VALUES(@Price, @Motherboard, @HasLTE, @HasWifi, @Videocard, @Releasedate, @CPUcores)";

            int rowsaffectedcount =dapper.ExecutesqlReturnCount(sqlinsert,mycomputer);
             Print(rowsaffectedcount);
            Boolean rowsaffected =dapper.Executesql(sqlinsert,mycomputer);
            Print(rowsaffected);
            Print(sqlinsert);

            //pritning data of table
            string sqlselect= @"Select Price, Motherboard, HasLTE, HasWifi, Videocard, Releasedate, CPUcores FROM Computer";
            // this below statment will return IEnumerable type result so we are using this to hold return data
            IEnumerable<Computer>computers= dapper.LoadData<Computer>(sqlselect); 

            foreach(Computer comp in computers)
            {
                Print("Price : "+comp.Price+
                " Motherboard:"+comp.Motherboard+
                " HasLTE: "+comp.HasLTE+
                " haswifi :" +comp.HasWifi+
                " Videocard: "+comp.Videocard+
                " Releasedate: "+comp.Releasedate+
                " CPU: "+comp.CPUcores
                );
            }
            // Print(computer.Motherboard);
            // Print(computer.Videocard);
            // Print(computer.Price);
            // Print(computer.HasTLE);
            // Print(computer.Haswifi);
            // Print(computer.Releasedate);




 /*           writing other queries


            inserting values  // prevents sql injection , its a parameter binding
            string insertsql="INSERT INTO Computer(Price,Motherboard,HasLTE,HasWifi,Videocard,Releasedate,CPUcores)values(Price,Motherboard,HasLTE,HasWifi,Videocard,Releasedate,CPUcores)";
            dbConnection.Execute(insertsql, new
            {
                Price=2500.56,
                Motherboard="Z568h",
                HasLTE=false,
                HasWifi=true,
                Videocard="Radeon AMD5",
                ReleaseDate=DateTime.Now,
                CPUcores=2
            });
            dbConnection.Execute(insertsql, new
            {
                Price=3550.56,
                Motherboard="zqw68",
                HasLTE=true,
                HasWifi=true,
                Videocard="Radeon Graphics",
                ReleaseDate=DateTime.Now,
                CPUcores=4
            });

           
           var computerdetails = dbConnection.Query<Computer>("select * from computer");
           Print(computerdetails);
           foreach(var details in computerdetails)
            {
                Print(details.Price+" "+details.Motherboard+" "+details.HasLTE+" "+details.Haswifi+" "+details.Videocard+" "+details.Releasedate+" "+details.CPUcores);
            }

*/

        }
    }
}