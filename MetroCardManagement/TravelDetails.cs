using System;


namespace MetroCardManagement
{
    public class TravelDetails
    {
        private static int s_travelID=300;

        public string TravelID{get;}
        public string UserID{get;set;}
        public string TicketID{get;set;}
        public DateTime TravelDate{get;set;}
        public double TravelCost{get;set;}


        public TravelDetails(string userID, string ticketID,DateTime date,double travelCost)
        {
            s_travelID++;
            TravelID="TID"+s_travelID;
            UserID=userID;
            TicketID=ticketID;
            TravelDate=date;
            TravelCost=travelCost;
        }

        public TravelDetails(string travel)
        {
            string[] array = travel.Split(",");
            s_travelID=int.Parse(array[0].Remove(0,3));
            TravelID=array[0];
            UserID=array[1];
            TicketID=array[2];
            TravelDate=DateTime.ParseExact(array[3],"dd/MM/yyyy",null);
            TravelCost=double.Parse(array[4]);

        }


    }
}