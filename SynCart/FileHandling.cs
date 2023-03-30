using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynCart
{
    public class FileHandling
    {
        public static void Create()
        {
            if(!Directory.Exists("SynCart"))
            {
                System.Console.WriteLine("Creating Folder");
                Directory.CreateDirectory("SynCart");
            }
            else
            {
                System.Console.WriteLine("Directory Exist");
            }

            if(!File.Exists("SynCart/CustomerDetails.csv"))
            {
                System.Console.WriteLine("Creating File");
                File.Create("SynCart/CustomerDetails.csv").Close();
            }
            else 
            {
                System.Console.WriteLine("File Exist");
            }

            if(!File.Exists("SynCart/ProductDetails.csv"))
            {
                System.Console.WriteLine("Creating File");
                File.Create("SynCart/ProductDetails.csv").Close();
            }
            else 
            {
                System.Console.WriteLine("File Exist");
            }

            if(!File.Exists("SynCart/OrderDetails.csv"))
            {
                System.Console.WriteLine("Creating File");
                File.Create("SynCart/OrderDetails.csv").Close();
            }
            else 
            {
                System.Console.WriteLine("File Exist");
            }
        }

        public static void WriteToCSV()
        {
            string[] customerDetails=new string[Operations.customersList.Count];
            for(int i=0;i<Operations.customersList.Count;i++)
            {
                customerDetails[i]=Operations.customersList[i].CustomerID+","+Operations.customersList[i].CustomerName+","+Operations.customersList[i].City+","+Operations.customersList[i].MobileNumber+","+Operations.customersList[i].WalletBalance+","+Operations.customersList[i].EmailID;

            }
            File.WriteAllLines("SynCart/CustomerDetails.csv",customerDetails);

            string[] productDetails=new string[Operations.productsList.Count];
            for(int i=0;i<Operations.productsList.Count;i++)
            {
                productDetails[i]=Operations.productsList[i].ProductID+","+Operations.productsList[i].ProductName+","+Operations.productsList[i].Price+","+Operations.productsList[i].Stock+","+Operations.productsList[i].ShippingDuration;

            }
            File.WriteAllLines("SynCart/ProductDetails.csv",productDetails);

            string[] orderDetails=new string[Operations.ordersList.Count];
            for(int i=0;i<Operations.ordersList.Count;i++)
            {
                orderDetails[i]=Operations.ordersList[i].OrderID+","+Operations.ordersList[i].CustomerID+","+Operations.ordersList[i].ProductID+","+Operations.ordersList[i].TotalPrice+","+Operations.ordersList[i].PurchaseDate.ToString("dd/MM/yyyy")+","+Operations.ordersList[i].Quantity;

            }
            File.WriteAllLines("SynCart/OrderDetails.csv",orderDetails);
        }

        public static void ReadFromCSV()
        {
            string[] customerDetails=File.ReadAllLines("SynCart/CustomerDetails.csv");
            foreach(string customer in customerDetails)
            {
                Operations.customersList.Add(new CustomerDetails(customer));
            }

            string[] productDetails=File.ReadAllLines("SynCart/ProductDetails.csv");
            foreach(string product in productDetails)
            {
                Operations.productsList.Add(new ProductDetails(product));
            }

            string[] orderDetails=File.ReadAllLines("SynCart/OrderDetails.csv");
            foreach(string order in orderDetails)
            {
                Operations.ordersList.Add(new OrderDetails(order));
            }
        }
    }
}