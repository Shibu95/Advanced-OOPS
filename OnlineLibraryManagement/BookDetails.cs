using System;


namespace OnlineLibraryManagement
{
    public class BookDetails
    {
        private static int s_bookID=100;

        public string BookID{get;}
        public string BookName{get;set;}
        public string AuthorName{get;set;}
        public int BookCount{get;set;}


        public BookDetails(string bookName,string authorName,int bookCount)
        {
            s_bookID++;
            BookID="BID"+s_bookID;
            BookName=bookName;
            AuthorName=authorName;
            BookCount=bookCount;
        }


    }
}