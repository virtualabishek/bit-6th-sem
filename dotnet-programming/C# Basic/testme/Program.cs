using System;
using System.MySql.Data.SqlClient;
// Assume a database named studentDB and a atble inside it names 
// tblstudent {StudentId (primary key), name, address}. 
// Write ADO.net code for CRUD operation.
class Program 
{
  static string connection = "Server=localhost;Port=3306;Database=exam;Uid=root;Password=imp2083";
  static void Main() 
  {
    InsertStudent(1, "Abishek", "Bharatpur");
    InsertStudent(2, "Abinash", "Lamjung");
    ReadOperation();
    UpdateOperation(1, "Gorkha");
    ReadOperation();
    DeleteStudent(2);
    ReadOperation();
    InsertStudent(3, "Mommmmm", "Heart");
    ReadOperation();

  }
  // CREATE operation
  public void InsertStudent (int id, string name, string address) 
  {
    using MySqlConnection conn = new MySqlConnection(connection) 
    {
      string sql = "INSERT INTO tblstudent (StudentId, name, address) VALUES (@id, @name, @address)";
      using MySqlCommand cmd = new MySqlCommand(conn, sql) 
      {
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("name", name);
        cmd.Parameters.AddWithValue("address", address);
        conn.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Executed Successfully.");
      }
    }
  }

  // Update operation
  public void UpdateOperation (int id, string address) 
  {
    using MySqlConnection conn = new MySqlConnection(connection) 
    {
      string sql = "UPDATE tblstudent SET address = @address WHERE studentId = @id";
      using MySqlCommand cmd = new MySqlCommand(sql, conn) 
      {
        cmd.Parameters.AddWithValue("@address", address);
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        string row = cmd.ExecuteNonQuery();
        Console.WriteLine(row + "Updated. Done Update operation");
      }
    }
  }

  // DELETE operation
  public void DeleteStudent (int id) 
  {
    using MySqlConnection conn = new MySqlConnection(connection) 
    {
      string sql = "DELETE FROM tblstudent WHERE studentId = @id";
      using MySqlCommand cmd = new MySqlCommand(sql, conn) 
      {
        cmd.Parameters.AddWithValue("@id", id);
        conn.Open();
        cmd.ExecuteNonQuery();
        Console.WriteLine("Deleted the student Successfully.");
      }
    }
  }

  // Read Operation.
  public void ReadOperation() 
  {
  using MySqlConnection conn = new MySqlConnection(connection) 
  {
    string sql = "SELECT * FROM tblstudent";
    using MySqlCommand cmd = new MySqlCommand(sql, conn) 
    {
      conn.Open();
      MySqlDataReader data = cmd.ExecuteReader();
      Console.WriteLine(data);
      while(data.Read()) {
        Console.WriteLine(data["studentId"] + data["name"] + data["address"]);
      }
    }
  }
  }
}
