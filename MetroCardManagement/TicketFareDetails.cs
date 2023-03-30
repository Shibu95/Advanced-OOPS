using System;


namespace MetroCardManagement
{
    public class TicketFareDetails
    {

        private static int s_ticketID=100;

        public string TicketID{get;}
        public string FromLocation{get;set;}
        public string ToLocation{get;set;}
        public double TicketPrice{get;set;}


        public TicketFareDetails(string fromLocation,string toLocation,double ticketPrice)
        {
            s_ticketID++;
            TicketID="MR"+s_ticketID;
            FromLocation=fromLocation;
            ToLocation=toLocation;
            TicketPrice=ticketPrice;
        }

        public TicketFareDetails(string ticketFare)
        {
            string[] array=ticketFare.Split(",");
            s_ticketID=int.Parse(array[0].Remove(0,2));
            TicketID=array[0];
            FromLocation=array[1];
            ToLocation=array[2];
            TicketPrice=double.Parse(array[3]);
        }        
    }
}