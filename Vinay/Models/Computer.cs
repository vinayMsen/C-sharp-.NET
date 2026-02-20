namespace Vinay.Models
{
public class Computer
    {
        public string Motherboard {get;set;} = "";
        public int CPUcores  {get;set;}
        public bool HasWifi  {get;set;}
        public bool HasLTE  {get;set;}
        public DateTime Releasedate  {get;set;}
        public string Videocard  {get;set;}=""; //default setup for null
        public decimal Price  {get;set;}

      
    }
}