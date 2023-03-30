using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            FileHandling.Create();
            //Operations.DefaultDatas();
            FileHandling.ReadFromCSV();
            Operations.MainMenu();
            FileHandling.WriteToCSV();
        }   
    }
}