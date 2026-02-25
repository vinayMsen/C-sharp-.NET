
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using Dapper;
using Vinay_Files.Data;
using Vinay_Files.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Vinay_Files
{


  /* Writing a Text File: The File class in C# defines two static methods to write a text file namely File.
  WriteAllText() and File.WriteAllLines().
The File.WriteAllText() writes the entire file at once. It takes two arguments, 
the path of the file and the text that has to be written.
The File.WriteAllLines() writes a file one line at a time.
 It takes two arguments, the path of the file and the text that has to be written, which is a string array.
*/
    
    class Program
    {

        static void Print(object x)
        {
            Console.WriteLine(x);
        }

        static void Main( string [] args)
        {

            IConfiguration config= new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
            
            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF ef = new DataContextEF(config);


            Vinay_Files.Models.Computer mycomputer = new Vinay_Files.Models.Computer()
            {
                Price =978.56m,
                Motherboard= "z690",
                HasLTE= false,
                HasWifi= true,
                VideoCard= "RTX 2060",
                ReleaseDate = DateTime.Now,
                CPUcores=8
                
            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + mycomputer.Motherboard
                    + "','" + mycomputer.HasWifi
                    + "','" + mycomputer.HasLTE
                    + "','" + mycomputer.ReleaseDate.ToString("yyyy-MM-dd")
                    + "','" + mycomputer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                    + "','" + mycomputer.VideoCard
            + "') \n";


           //  File.WriteAllText("log.txt", "\n" + sql + "\n"); //path,string to write
             
             using StreamWriter openFile= new ("log.txt", append:true);
             openFile.WriteLine(sql);

             openFile.Close();
             string filetext = File.ReadAllText("log.txt");
             Print(filetext);


        }
     }
}