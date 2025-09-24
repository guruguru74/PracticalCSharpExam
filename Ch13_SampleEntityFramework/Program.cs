using Ch13_SampleEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch13_SampleEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //InsertBooks();

            //AddAuthor();

            //AddBooks();

            //UpdateBook();

            //DeleteBook();

            DisplayAllBooks();
        }

        static void InsertBooks()
        {
            using (var db = new BooksDbContext())
            {
                var book1 = new Book
                {
                    Title = "별의 계승자",
                    PublishedYear = 1977,
                    Author = new Author
                    {
                        Birthday = new DateTime(1941, 6, 27),
                        Gender = "M",
                        Name = "제임스 P. 호건",
                    }
                };
                db.Books.Add(book1);

                var book2 = new Book
                {
                    Title = "타임 머신",
                    PublishedYear = 1895,
                    Author = new Author
                    {
                        Birthday = new DateTime(1866, 9, 21),
                        Gender = "M",
                        Name = "H.G. 웰스",
                    }
                };
                db.Books.Add(book2);

                db.SaveChanges();

                Console.WriteLine($"{book1.Id}, {book2.Id}"); // 1, 2")
            }
        }

        static IEnumerable<Book> GetBooks()
        {
            using (var db = new BooksDbContext())
            {
                return db.Books
                    .Where(book => book.Author.Name.StartsWith("찰스 디킨스"))
                    .ToList();
            }
        }

        static void DisplayAllBooks()
        {
            var books = GetBooks();
            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}, {book.Title}, {book.PublishedYear}");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void AddAuthor()
        {
            using (var db = new BooksDbContext())
            {
                var author1 = new Author
                {
                    Name = "애거사 크리스티",
                    Gender = "F",
                    Birthday = new DateTime(1920, 1, 2),
                };
                db.Authors.Add(author1);

                var author2 = new Author
                {
                    Name = "찰스 디킨스",
                    Gender = "M",
                    Birthday = new DateTime(1812, 2, 7),
                };
                db.Authors.Add(author2);

                db.SaveChanges();
            }
        }

        private static void AddBooks()
        {
            using (var db = new BooksDbContext())
            {
                var auther1 = db.Authors.Single(a => a.Name == "애거사 크리스티");
                var book1 = new Book
                {
                    Title = "그리고 아무도 없었다",
                    PublishedYear = 1934,
                    Author = auther1,
                };
                db.Books.Add(book1);

                var auther2 = db.Authors.Single(a => a.Name == "찰스 디킨스");
                var book2 = new Book
                {
                    Title = "두 도시 이야기",
                    PublishedYear = 1859,
                    Author = auther2,
                };
                db.Books.Add(book2);
                db.SaveChanges();
            }
        }

        private static void UpdateBook()
        {
            using (var db = new BooksDbContext())
            {
                var book = db.Books.Single(x => x.Title == "별의 계승자");
                book.PublishedYear = 2016;
                db.SaveChanges();
            }
        }

        private static void DeleteBook()
        {
            using (var db = new BooksDbContext())
            {
                var book = db.Books.Single(x => x.Id == 1);
                if (book != null)
                {
                    db.Books.Remove(book);
                    db.SaveChanges();
                }
            }
        }
    }
}
