using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Abi Neupane", Course = "BIT" },
                new Student { Id = 2, Name = "Rohit Sharma", Course = "CSIT" },
                new Student {Id = 3, Name = "Haru Thapa", Course = "BIM"}
            };

            return View(students);
        }
    }
}
