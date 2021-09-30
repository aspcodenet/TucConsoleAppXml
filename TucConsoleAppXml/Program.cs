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

                 foreach (var element in doc.XPathSelectElements("//catalog/book"))
                 {
                    string id = element.Attribute("id").Value;
                    string author = element.XPathSelectElement("author").Value;
                    string title = element.XPathSelectElement("title").Value;
                    string genre = element.XPathSelectElement("genre").Value;
                    decimal price = decimal.Parse(element.XPathSelectElement("price").Value, 
                         NumberStyles.Number,
                         us);

                    DateTime publish_date = DateTime.ParseExact(
                        element.XPathSelectElement("publish_date").Value, "yyyy-MM-dd", null);
                    string description = element.XPathSelectElement("description").Value;

                    var cat = categoryService.FindByName(genre);
                    if (cat == null)
                        cat = categoryService.CreateNew(genre);
                    var book = bookService.FindByExternalid(id);
                    if (book == null)
                        bookService.Create(cat,new Book
                        {
                            Description = description,
                            Author = author,
                            BookTitle = title,
                            Published = publish_date,
                            Externalid = id,
                            Salesprice = price
                        });
                    else
                        bookService.Update(cat, new Book {
                            Description = description,
                            Author = author,
                            BookTitle = title,
                            Published = publish_date,
                            Externalid = id,
                            Salesprice = price
                        });

                 }
                 //Loopa igenom alla böcker i XML-filen
                 //Skapa categpry om inte finns
                 //alt Hämta category
                 //Skapa produkt om inte finns
                 //alt Hämta product 

                    //Spara i databasen!

            }


        }
    }
}
