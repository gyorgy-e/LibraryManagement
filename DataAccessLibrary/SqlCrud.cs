using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly string _connectionString;
        private readonly SqlDataAccess _db;
        
        public SqlCrud(string connectionString)
        {
            _connectionString = connectionString;
            SqlDataAccess db = new SqlDataAccess();
            _db = db;
        }

        public List<BookModel> GetAllBooks()
        {
            string sql = "select Id, Title, Author, PublishedYear, Genre from Books;";
            return _db.LoadData<BookModel, dynamic>(sql, new { }, _connectionString);
        }

        public BookModel GetBookById(int id)
        {
            string sql = "select Id, Title, Author, PublishedYear, Genre from Books where Id = @Id;";
            BookModel output = new BookModel();

            output = _db.LoadData<BookModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault();

            if (output == null)
            {
                return null;
            }
            else
            {
                return output;
            }
        }

        public void CreateBook(BookModel book)
        {
            string sql = "insert into Books (Title, Author, PublishedYear, Genre) values (@Title, @Author, @PublishedYear, @Genre);";
            _db.SaveData(sql, new { book.Title, book.Author, book.PublishedYear, book.Genre }, _connectionString);
        }

        public void UpdateBook(string fieldToUpdate, object newValue, int id)
        {
            string sql = $"update Books set {fieldToUpdate} = @NewValue where Id = @Id;";
            _db.SaveData(sql, new { Id = id, NewValue = newValue }, _connectionString);
        }

        public void RemoveBook(int id)
        {
            string sql = "delete from Books where Id = @Id;";
            _db.SaveData(sql, new { Id = id }, _connectionString);
        }
    }
}
