using System.Diagnostics;
using System.Security.Claims;
using CoreFirstProject.Database;
using CoreFirstProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreFirstProject.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly EmployeeDbContext _db;

        public HomeController(EmployeeDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var res = _db.Employees.ToList();
            return View(res);
        }

        public IActionResult Delete(int id)
        {
            var deletedata = _db.Employees.Where(a=>a.Id == id).First();
            _db.Employees.Remove(deletedata);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult AddDetails() 
        {
            ViewBag.msg = "Add";
            return View();
        }
        [HttpPost]
        public IActionResult AddDetails(EmployeeModel h) 
        {
            if(h.Id == 0)
            {
                _db.Employees.Add(h);
                _db.SaveChanges();
                
            }
            else
            {
                _db.Entry(h).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                
            }
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int Id)
        {
            var editdata = _db.Employees.Find(Id);

            ViewBag.msg = "Update";

            return View("AddDetails", editdata);
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
        [AllowAnonymous]
        public IActionResult User()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult User(UserModel k)
        {
            var emp_Login = _db.Users.Where(a => a.Email.ToLower() == k.Email.ToLower()).FirstOrDefault();
            if (emp_Login == null)
            {
                TempData["InvalidEmail"] = "Please Enter Valid Email";
            }
            else
            {
                if (emp_Login.Email.ToLower() == k.Email.ToLower() && emp_Login.Password == k.Password)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, emp_Login.Name));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, emp_Login.Name));
                    ClaimsIdentity m = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal p = new ClaimsPrincipal(m);
                    HttpContext.SignInAsync(p);
                    HttpContext.Session.SetString("c", emp_Login.Name);


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["InvalidPassword"] = "Please Enter Correct Password";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("User");
        }
        
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Signup(UserModel j)
        {
            if (j != null)
            {
                _db.Users.Add(j);
                _db.SaveChanges();
            }
            return RedirectToAction("User");
        }
    }
}
