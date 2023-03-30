using System;


namespace SynCart
{
    public class CustomerDetails
    {
        //Create field for customerID as static
        private static int s_customerID=1000;

        private double _balance;       
        //Create properties for customer details
        public string CustomerID{get;}
        public string CustomerName{get;set;}
        public string City{get;set;}
        public long MobileNumber{get;set;}
        public double WalletBalance{get{return _balance;}}
        public string EmailID{get;set;}

        //create constructor to get the values 
        public CustomerDetails(string customerName,string city,long mobileNumber,double walletBalance,string emailID)
        {
            s_customerID++;
            CustomerID="CID"+s_customerID;
            CustomerName=customerName;
            City=city;
            MobileNumber=mobileNumber;
            _balance=walletBalance;
            EmailID=emailID;
        }

        public CustomerDetails(string customer)
        {
            string[] array=customer.Split(",");
            s_customerID=int.Parse(array[0].Remove(0,3));
            CustomerID=array[0];
            CustomerName=array[1];
            City=array[2];
            MobileNumber=long.Parse(array[3]);
            _balance=Double.Parse(array[4]);
            EmailID=array[5];
        }

        public void Recharge(double rechargeAmount)
        {
            _balance+=rechargeAmount;
        } 

        public void DeductBalance(double amount)
        {

            _balance-=amount;
        }

        



    }
}