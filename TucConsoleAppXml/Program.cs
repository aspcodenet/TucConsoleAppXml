using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
using Dapper;

namespace TucConsoleAppXml
{
    class Program
    {
        //URL https://schoolbusiness.blob.core.windows.net/sharedfiles/books.xml
        private static string conn = "server=localhost;Database=bookshop;Trusted_Connection=True;MultipleActiveResultSets=true";
        static void Main(string[] args)
        {
             var client = new HttpClient();
             var s = client.GetStringAsync("https://schoolbusiness.blob.core.windows.net/sharedfiles/books.xml")
                 .Result;
             var doc = XDocument.Parse(s);

             var us = new CultureInfo("en-US");

             using (var connection = new SqlConnection(conn))
             { 
                 connection.Open();
                 var categoryService = new CategoryService(connection);
                 var bookService = new BookService(connection);

                 //Loopa igenom alla böcker i XML-filen
                 //Skapa categpry om inte finns
                 //alt Hämta category
                 //Skapa produkt om inte finns
                 //alt Hämta product 

                //Spara i databasen!

             }


            Console.WriteLine(s);
        }
    }
}
