using System.Reflection.PortableExecutable;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MetroCardManagement;

public class FileHandling
{
    public static void Create()

    {
        if (!Directory.Exists("Metro"))
        {
            System.Console.WriteLine("Creating Folder");
            Directory.CreateDirectory("Metro");
        }
        else
        {
            System.Console.WriteLine("Directory Exist");
        }

        if (!File.Exists("Metro/UserDetails.csv"))
        {
            System.Console.WriteLine("Creating File");
            File.Create("Metro/UserDetails.csv").Close();
        }
        else
        {
            System.Console.WriteLine("File exists");
        }

        if (!File.Exists("Metro/TravelDetails.csv"))
        {
            System.Console.WriteLine("Creating File");
            File.Create("Metro/TravelDetails.csv").Close();
        }
        else
        {
            System.Console.WriteLine("File exists");
        }

        if (!File.Exists("Metro/TicketFareDetails.csv"))
        {
            System.Console.WriteLine("Creating File");
            File.Create("Metro/TicketFareDetails.csv").Close();
        }
        else
        {
            System.Console.WriteLine("File exists");
        }
    }
    public static void WriteToCSV()
    {
        string[] userDetails = new string[Operations.userDetailsList.Count];
        for (int i = 0; i < Operations.userDetailsList.Count; i++)
        {
            userDetails[i] = Operations.userDetailsList[i].UserID + "," + Operations.userDetailsList[i].UserName + "," + Operations.userDetailsList[i].PhoneNumber + "," + Operations.userDetailsList[i].Balance;
        }
        File.WriteAllLines("Metro/UserDetails.csv", userDetails);

        string[] travelDetails = new string[Operations.travelList.Count];
        for (int i = 0; i < Operations.travelList.Count; i++)
        {
            travelDetails[i] = Operations.travelList[i].TravelID + "," + Operations.travelList[i].UserID + "," + Operations.travelList[i].TicketID + "," + Operations.travelList[i].TravelDate.ToString("dd/MM/yyyy") + "," + Operations.travelList[i].TravelCost;
        }
        File.WriteAllLines("Metro/TravelDetails.csv", travelDetails);


        string[] ticketFareDetails = new string[Operations.ticketFareList.Count];
        for (int i = 0; i < Operations.ticketFareList.Count; i++)
        {
            ticketFareDetails[i] = Operations.ticketFareList[i].TicketID + "," + Operations.ticketFareList[i].FromLocation + "," + Operations.ticketFareList[i].ToLocation + "," + Operations.ticketFareList[i].TicketPrice;
        }
        File.WriteAllLines("Metro/TicketFareDetails.csv", ticketFareDetails);
    }
    public static void ReadFromCSV()
    {
        string[] userDetails=File.ReadAllLines("Metro/UserDetails.csv");

        foreach(string user in userDetails)
        {
            Operations.userDetailsList.Add(new UserDetails(user));
        }

        string[] travelDetails=File.ReadAllLines("Metro/TravelDetails.csv");

        foreach(string travel in travelDetails)
        {
            Operations.travelList.Add(new TravelDetails(travel));
        }

        string[] ticketFareDetails=File.ReadAllLines("Metro/TicketFareDetails.csv");

        foreach(string ticketFare in ticketFareDetails)
        {
            Operations.ticketFareList.Add(new TicketFareDetails(ticketFare));
        }
    }
}
