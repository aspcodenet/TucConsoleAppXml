using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace TucConsoleAppXml
{
    public class BookService
    {
        private readonly SqlConnection _connection;

        public BookService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Book FindByExternalid(string externalId)
        {
            return _connection.QueryFirstOrDefault<Book>("select * from book where externalId=@externalId", new
            {
                externalId
            });
        }


        public void Update(Category cat, Book book)
        {
            _connection.Execute(
                "update book set category_id=@category_id,author=@author,booktitle=@booktitle,salesprice=@salesprice, published=@published, description=@description, externalid=@external_id where id=@id",
                new
                {
                    id = book.Id,
                    category_id = cat.Id,
                    author = book.Author,
                    booktitle = book.BookTitle,
                    salesprice = book.Salesprice,
                    published = book.Published,
                    description = book.Description,
                    external_id = book.Externalid


                });
        }



        public void Create(Category cat, Book book)
        {
            int newId = _connection.Query<int>("insert into book(category_id,author,booktitle,salesprice, published, description, externalid) values(@category_id,@author,@booktitle,@salesprice,@published, @description, @externalid);SELECT CAST(SCOPE_IDENTITY() as int)", new
            {
                category_id = cat.Id,
                author = book.Author,
                booktitle = book.BookTitle,
                salesprice = book.Salesprice,
                published = book.Published,
                description = book.Description,
                externalid=book.Externalid


            }).First();

        }
    }
}