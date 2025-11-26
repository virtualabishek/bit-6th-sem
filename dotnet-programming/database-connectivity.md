# Unit 5 – Working with Database (ADO.NET + EF Core)  
(Exam-focused notes + patterns + practice questions with solutions)

Main idea: for exam, you must be able to write **small C# console (or simple app) programs** that perform **CRUD** (Create, Read, Update, Delete) using:

- **ADO.NET** (low‑level: `SqlConnection`, `SqlCommand`, `SqlDataReader`, `SqlDataAdapter`)
- **Entity Framework Core** (high‑level ORM: `DbContext`, `DbSet<T>`, LINQ)

Below are concise notes, then complete solutions for your given past questions, and finally 6 extra likely questions with full solutions.

***

## 1. Core Theory – What examiner expects

### ADO.NET basics

Important classes (remember these words for theory):

- **Connection**: e.g. `SqlConnection` – opens connection to database.
- **Command**: e.g. `SqlCommand` – holds SQL like `INSERT`, `UPDATE`, `DELETE`, `SELECT`.
- **Reader**: `SqlDataReader` – reads query results row by row (forward-only).
- **Adapter**: `SqlDataAdapter` – fills `DataSet` / `DataTable` (disconnected model).

Typical pattern (very exam-usable template):

```csharp
using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Data Source=.;Initial Catalog=YourDB;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sql = "...";   // your SQL here
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                // add parameters if needed
                int rows = cmd.ExecuteNonQuery();   // for INSERT/UPDATE/DELETE
            }
        }
    }
}
```

### EF Core basics

Key ideas:

- **ORM (Object‑Relational Mapper)**: Maps classes to tables, properties to columns.
- You define:
  - **Model class**: e.g. `public class Book { … }`
  - **DbContext**: e.g. `public class LibraryContext : DbContext { DbSet<Book> Books {get;set;} }`
- Use **LINQ** for queries and updates (`Where`, `FirstOrDefault`, `Add`, `Remove`, etc.).
- Call `SaveChanges()` to persist changes.

Minimal pattern:

```csharp
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Author { get; set; }
    public DateTime Pub_Date { get; set; }
}

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Library;Integrated Security=True");
    }
}
```

***

## 2. SOLUTIONS TO YOUR GIVEN PAST QUESTIONS

### Q1. ADO.NET – Update price of the book 1000 whose author is Jhon

> Given a table Book (ISBN, Name, Author, Price), write a program to update the price of the book 1000 whose author is Jhon. [5 marks]

```csharp
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Data Source=.;Initial Catalog=YourDB;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sql = "UPDATE Book SET Price = @price " +
                         "WHERE ISBN = @isbn AND Author = @author";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@price", 1000);
                cmd.Parameters.AddWithValue("@isbn", 1000);       // book 1000
                cmd.Parameters.AddWithValue("@author", "Jhon");

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows + " row(s) updated.");
            }
        }
    }
}
```

Key points to mention if they ask theory:

- Uses **`SqlConnection`** to connect.
- Uses **parameterized query** to prevent SQL injection.
- **`ExecuteNonQuery()`** for UPDATE.

***

### Q2. ADO.NET – Delete books having price less than 500

> Using ADO.NET, in a relation BOOK (id, author, name, price), delete the books having price less than 500. [8 marks]

```csharp
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Data Source=.;Initial Catalog=YourDB;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sql = "DELETE FROM BOOK WHERE price < @maxPrice";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@maxPrice", 500);

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows + " book(s) deleted.");
            }
        }
    }
}
```

You can add comments in exam for clarity.

***

### Q3. ADO.NET – Work with `studentDB` and `tblstudent`

> Assume a database named studentDB and a table inside it named tblstudent {StudentId (primary key), name, address} [6 marks]

Exam may ask: *insert*, *update*, *delete*, or *display*. Prepare an insert + select example.

#### Insert and display all students:

```csharp
using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Data Source=.;Initial Catalog=studentDB;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            // 1. INSERT a student
            string insertSql = "INSERT INTO tblstudent (StudentId, name, address) " +
                               "VALUES (@id, @name, @addr)";
            using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
            {
                insertCmd.Parameters.AddWithValue("@id", 1);
                insertCmd.Parameters.AddWithValue("@name", "Ram");
                insertCmd.Parameters.AddWithValue("@addr", "Kathmandu");
                insertCmd.ExecuteNonQuery();
            }

            // 2. SELECT all students using DataReader
            string selectSql = "SELECT StudentId, name, address FROM tblstudent";
            using (SqlCommand selectCmd = new SqlCommand(selectSql, conn))
            using (SqlDataReader reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string addr = reader.GetString(2);
                    Console.WriteLine($"{id} - {name} - {addr}");
                }
            }
        }
    }
}
```

Here you show **Connection**, **Command**, **Reader** from the syllabus.

***

### Q4. EF Core – Insert 10 books and retrieve books published in 2022

> Assume a database named “Library” with relation Book(Id, Name, Price, Author, Pub_Date). Write a program to insert the records of 10 books and retrieve those books which are published in 2022 using Entity Framework. [10 Marks]

#### Step 1: Model + DbContext

```csharp
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

public class Book
{
    public int Id { get; set; }      // primary key
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Author { get; set; }
    public DateTime Pub_Date { get; set; }
}

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source=.;Initial Catalog=Library;Integrated Security=True");
    }
}
```

#### Step 2: Insert 10 books and query 2022

```csharp
class Program
{
    static void Main()
    {
        using (var context = new LibraryContext())
        {
            // 1. Insert 10 books
            var books = new List<Book>
            {
                new Book { Name = "Book1", Price = 500, Author = "Author1",
                           Pub_Date = new DateTime(2022, 1, 10) },
                new Book { Name = "Book2", Price = 600, Author = "Author2",
                           Pub_Date = new DateTime(2021, 5, 20) },
                new Book { Name = "Book3", Price = 700, Author = "Author3",
                           Pub_Date = new DateTime(2022, 3, 15) },
                // ...similarly add until 10 records
            };

            context.Books.AddRange(books);
            context.SaveChanges();

            // 2. Retrieve books published in 2022
            var books2022 = context.Books
                                   .Where(b => b.Pub_Date.Year == 2022)
                                   .ToList();

            foreach (var b in books2022)
            {
                Console.WriteLine($"{b.Id} - {b.Name} - {b.Pub_Date.ToShortDateString()}");
            }
        }
    }
}
```

In exam, you can shorten the 10‑book list (show 3–4, write comment “// similarly add others”).

***

## 3. HOW TO STUDY AND IMPLEMENT FOR EXAM

1. **Memorize two templates**  
   - One **ADO.NET template** (connect → command → execute).  
   - One **EF Core template** (context + model + LINQ + SaveChanges).

2. **Translate any question into CRUD operation**  
   - “update price” → UPDATE / EF property assignment + `SaveChanges()`
   - “delete books with price < 500” → DELETE / `RemoveRange(...)`
   - “insert 10 records” → multiple `INSERT` or `AddRange(...)`
   - “retrieve books from 2022” → SELECT with condition / LINQ `Where`.

3. **Write comments in exam** to show understanding even if code not perfect.

4. **Practice** writing:
   - 2 programs using ADO.NET (one UPDATE, one DELETE).
   - 2–3 programs using EF Core (insert + query, update, delete).

5. When short theory is asked:
   - Define: **Connection**, **Command**, **Reader**, **Adapter**, **ORM**, **DbContext**, **DbSet**, **LINQ** in 1–2 lines.

***

## 4. EXTRA 6 LIKELY QUESTIONS WITH SOLUTIONS

### Q5. Using ADO.NET, display all books whose price is greater than 1000. [5 marks]

```csharp
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Data Source=.;Initial Catalog=Library;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sql = "SELECT Id, Name, Price, Author FROM Book WHERE Price > @minPrice";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@minPrice", 1000);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        string author = reader.GetString(3);

                        Console.WriteLine($"{id} - {name} - {price} - {author}");
                    }
                }
            }
        }
    }
}
```

***

### Q6. Using ADO.NET DataAdapter, fill a DataTable with all students and display them. [6 marks]

```csharp
using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Data Source=.;Initial Catalog=studentDB;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string sql = "SELECT StudentId, name, address FROM tblstudent";

            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            adapter.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["StudentId"]} - {row["name"]} - {row["address"]}");
            }
        }
    }
}
```

Shows **Adapter** and **disconnected model**.

***

### Q7. Using EF Core, update the address of a student with given StudentId. [6 marks]

```csharp
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}

public class StudentContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Data Source=.;Initial Catalog=studentDB;Integrated Security=True");
    }
}

class Program
{
    static void Main()
    {
        using (var context = new StudentContext())
        {
            int idToUpdate = 1;
            var student = context.Students.FirstOrDefault(s => s.StudentId == idToUpdate);

            if (student != null)
            {
                student.Address = "Pokhara";
                context.SaveChanges();
                Console.WriteLine("Address updated.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
        }
    }
}
```

***

### Q8. Using EF Core, delete all books authored by “Jhon”. [5 marks]

```csharp
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Author { get; set; }
}

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Data Source=.;Initial Catalog=Library;Integrated Security=True");
    }
}

class Program
{
    static void Main()
    {
        using (var context = new LibraryContext())
        {
            var booksByJhon = context.Books
                                     .Where(b => b.Author == "Jhon")
                                     .ToList();

            context.Books.RemoveRange(booksByJhon);
            context.SaveChanges();

            Console.WriteLine("Books by Jhon deleted.");
        }
    }
}
```

***

### Q9. Using ADO.NET, insert a new book record into BOOK table. [4 marks]

```csharp
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connStr = "Data Source=.;Initial Catalog=Library;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            string sql = "INSERT INTO Book (Id, Name, Price, Author) " +
                         "VALUES (@id, @name, @price, @auth)";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", 101);
                cmd.Parameters.AddWithValue("@name", "Net Centric Book");
                cmd.Parameters.AddWithValue("@price", 800);
                cmd.Parameters.AddWithValue("@auth", "CSIT Author");

                cmd.ExecuteNonQuery();
                Console.WriteLine("Book inserted.");
            }
        }
    }
}
```

***

### Q10. Using EF Core, list all students whose address is “Kathmandu”. [5 marks]

```csharp
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}

public class StudentContext : DbContext
{
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Data Source=.;Initial Catalog=studentDB;Integrated Security=True");
    }
}

class Program
{
    static void Main()
    {
        using (var ctx = new StudentContext())
        {
            var list = ctx.Students
                          .Where(s => s.Address == "Kathmandu")
                          .ToList();

            foreach (var s in list)
            {
                Console.WriteLine($"{s.StudentId} - {s.Name} - {s.Address}");
            }
        }
    }
}
```

***

If you want, next step can be:  
- a short “formula sheet” summarizing the core syntax patterns (ADO.NET CRUD, EF Core CRUD) in one page you can memorize quickly, and  
- a few mini‑MCQs/theory questions (definitions of Connection, Command, Reader, ORM, DbContext, DbSet, etc.) to test yourself.
