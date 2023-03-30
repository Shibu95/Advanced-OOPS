using System;


namespace SynCart
{
    public class ProductDetails
    {
        //Create field for customerID as static
        private static int s_produtID=100;

        
        //Create properties for customer details
        public string ProductID{get;}
        public string ProductName{get;set;}
        public double Price{get;set;}
        public int Stock{get;set;}
        public int ShippingDuration{get;set;}

        //create constructor to get the values 
        public ProductDetails(string productName,double price,int stock,int shippingDuration)
        {
            s_produtID++;
            ProductID="PID"+s_produtID;
            ProductName=productName;
            Price=price;
            Stock=stock;
            ShippingDuration=shippingDuration;    
        }

        public ProductDetails(string product)
        {
            string[] array=product.Split(",");
            s_produtID=int.Parse(array[0].Remove(0,3));
            ProductID=array[0];
            ProductName=array[1];
            Price=double.Parse(array[2]);
            Stock=int.Parse(array[3]);
            ShippingDuration=int.Parse(array[4]);
        }
        
    }
}