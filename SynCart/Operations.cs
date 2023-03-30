using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;

namespace SynCart;
public class Operations
{

    //Create Lists for store the Order,Product and Customer details
    public static List<CustomerDetails> customersList = new List<CustomerDetails>();
    public static List<ProductDetails> productsList = new List<ProductDetails>();
    public static List<OrderDetails> ordersList = new List<OrderDetails>();

    public static CustomerDetails currentLogedInCustomer;

    public static void MainMenu()
    {
        

        System.Console.WriteLine("---SynCart---");

        string choice = "yes";

        do
        {
            System.Console.WriteLine();
            //Get the choice from the user
            System.Console.WriteLine("Please choose the option  \n1.Customer Registration \n2.Login  \n3.Exit");
            int option ;
            while(!(int.TryParse(Console.ReadLine(),out option)))
            {
                System.Console.WriteLine("Enter a valid one");
            }


            switch (option)
            {
                //Create seperate method for registration and call
                case 1:
                    {
                        CustomerRegistration();
                        break;
                    }
                //create seperate mrthod for login and call
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
        } while (choice == "yes");

    }
    //Get the customer details
    public static void CustomerRegistration()
    {
        System.Console.WriteLine("Enter Your Name");
        string customerName = Console.ReadLine();
        System.Console.WriteLine("Enter Your City");
        string city = Console.ReadLine();
        System.Console.WriteLine("Enter Your Phone Number");
        long phoneNumber ;
        //handling exception for giving values in string instead of other datatypes
        while(!(long.TryParse(Console.ReadLine(),out phoneNumber)))
        {
            System.Console.WriteLine("You are entered in wrong format. Please enter it correct way");
        }
        System.Console.WriteLine("Enter Your Email ID");
        string mailID = Console.ReadLine();
        System.Console.WriteLine("Enter Amount to Recharge");
        double walletBalance ;
        while(!(double.TryParse(Console.ReadLine(),out walletBalance)))
        {
            System.Console.WriteLine("You are entered in wrong format. Please enter it correct way");
        }

        //Create object for store customer details in customers list
        CustomerDetails customer = new CustomerDetails(customerName, city, phoneNumber, walletBalance, mailID);
        customersList.Add(customer);
        //display the customer ID
        System.Console.WriteLine("Account is created  \n Your customer ID is " + customer.CustomerID);
    }

    public static void LogIn()
    {

        //ask the user to enter the Customer ID
        System.Console.WriteLine("Please Enter Your Customer ID");
        string customerID = Console.ReadLine();


        foreach (CustomerDetails customer in customersList)
        {
            if (customerID == customer.CustomerID)
            {
                System.Console.WriteLine("Login Successful");
                currentLogedInCustomer = customer;
                //create seperate method for submenu and call
                ShowSubmenu();
            }
        }

    }

    //Create the submenu to show the customer action to customers
    public static void ShowSubmenu()
    {
        string choice = "yes";

        do
        {
            System.Console.WriteLine("Plaese choose one option  \na.Purchase \nb.Order History \nc.Cancel Order \nd.Wallet Balance \ne.Wallet Recharge \nf.Exit");
            char option ;
            while(!(char.TryParse(Console.ReadLine(),out option)))
            {
                System.Console.WriteLine("Please enter a valid option");
            }

            switch (option)
            {
                case 'a':
                    {
                        Purchase();
                        break;
                    }
                case 'b':
                    {
                        OrderHistory();
                        break;
                    }
                case 'c':
                    {
                        CancelOrder();
                        break;
                    }
                case 'd':
                    {
                        WalletBalance();
                        break;
                    }
                case 'e':
                    {
                        Recharge();
                        break;
                    }
                case 'f':
                    {
                        choice = "no";
                        break;
                    }

            }
        } while (choice == "yes");
    }
    //create other method for purchase
    public static void Purchase()
    {
        //display the Product Details
        foreach (ProductDetails products in productsList)
        {
            System.Console.WriteLine($"{products.ProductID} {products.ProductName} {products.Stock} {products.Price} {products.ShippingDuration}");
        }

        //Ask customer to choose product ID from list
        System.Console.WriteLine("Select the Product ID");
        string productID = Console.ReadLine();
        bool flag = false;
        //check whether product ID id available in list or not
        foreach (ProductDetails products in productsList)
        {
            if (productID == products.ProductID)
            {
                //if product id is available ask user to enter count
                System.Console.WriteLine("Enter the count you need");
                int count ;
                while(!(int.TryParse(Console.ReadLine(),out count)))
                {
                    System.Console.WriteLine("Please enter a valid one");
                }
                //If count is available calculate the total amount 
                if (count <= products.Stock)
                {
                    double deliveryCharge = 50;
                    double totalAmount = (count * products.Price) + deliveryCharge;
                    //check the customer having sufficient amount in wallet
                    if (totalAmount <= currentLogedInCustomer.WalletBalance)
                    {
                        currentLogedInCustomer.DeductBalance(totalAmount);
                        products.Stock -= count;

                        OrderDetails order = new OrderDetails(currentLogedInCustomer.CustomerID, products.ProductID, totalAmount, DateTime.Now, count, OrderStatus.Ordered);
                        ordersList.Add(order);
                        //displayb the order Id
                        System.Console.WriteLine("Order Placed Successfully. Order ID : " + order.OrderID);
                        //display the delivery date
                        System.Console.WriteLine($"Your order will be delivered on {order.PurchaseDate.AddDays(products.ShippingDuration)}");
                        if (currentLogedInCustomer.CustomerID == products.ProductID)
                        {
                            products.Stock -= count;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Insufficient Amount");
                    }
                }
                else
                {
                    System.Console.WriteLine("No stock");
                }
            }
        }
        if (flag)
        {
            System.Console.WriteLine("The product is not available");
        }
    }

    public static void OrderHistory()
    {
        bool flag=true;
        foreach (OrderDetails order in ordersList)
        {
            //Dispaly the logedIn user's order history
            if (currentLogedInCustomer.CustomerID == order.CustomerID)
            {
                System.Console.WriteLine($"{order.OrderID} {order.CustomerID} {order.ProductID} {order.TotalPrice} {order.PurchaseDate} {order.Quantity} {order.OrderStatus}");
                flag=false;
            }
        }
        if(flag)
        {
            System.Console.WriteLine("You have no order history");
        }
    }

    public static void CancelOrder()
    {
        //display the orders details placed by customer
        foreach (OrderDetails orders in ordersList)
        {
            if (currentLogedInCustomer.CustomerID == orders.CustomerID && orders.OrderStatus == OrderStatus.Ordered)
            {
                System.Console.WriteLine($"{orders.OrderID} {orders.CustomerID} {orders.ProductID} {orders.TotalPrice} {orders.PurchaseDate} {orders.Quantity} {orders.OrderStatus}");
                //ask customer enter the order ID to cancel order
                System.Console.WriteLine("Enter the Order ID to cancel the order");
                string orderID = Console.ReadLine();

                foreach (OrderDetails order in ordersList)
                {
                    if (orderID == order.OrderID)
                    {
                        order.OrderStatus = OrderStatus.Cancelled;
                        currentLogedInCustomer.Recharge(order.TotalPrice);
                        //if cancelled increase the stock
                        foreach (ProductDetails product in productsList)
                        {
                            if (order.ProductID == product.ProductID)
                            {
                                product.Stock += order.Quantity;
                                order.OrderStatus = OrderStatus.Cancelled;
                                System.Console.WriteLine($"Order {order.OrderID} cancelled successfully");
                            }
                        }
                    }

                }

            }

        }
    }

    //Display the customer Wallet Balance 
    public static void WalletBalance()
    {
        foreach (CustomerDetails customer in customersList)
        {
            if (currentLogedInCustomer.CustomerID == customer.CustomerID)
            {
                System.Console.WriteLine("Your Wallet balance is :" + customer.WalletBalance);
            }
        }

    }

    //Recharge the user Wallet
    public static void Recharge()
    {
        //Ask the user amount to be recharged
        System.Console.WriteLine("Enter the amount to recharge");
        double amount ;
        while(!(double.TryParse(Console.ReadLine(),out amount)))
        {
            System.Console.WriteLine("Enter a valid format");
        }

        currentLogedInCustomer.Recharge(amount);

        System.Console.WriteLine("Your current Wallet Balance is " + currentLogedInCustomer.WalletBalance);
    }

    //store default values
    public static void DefaultDatas()
    {
        productsList.Add(new ProductDetails("Mobile(Samsung)", 10000, 10, 3));
        productsList.Add(new ProductDetails("Tablet(Lenova)", 15000, 5, 2));
        productsList.Add(new ProductDetails("Camera(Sony)", 20000, 3, 4));
        productsList.Add(new ProductDetails("iPhone", 50000, 5, 6));
        productsList.Add(new ProductDetails("Laptop(Lenova I3)", 40000, 3, 3));
        productsList.Add(new ProductDetails("HeadPhone(Boat)", 1000, 5, 2));
        productsList.Add(new ProductDetails("Speakers(Boat)", 500, 4, 2));

        ordersList.Add(new OrderDetails("CID1001", "PID101", 20000, DateTime.Now, 2, OrderStatus.Ordered));
        ordersList.Add(new OrderDetails("CID1002", "PID103", 40000, DateTime.Now, 2, OrderStatus.Ordered));

        customersList.Add(new CustomerDetails("Ravi", "Chennai", 9885858588, 50000, "ravi@mail.com"));
        customersList.Add(new CustomerDetails("Baskaran", "Chennai", 9888475757, 60000, "baskaran@mail.com"));
    }
}