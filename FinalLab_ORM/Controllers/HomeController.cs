using Entities;
using EntityService;
using FinalLab_ORM.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalLab_ORM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            UserService us = new();
            List<User> users = us.GetUsers();
            var result = new { users };
            return Ok(result);
        }


        [HttpPost]
        public ActionResult AddAUser([FromBody] User user)
        {
            UserService us = new();
            var result = us.AddAUser(user);
            if (result)
            {
                return Ok(new { message = "Add successfully!" }); // Return a 200 OK status
            }
            return NotFound(new { message = "Add Failed!" }); // Or return another appropriate status
        }

        [HttpPost]
        public ActionResult DeleteAUser(int id)
        {
            UserService us = new();
            var result = us.DeleteAUser(id);
            if (result)
            {
                return Ok(new { message = "Delete successfully!" }); // Return a 200 OK status
            }
            return NotFound(new { message = "Delete Failed!" }); // Or return another appropriate status
        }

        [HttpPost]
        public ActionResult UpdateAUser([FromBody] User user)
        {
            UserService us = new();
            var result = us.UpdateUser(user);
            {
                return Ok(new { message = "Update successfully!" }); // Return a 200 OK status
            }
            return NotFound(new { message = "Update Failed!" });
        }
    }
}
