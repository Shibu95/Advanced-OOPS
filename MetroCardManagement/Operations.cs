using System;
using System.Collections.Generic;

namespace MetroCardManagement;
public class Operations
{
    public static CustomList<UserDetails> userDetailsList = new CustomList<UserDetails>();
    public static CustomList<TravelDetails> travelList = new CustomList<TravelDetails>();
    public static CustomList<TicketFareDetails> ticketFareList = new CustomList<TicketFareDetails>();


    //create object for store details of login user
    public static UserDetails currentLogedInUser;

    public static void MainMenu()
    {

        string choice = "yes";


        System.Console.WriteLine("---MetroCard Management---");

        do
        {
            System.Console.WriteLine("Please select an option \n1.New User Registration \n2.Login User \n3.Exit");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    {
                        Registration();
                        break;
                    }
                case 2:
                    {
                        LogIn();
                        break;
                    }
                case 3:
                    {
                        choice = "no";
                        break;
                    }
            }


        }
        while (choice == "yes");

    }
    public static void Registration()
    {
        System.Console.WriteLine("Enter Your Name");
        string userName = Console.ReadLine();
        System.Console.WriteLine("Enter Your PhoneNumber");
        long phoneNumber = long.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter Amount to Recharge");
        double balance = double.Parse(Console.ReadLine());

        UserDetails user = new UserDetails(userName, phoneNumber, balance);
        userDetailsList.Add(user);

        System.Console.WriteLine("You have successfully registered");
        System.Console.WriteLine("Your UserID is {0} ", user.UserID);

    }

    public static void LogIn()
    {
        //User validation

        //Get the userID from the user
        System.Console.WriteLine("Enter Your UserID");
        string userID = Console.ReadLine().ToUpper();

        bool flag = false;


        //check whether userID id present in userDetails or not
        foreach (UserDetails user in userDetailsList)
        {
            //if presented display the submenu
            if (userID == user.UserID)
            {
                System.Console.WriteLine("Login Successful");
                //to store current login user details
                currentLogedInUser = user;
                ShowSubMenu();
                flag = true;
            }
        }
        //if not presented show "invalid User ID"
        if (flag)
        {
            System.Console.WriteLine("The UserID you entered is not a valid one");
        }
    }

    public static void ShowSubMenu()
    {
        string choice1 = "yes";

        do
        {
            //Show submenu
            System.Console.WriteLine("Please select an option  \na.Balance Check \nb.Recharge \nc.View Travel History \nd.Travel \ne.Exit");
            //Get input from user
            char choice = char.Parse(Console.ReadLine().ToLower());
            //Using switchcase to execute the user choice
            switch (choice)
            {
                case 'a':
                    {
                        //Check balance amount
                        BalanceCheck();
                        break;
                    }
                case 'b':
                    {
                        //To recharge user Card
                        Recharge();
                        break;
                    }
                case 'c':
                    {
                        //show full travel history
                        TravelHistory();
                        break;
                    }
                case 'd':
                    {
                        //to enter the travel places
                        Travel();
                        break;
                    }
                case 'e':
                    {
                        //Exit from submenu to menu
                        choice1 = "no";
                        break;
                    }
            }
            //repeat the process until find the login id otherwise exit from submenu

        }
        while (choice1 == "yes");

    }

    //to check the balance using BalanceCheck method
    public static void BalanceCheck()
    {
        System.Console.WriteLine("Your balance is : " + currentLogedInUser.Balance);
    }
    //to recharge the card
    public static void Recharge()
    {
        //get the input from user
        System.Console.WriteLine("Enter the amount for recharge");
        double rechargeAmount = double.Parse(Console.ReadLine());

        //to check current balance after recharge
        System.Console.WriteLine("Your updated balance is : " + (currentLogedInUser.Balance));

    }
    //To view the travel history
    public static void TravelHistory()
    {
        bool flag = true;
        System.Console.WriteLine("---Travel History Details---");
        foreach (TravelDetails travel in travelList)
        {
            if (currentLogedInUser.UserID == travel.UserID)
            {
                System.Console.WriteLine($"{travel.TravelID} {travel.UserID} {travel.TicketID} {travel.TravelDate.ToShortDateString()} {travel.TravelCost}");
                flag = false;
            }
        }
        if (flag)
        {
            System.Console.WriteLine("You don't have travel history yet");
        }
    }
    //enter journey details and check the fair details
    public static void Travel()
    {
        System.Console.WriteLine("---Show route Details---");
        foreach (TicketFareDetails ticketFare in ticketFareList)
        {
            System.Console.WriteLine("{0} {1} {2} {3}", ticketFare.TicketID, ticketFare.FromLocation, ticketFare.ToLocation, ticketFare.TicketPrice);
        }

        System.Console.WriteLine("Choose Route ID");
        string routeID = Console.ReadLine().ToUpper();

        bool flag = true;
        //check ticket id is valid or not
        foreach (TicketFareDetails ticketFare in ticketFareList)
        {

            if (routeID == ticketFare.TicketID)
            {
                flag = false;
                //If ID valid check user balance 
                if (currentLogedInUser.Balance >= ticketFare.TicketPrice)
                {
                    currentLogedInUser.DeductBalance(ticketFare.TicketPrice);
                    TravelDetails travel = new TravelDetails(currentLogedInUser.UserID, ticketFare.TicketID, DateTime.Now, ticketFare.TicketPrice);
                    travelList.Add(travel);
                    System.Console.WriteLine("Your Travel ID is : " + travel.TravelID);
                }
                //
                else
                {
                    System.Console.WriteLine("Insufficient Balance");
                }
            }
        }
        //ID is not in list then show invalid message
        if (flag)
        {
            System.Console.WriteLine("Invalid User Id");
        }




    }

    public static void LoadDefaultData()
    {
        ticketFareList.Add(new TicketFareDetails("Airport", "Egmore", 55));
        ticketFareList.Add(new TicketFareDetails("Airport", "Koyambedu", 25));
        ticketFareList.Add(new TicketFareDetails("Alandur", "Koyambedu", 25));
        ticketFareList.Add(new TicketFareDetails("Koyambedu", "Egmore", 32));
        ticketFareList.Add(new TicketFareDetails("Vadapalani", "Egmore", 45));
        ticketFareList.Add(new TicketFareDetails("Arumbakkam", "Egmore", 25));
        ticketFareList.Add(new TicketFareDetails("Vadapalani", "Koyambedu", 25));
        ticketFareList.Add(new TicketFareDetails("Arumbakkam", "Koyambedu", 16));

        travelList.Add(new TravelDetails("CMRL1001", "MR101", new DateTime(2022, 10, 10), 55));
        travelList.Add(new TravelDetails("CMRL1001", "MR104", new DateTime(2022, 10, 10), 32));
        travelList.Add(new TravelDetails("CMRL1002", "MR103", new DateTime(2021, 10, 11), 25));
        travelList.Add(new TravelDetails("CMRL1002", "MR107", new DateTime(2021, 10, 11), 25));

        userDetailsList.Add(new UserDetails("Ravi", 98488, 1000));
        userDetailsList.Add(new UserDetails("Baskaran", 99488, 1000));
    }
}