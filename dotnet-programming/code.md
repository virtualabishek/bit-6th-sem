# Hey Abi — you glorious code-scratching dumbass 😏

Nice — you gave the syllabus and past questions. I’ll teach you *what to know*, *how to study*, and *how to implement* the kinds of answers they’ll expect in the exam — but I won’t just hand you blind copy-paste homework. I’ll give clear notes, small guided examples, and practice steps so **you** can write, run and explain the code in the viva. After you try something, paste it here and I’ll roast it and fix it.

---

# Quick plan (what I’ll give you)

1. Short, high-yield notes on ADO.NET building blocks (what they are & when to use them).
2. Short notes on EF Core basics and when to choose it vs ADO.NET.
3. Guided step-by-step approach for each past question (SQL + how to implement in C# ADO.NET / EF Core).
4. Small practice tasks + suggested answers structure (you do the code; I review).
5. Exam tips: common mistakes, what to say in viva.

---

# 1) ADO.NET cheat-sheet — the essentials (keep this in your head)

* **SqlConnection / OleDbConnection / NpgsqlConnection**

  * Represents DB connection. Always open just before use and close/dispose (`using` block).
* **SqlCommand**

  * Holds SQL text or stored procedure name. Use parameters (`@param`) to avoid SQL injection.
* **SqlDataReader**

  * Forward-only, read-only stream of rows; best for fast reads. Use `ExecuteReader()`.
* **SqlDataAdapter + DataSet/DataTable**

  * Adapter fills DataSet/DataTable; good for disconnected scenarios and updates via `Update()` with proper commands.
* **ExecuteNonQuery**

  * For INSERT/UPDATE/DELETE; returns affected rows.
* **ExecuteScalar**

  * For single value queries (COUNT, SUM).
* **Transactions**

  * Use `SqlTransaction` with `BeginTransaction()` for multiple dependent changes; `Commit()` or `Rollback()` on error.
* **Parameterization**

  * Always `command.Parameters.AddWithValue("@p", value)` or better typed Add. Prevents SQL injection and type issues.
* **Exception handling**

  * Wrap DB calls in try/catch, log exception, ensure connection disposed (use `using`).

---

# 2) EF Core essentials — why & how

* **Why EF Core**: ORM — maps tables to C# classes. Faster dev, less boilerplate, good for CRUD and complex LINQ queries.
* **When to use ADO.NET**: exam may require you to show ADO.NET knowledge; also choose it for tiny focused tasks, performance-critical code, or when you must write raw SQL.
* **Basic pieces**:

  * `DbContext` — represents session with DB.
  * `DbSet<TEntity>` — table representation.
  * Add EF Core via NuGet: `Microsoft.EntityFrameworkCore` and provider like `Microsoft.EntityFrameworkCore.SqlServer`.
  * Migrations to create DB schema: `dotnet ef migrations add Init` and `dotnet ef database update`.
  * CRUD: `context.Books.Add()`, `context.SaveChanges()`, `context.Books.Find(id)`, `context.Books.Remove()`, LINQ `Where()`, etc.

---

# 3) How to convert an exam question into an answer — a repeatable 5-step approach

1. **Read carefully** — note DB/table names and fields.
2. **Write the SQL** for the operation first (this is often enough for 50% marks).
3. **Choose ADO.NET or EF** based on the question wording (if they explicitly ask “Using ADO.NET” — use ADO.NET).
4. **Write skeleton C# code**:

   * `using (SqlConnection conn = new SqlConnection(connString)) { conn.Open(); using (SqlCommand cmd = new SqlCommand(sql, conn)) { ... } }`
5. **Mention error handling and disposal** (using blocks), and say how to test (run sample data, show affected rows).

---

# 4) Guided solutions for the **past questions** (I’ll show SQL + a **short** ADO.NET outline — then YOU implement)

### Q1

**Problem**: `Book (ISBN, Name, Author, Price)`. Update the price to 1000 for books whose author is `Jhon`.
**SQL**:

```sql
UPDATE Book
SET Price = 1000
WHERE Author = 'Jhon';
```

**How to implement (ADO.NET steps)**:

1. Use a parameterized query to be safe:

```sql
UPDATE Book SET Price = @price WHERE Author = @author;
```

2. C# skeleton (fill in values and run):

```csharp
using (var conn = new SqlConnection(connString))
{
    string sql = "UPDATE Book SET Price = @price WHERE Author = @author;";
    using (var cmd = new SqlCommand(sql, conn))
    {
        cmd.Parameters.AddWithValue("@price", 1000);
        cmd.Parameters.AddWithValue("@author", "Jhon");
        conn.Open();
        int rows = cmd.ExecuteNonQuery(); // rows affected
    }
}
```

*Exam talk:* mention `ExecuteNonQuery()` returns affected rows; show sample result: “1 row affected” or “N rows affected”.

---

### Q2

**Problem**: Using ADO.NET, relation BOOK (id, author, name, price) — delete books with price < 500.
**SQL**:

```sql
DELETE FROM BOOK WHERE price < 500;
```

**ADO.NET outline**:

```csharp
using (var conn = new SqlConnection(connString))
{
    string sql = "DELETE FROM BOOK WHERE price < @limit;";
    using (var cmd = new SqlCommand(sql, conn))
    {
        cmd.Parameters.AddWithValue("@limit", 500);
        conn.Open();
        int deleted = cmd.ExecuteNonQuery();
        // deleted contains number of rows removed
    }
}
```

*Exam tips:* Mention transaction only if multiple dependent deletes needed. For single-statement delete, `ExecuteNonQuery` is fine.

---

### Q3

**Problem**: Assume DB `studentDB` and table `tblstudent { StudentId (PK), name, address }`. (Usually they ask to create DB/table or insert/select/update).
**SQL (create table)**:

```sql
CREATE DATABASE studentDB;
GO
USE studentDB;
GO
CREATE TABLE tblstudent (
    StudentId INT PRIMARY KEY,
    name NVARCHAR(100),
    address NVARCHAR(200)
);
```

**If asked to insert**:

```sql
INSERT INTO tblstudent (StudentId, name, address)
VALUES (1, 'Abishek', 'Chitwan');
```

**ADO.NET to insert** (sketch):

```csharp
string sql = "INSERT INTO tblstudent (StudentId, name, address) VALUES (@id, @name, @addr)";
using (var cmd = new SqlCommand(sql, conn))
{
    cmd.Parameters.AddWithValue("@id", 1);
    cmd.Parameters.AddWithValue("@name", "Abishek");
    cmd.Parameters.AddWithValue("@addr", "Chitwan");
    conn.Open();
    int inserted = cmd.ExecuteNonQuery();
}
```

**If asked with EF Core**: show `Student` class, `DbContext` with `DbSet<Student> Students`, then `context.Students.Add(new Student{...}); context.SaveChanges();`

---

# 5) Mini–examples (copy, run, explain — but **you** should type them into Visual Studio / VS Code and run)

I’ll give one full runnable ADO.NET example for **deleting** (Q2 style). **Type this into a .NET Console app** and run after setting a correct `connString`. Don’t paste back until you run it — then paste output if it fails.

```csharp
using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connString = "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;";
        using (var conn = new SqlConnection(connString))
        {
            string sql = "DELETE FROM BOOK WHERE price < @limit;";
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@limit", 500);
                conn.Open();
                int deleted = cmd.ExecuteNonQuery();
                Console.WriteLine($"Rows deleted: {deleted}");
            }
        }
    }
}
```

**Homework (do this now):**

* Replace `YOUR_SERVER` and `YOUR_DB`, create table `BOOK` with a few sample rows, then run the program.
* Paste the console output here. If you get errors, paste the exception — I’ll roast and fix.

---

# 6) How to study this for an exam (fast, effective)

* **Make a one-page cheat sheet** with: `SqlConnection`, `SqlCommand`, `ExecuteNonQuery`, `ExecuteReader`, `SqlDataAdapter` and example SQL statements (UPDATE, DELETE, INSERT, SELECT). Memorize the `using` pattern.
* **Practice by hand**: write the SQL first, then write the ADO.NET skeleton — 10× for different scenarios (update by author, update by ISBN, delete by price, delete by id, insert student, select student by id).
* **Viva pitch**: when asked, say SQL sentence → code skeleton → result explanation (rows affected / reader loop) → error handling.
* **Edge cases**: always show parameterization, and briefly mention transactions and connection disposal.
* **Timebox practice**: 30 minutes reading theory + 60 minutes coding small examples + 30 minutes mock viva (explain the code aloud).

---

# 7) Quick quiz (one at a time — answer then I’ll check)

I’ll ask short practice questions and you answer **one** — then I’ll correct.

First question: Write the SQL to change the `Price` of the book having `ISBN = '978-ABC'` to `1500`. (Just SQL — one line.)

Type only the SQL. Go.
