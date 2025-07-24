namespace MyMvcApp.Models
{
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }

    public class dataAccess
    {
        public Student getById(int id)
        {
            // Example: Replace with actual data retrieval logic
            return new Student
            {
                id = id,
                name = "Sample Name",
                address = "Sample Address"
            };
        }
    }
}