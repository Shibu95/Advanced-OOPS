using System;


namespace MetroCardManagement
{
    public class UserDetails
    {
        private static int s_userID=1000;
        private double _balance;


        public string UserID{get;}
        public string UserName{get;set;}
        public long PhoneNumber{get;set;}
        public double Balance{get{return _balance;}}


        public UserDetails(string userName,long phoneNumber,double balance)
        {
            s_userID++;
            UserID="CMRL"+s_userID;
            UserName=userName;
            PhoneNumber=phoneNumber;
            _balance=balance;
        }

        public UserDetails(string user)
        {
            string[] array = user.Split(",");
            s_userID=int.Parse(array[0].Remove(0,4));
            UserID=array[0];
            UserName=array[1];
            PhoneNumber=long.Parse(array[2]);
            _balance=double.Parse(array[3]);

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