namespace MyMvcApp.Models
{
    public class Student
    {
        public int id {get: set};
        public string name {get: set};
        public string address {get: set};
    }
    public class dataAccess {
        public Student getById(int id) {
            Student sm = new Student();
            sm.id = id;
            sm.name = name;
            sm.address = address;
        }
    }
}
