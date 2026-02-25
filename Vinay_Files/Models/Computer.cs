namespace Vinay_Files.Models
{
public class Computer
    {
        public int ComputerId {get;set;}
        public string Motherboard {get;set;} = "";
        public int CPUcores  {get;set;}
        public bool HasWifi  {get;set;}
        public bool HasLTE  {get;set;}
        public DateTime ReleaseDate  {get;set;}
        public string VideoCard  {get;set;}=""; //default setup for null
        public decimal Price  {get;set;}
        



      
    }
}