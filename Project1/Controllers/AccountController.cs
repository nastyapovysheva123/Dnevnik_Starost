using Project1.Managers;
using Project1.Models;

using System.Web.Mvc;

namespace Project1.Controllers
{
    public class AccountController : Controller
    {
        private IStudentManager _studentManager;
        private ICaptainsManager _captainsManager;
        private IGroupsManager _groupsManager;

        public AccountController(IStudentManager studentManager, ICaptainsManager captainsManager, IGroupsManager groupsManager)
        {
            _groupsManager = groupsManager;
            _studentManager = studentManager;
            _captainsManager = captainsManager;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var current = _captainsManager.GetCurrent(model.Email, model.Password);
            
            if (current != null)
                return RedirectToAction("Index", "Home");

            return View(model);
        }     

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var current = _captainsManager.GetCurrent(model.Email, model.Password);

                if (current == null)
                {
                    var group = new Group();
                    _groupsManager.Create(group);

                    var student = new Student
                    {
                        Email = model.Email,
                        Group_Id = group.Id,
                    };
                    _studentManager.Create(student);
                    var cap = new Captain
                    {
                        Email = model.Email,
                        PassHash = model.Password,
                        StudentId = student.Id,
                        GroupId = group.Id,
                    };
                    _captainsManager.Create(cap);
                    return RedirectToAction("Create", "Groups", new { id = group.Id });
                }
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [AllowAnonymous]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login", "Account");
        }


    }
}