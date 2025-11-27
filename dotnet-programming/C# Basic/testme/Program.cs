using System;
using MySql.Data.MySqlClient;

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

    static void InsertStudent(int id, string name, string address)
    {
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            string sql = "INSERT INTO tblstudent (StudentId, name, address) VALUES (@id, @name, @address)";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Inserted Successfully.");
            }
        }
    }

    static void UpdateOperation(int id, string address)
    {
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            string sql = "UPDATE tblstudent SET address = @address WHERE StudentId = @id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int row = cmd.ExecuteNonQuery();
                Console.WriteLine(row + " row(s) updated.");
            }
        }
    }

    static void DeleteStudent(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            string sql = "DELETE FROM tblstudent WHERE StudentId = @id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Deleted Successfully.");
            }
        }
    }

    static void ReadOperation()
    {
        using (MySqlConnection conn = new MySqlConnection(connection))
        {
            string sql = "SELECT * FROM tblstudent";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader data = cmd.ExecuteReader())
                {
                    Console.WriteLine("\n--- STUDENTS ---");
                    while (data.Read())
                    {
                        Console.WriteLine(data["StudentId"] + " " + data["name"] + " " + data["address"]);
                    }
                }
            }
        }
    }
}
