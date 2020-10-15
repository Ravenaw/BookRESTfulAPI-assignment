using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookProject;

namespace BookWebAPI.Model
{
    public class DB_Books
    {
        public static List<Book> BookList = new List<Book>()
        {
            new Book("The man", "Zuhair", 23, "123456789123a"),
            new Book("the Super man", "Marcel", 23, "123456789123b"),
            new Book("the Women", "Alex", 23, "123456789123c"),
           
        };

        public static Book GetAllBooks(string isbn13)
        {
            Book book = BookList.FirstOrDefault(b => b.Isbn == isbn13);
            return book;
        }




    }
}
