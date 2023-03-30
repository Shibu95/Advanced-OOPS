using System;
using System.Collections.Generic;
namespace OnlineLibraryManagement;
class program
{
    //Static list declaration
    static List<UserDetails> userList = new List<UserDetails>();

    static List<BookDetails> bookList = new List<BookDetails>();

    static List<BorrowDetails> borrowedList = new List<BorrowDetails>();

    //static user object
    static UserDetails  currentLoggedInUser;

    public static void Main(string[] args)
    {
        GetDefaultData();
        int mainOption = 0;
        string mainChoice = "YES";
        do    // do while loop 
        {
            System.Console.WriteLine("********ONLINE LIBRARY MANAGEMENT SYSTEM*********");
            System.Console.WriteLine("Enter a option from Main Menu\n1.User Registration\n2.User Login\n3.Exit"); //getting inputs
            System.Console.WriteLine("-------------------------------------------------------------------------");
            mainOption = int.Parse(Console.ReadLine());
            switch(mainOption)          //Inititating switchcase
            {
                case 1:
                {
                    Registration();
                    break;
                }
                case 2:
                {
                    Login();
                    break;
                }
                case 3:
                {
                    System.Console.WriteLine("Application Exited Successfully");
                    mainChoice = "NO";
                    break;
                }
            }
        }while(mainChoice == "YES");
    }//main end

    static void Registration() //registration method
    { //get inputs --- create object -- - store in list --- display registartion id
        System.Console.WriteLine("Enter your name");
        string userName = Console.ReadLine();
        System.Console.WriteLine("Enter your Gender");
        Gender userGender = Enum.Parse<Gender>(Console.ReadLine(),true);
        System.Console.WriteLine("Enter your Department");
        Department userDepartment = Enum.Parse<Department>(Console.ReadLine(),true);
        System.Console.WriteLine("Enter your Phone Number");
        long phoneNumber = long.Parse(Console.ReadLine());
        System.Console.WriteLine("Enter you Email ID");
        string mailID = Console.ReadLine();

        UserDetails user = new UserDetails(userName,userGender,userDepartment,phoneNumber,mailID);
        userList.Add(user);
        System.Console.WriteLine("-------------------------------------------------------------------------");
        System.Console.WriteLine($"Registartion is successful. Your Registration ID - {user.RegistrationID}");
        System.Console.WriteLine("-------------------------------------------------------------------------");
    }//registartion method ends

    static void Login() //login method 
    { //get id --  check if its present -- then submenu --- else invalid id
        System.Console.WriteLine("Enter you Registration ID to proceed");
        string passID = Console.ReadLine().ToUpper();
        bool flag = true;
        foreach(UserDetails user in userList)
        {
            if(passID == user.RegistrationID)
            {
                System.Console.WriteLine("Login Successful");
                flag = false;
                currentLoggedInUser = user;
                ShowSubMenu();
            }
        }
        if(flag)
        {
            System.Console.WriteLine("Entered Id is Invalid");
        }
    }//login method end

    static void ShowSubMenu()
    {
        string subChoice = "YES";
        int subOption = 0;
        do
        {
            System.Console.WriteLine("Select the option from the Sub Menu\n1.Borrow Books\n2.Show Borrowed History\n3.Return Books\n4.Exit");
            subOption = int.Parse(Console.ReadLine());
            switch(subOption)
            {
                case 1:
                {
                    Borrow();
                    break;
                }
                case 2:
                {
                    ShowBorrowedBook();
                    break;
                }
                case 3:
                {
                    Return();
                    break;
                }
                case 4:
                {
                    System.Console.WriteLine("Going back to Main Menu");
                    subChoice = "NO";
                    break;
                }
            }
        }while(subChoice == "YES");
    }//submenu method ends
    
    static void ShowBorrowedBook()
    {
        foreach(BorrowDetails borrow in borrowedList)
        {
            if(currentLoggedInUser.RegistrationID == borrow.RegistrationID)
            {
                System.Console.WriteLine($"{borrow.BorrowID}    {borrow.BookID}     {borrow.RegistrationID}     {borrow.BorrowedDate}      {borrow.Status}");
            }
        }
    }//showborrowedbook method ends


    static void Return()        //displaying borrowed book list with return date and fine amount if its exceeds 15
    {
        System.Console.WriteLine("******Borrowed Book Details******");
        System.Console.WriteLine("------------------------------------------------");
        foreach(BorrowDetails borrow in borrowedList)
        {
            if(currentLoggedInUser.RegistrationID == borrow.RegistrationID && borrow.Status == Status.Borrowed)
            {
                DateTime returnDate = DateTime.Now;
                TimeSpan fineAmount = returnDate - borrow.BorrowedDate.AddDays(15);
                var fine = fineAmount * 1;
                System.Console.WriteLine($"{borrow.BorrowID}    {borrow.BookID}    {borrow.RegistrationID}    {borrow.BorrowedDate}     {borrow.Status}     return Date:{returnDate}   Fine:{fine.ToString("dd")}");
            }
        }
        System.Console.WriteLine("Enter the Borrowed ID to return the book");
        string returnBook = Console.ReadLine().ToUpper();
        foreach(BorrowDetails borrow1 in borrowedList)
        {
            if(returnBook == borrow1.BorrowID)
            {
                borrow1.Status = Status.Returned; 
                System.Console.WriteLine("Book Returned Successfully");
            }
        }
    }//return method ends

    static void Borrow()        //displaying book list
    {
        System.Console.WriteLine("***********BOOK AVAILABILITY DETAILS************");
        System.Console.WriteLine("------------------------------------------------");
        foreach(BookDetails book in bookList)
        {
            System.Console.WriteLine($"{book.BookID}    {book.BookName}    {book.AuthorName}    {book.BookCount}");
        }
        System.Console.WriteLine("------------------------------------------------");
        System.Console.WriteLine("Enter the Book ID to borrow");
        string bookBorrow = Console.ReadLine().ToUpper();           //asking inputs
        bool flag = true;
        foreach(BookDetails book in bookList)
        {
            if(bookBorrow == book.BookID)           //checking book is present
            {
                flag = false;
                if(book.BookCount > 0)          //checking book availability
                {
                    int count = UserEligibility();          //checking user borrowed morethan three books
                    if(count < 3)
                    {
                        book.BookCount--;
                        BorrowDetails borrow = new BorrowDetails(book.BookID,currentLoggedInUser.RegistrationID,DateTime.Now,Status.Borrowed);
                        borrowedList.Add(borrow);
                        System.Console.WriteLine($"Book borrowed successfully. You Book borrow ID: {borrow.BorrowID}");
                    }
                    else
                    {
                        System.Console.WriteLine("Since you have borrowed 3 books you cannot borrow anymore");
                    }
                }
                else
                {
                    System.Console.WriteLine("selected books are not available");  //if book not available - availability estimation date
                    BooKAvailable(book.BookID);
                }
            }
        }
        if(flag)
        {
            System.Console.WriteLine("Entered Book ID is Invalid");
        }
    }//borrow method end

    static void BooKAvailable(string bookID) // sub method of borrow method
    {
        DateTime newDate = new DateTime();

        foreach(BorrowDetails borrow in borrowedList)
        {
            if(bookID ==  borrow.BookID)
            {
                newDate = borrow.BorrowedDate.AddDays(15);      //book availability date
            }
        }
        System.Console.WriteLine($"The book will be available on {newDate}");
    }

    static int UserEligibility() // submethod of borrow method
    {
        int count = 0;
        foreach(BorrowDetails borrow in  borrowedList)
        {
            if(currentLoggedInUser.RegistrationID == borrow.RegistrationID && borrow.Status == Status.Borrowed)   // checking user borrowed more than 3 books
            {
                count = count + 1;
            }
        }
        return count;
    }

    static void GetDefaultData()
    {
        //adding default user in user list
        UserDetails user1 = new UserDetails("Ravichandran",Gender.Male,Department.EEE,9938388333,"ravi@gmail.com");
        userList.Add(user1);
        UserDetails user2 = new UserDetails("Priyadharshini",Gender.Female,Department.CSE,9944444455,"priya@gmail.com");
        userList.Add(user2);

        //adding default book in book list
        BookDetails book1 = new BookDetails("c#","Author1",3);
        bookList.Add(book1);
        BookDetails book2 = new BookDetails("HTML","Author2",5);
        bookList.Add(book2);
        BookDetails book3 = new BookDetails("CSS","Author1",3);
        bookList.Add(book3);
        BookDetails book4 = new BookDetails("JS","Author1",3);
        bookList.Add(book4);
        BookDetails book5 = new BookDetails("TS","Author2",2);
        bookList.Add(book5);

        //adding deafult borrowed details in list
        BorrowDetails borrow1 = new BorrowDetails(bookList[0].BookID,userList[0].RegistrationID,new DateTime(2022,04,10),Status.Borrowed);
        borrowedList.Add(borrow1);
        BorrowDetails borrow2 = new BorrowDetails(bookList[2].BookID,userList[0].RegistrationID,new DateTime(2022,04,12),Status.Borrowed);
        borrowedList.Add(borrow2);
        BorrowDetails borrow3 = new BorrowDetails(bookList[3].BookID,userList[0].RegistrationID,new DateTime(2022,04,15),Status.Returned);
        borrowedList.Add(borrow3);
        BorrowDetails borrow4 = new BorrowDetails(bookList[1].BookID,userList[1].RegistrationID,new DateTime(2022,04,11),Status.Borrowed);
        borrowedList.Add(borrow4);
        BorrowDetails borrow5 = new BorrowDetails(bookList[4].BookID,userList[1].RegistrationID,new DateTime(2022,04,15),Status.Returned);
        borrowedList.Add(borrow5);
    }//default data end
}
