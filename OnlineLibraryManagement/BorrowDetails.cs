using System;

public enum Status{Default,Borrowed,Returned}


namespace OnlineLibraryManagement
{
    public class BorrowDetails
    {
        private static int s_borrowID=300;

        public string BorrowID{get;}
        public string BookID{get;set;}
        public string RegistrationID{get;set;}
        public DateTime BorrowedDate{get;set;}
        public Status Status{get;set;}

        public BorrowDetails(string bookID,string registrationID,DateTime borrowedDate,Status status)
        {
            s_borrowID++;
            BorrowID="LB"+s_borrowID;
            BookID=bookID;
            RegistrationID=registrationID;
            BorrowedDate=borrowedDate;
            Status=status;
        }

    }
}