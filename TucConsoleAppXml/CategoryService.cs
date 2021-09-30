using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace TucConsoleAppXml
{
    public class CategoryService
    {
        private readonly SqlConnection _connection;

        public CategoryService(SqlConnection connection)
        {
            _connection = connection;
        }

        public Category FindByName(string genre)
        {
            return _connection.QueryFirstOrDefault<Category>("select * from category where name=@namn",new
            {
                namn = genre
            });
        }

        public Category CreateNew(string genre)
        {
            int newId =_connection.Query<int>("insert into category(name) values(@namn);SELECT CAST(SCOPE_IDENTITY() as int)", new
            {
                namn = genre
            }).First();

            return _connection.QueryFirstOrDefault<Category>("select * from category where id=@id", new
            {
                id = newId
            });
        }
    }
}