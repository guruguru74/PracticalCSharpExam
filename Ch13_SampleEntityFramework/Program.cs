using Ch13_SampleEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ch13_SampleEntityFramework
{
    internal class Program
    {
        static void Main(string[] args)
        {

            InsertBooks();

            AddAuthor();

            AddBooks();

            UpdateBook();

            DeleteBook();

            DisplayAllBooks();

            GetData();

            GetBooksAll();
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

        static void GetData()
        {
            using (var db = new BooksDbContext())
            {
                Console.WriteLine();
                // 집필한 서적이 두 권 이상인 저자를 구한다.
                var authors = db.Authors
                    .Where(author => author.Books.Count() >= 1);

                Console.WriteLine("== 집필한 서적이 두 권 이상인 저자 ==");
                foreach (var author in authors)
                    Console.WriteLine($"{author.Name}, {author.Gender}, {author.Birthday}");


                // 서적을 출판연도, 저자 이름 순서(오름차순)로 정렬해서 구한다.
                var books = db.Books
                    .OrderBy(book => book.PublishedYear)
                    .ThenBy(book => book.Author.Name);

                Console.WriteLine();
                Console.WriteLine("== 서적을 출판연도, 저자 이름 순서(오름차순)로 정렬 ==");
                foreach (var book in books)
                    Console.WriteLine($"{book.Title}, {book.PublishedYear}, {book.Author.Name}");


                // 각 발행연도에 해당하는 서적 수를 구한다.
                var groups = db.Books
                    .GroupBy(book => book.PublishedYear)
                    .Select(g => new { PublishedYear = g.Key, Count = g.Count() });

                Console.WriteLine();
                Console.WriteLine("== 각 발행연도에 해당하는 서적 수 ==");
                foreach (var group in groups)
                    Console.WriteLine($"{group.PublishedYear}, {group.Count}");


                // 집필한 서적이 가장 많은 저자 한 명을 구한다.
                var authorWithMostBooks = db.Authors.Where(author => author.Books.Count() == db.Authors.Max(a => a.Books.Count())).FirstOrDefault();

                Console.WriteLine();
                Console.WriteLine($"== 집필한 서적이 가장 많은 저자 한 명 ==");
                Console.WriteLine($"{authorWithMostBooks.Name}, {authorWithMostBooks.Books.Count()}");
            }
        }

        static void GetBooksAll()
        {
            using (var db = new BooksDbContext())
            {
                db.Database.Log = sql => Debug.WriteLine(sql);

                List<Book> lstBooks = db.Books
                    .Where(b => b.PublishedYear > 1800)
                    .ToList();

                Console.WriteLine();
                Console.WriteLine($"== GetBooksAll() ==");
                foreach (var book in lstBooks)
                {
                    Console.WriteLine($"{book.Title}, {book.Author.Name}");
                }
            }
        }
    }
}
