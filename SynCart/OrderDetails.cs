using System.Reflection.Emit;
using System;
/// <summary>
/// used to select a order status
/// </summary>
public enum OrderStatus{Default,Ordered,Cancelled}


namespace SynCart
{
    /// <summary>
    /// Class <see cref="OrderDetails" /> used to collect order details
    /// </summary>
    public class OrderDetails
    {
        //Create field for customerID as static
        /// <summary>
        /// static field used to auto increment and it uniquely identified
        /// </summary>
        private static int s_orderID=1000;

        
        //Create properties for customer details
        /// <summary>
        /// property Order ID is used to uniquely identified a OrderDetails class's object
        /// </summary>
        public string OrderID{get;}
        /// <summary>
        /// Property customer ID is used to get unique ID for the customer
        /// </summary>
        public string CustomerID{get;set;}
        /// <summary>
        /// Property product ID is used to get unique ID for the product
        /// </summary>
        public string ProductID{get;set;}
        /// <summary>
        /// property product ID is used to provide price of the product
        /// </summary>
        public double TotalPrice{get;set;}
        /// <summary>
        /// Property purchase date is used to get the product purchased date
        /// </summary>
        public DateTime PurchaseDate{get;set;}
        /// <summary>
        /// Property Quantity is used to get availability of products
        /// </summary>
        public int Quantity{get;set;}
        /// <summary>
        /// property Order status is used to get whether the product is ordered or not
        /// </summary>
        public OrderStatus OrderStatus{get;set;}

        //create constructor to get the values 
        /// <summary>
        /// Constructor of <see cref="OrderDetails" /> class used to initialize values to its properties
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="productID"></param>
        /// <param name="totalPrice"></param>
        /// <param name="purchaseDate"></param>
        /// <param name="quantity"></param>
        /// <param name="orderStatus"></param>
        public OrderDetails(string customerID,string productID,double totalPrice,DateTime purchaseDate,int quantity,OrderStatus orderStatus)
        {
            s_orderID++;
            OrderID="OID"+s_orderID;
            CustomerID=customerID;
            ProductID=productID;
            TotalPrice=totalPrice;
            PurchaseDate=purchaseDate;
            Quantity=quantity;
            OrderStatus=orderStatus;
        }

        public OrderDetails(string order)
        {
            string[] array=order.Split(",");
            s_orderID=int.Parse(array[0].Remove(0,3));
            OrderID=array[0];
            CustomerID=array[1];
            ProductID=array[2];
            TotalPrice=double.Parse(array[3]);
            PurchaseDate=DateTime.ParseExact(array[4],"dd/MM/yyyy",null);
            Quantity=int.Parse(array[5]);
            OrderStatus=Enum.Parse<OrderStatus>(array[7]);
        }
    }
}