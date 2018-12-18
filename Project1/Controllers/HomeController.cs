using Project1.Managers;
using Project1.Models;

using System.Web.Mvc;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private IStudentManager _studentManager;
        private ICaptainsManager _captainsManager;
        private IGroupsManager _groupsManager;

        public HomeController(IStudentManager studentManager, ICaptainsManager captainsManager, IGroupsManager groupsManager)
        {
            _groupsManager = groupsManager;
            _studentManager = studentManager;
            _captainsManager = captainsManager;
        }

        public ActionResult Index()
        {
            Captain captain = _captainsManager.GetCurrent();
            if (captain == null)
                return RedirectToAction("Login", "Account");

            var student = _studentManager.GetStudent(captain.StudentId);
            ViewBag.GroupName = _groupsManager.GetGroup(student.Group_Id).Name;
            return View(student);
        }

        public ActionResult About()
        {
            Captain captain = _captainsManager.GetCurrent();
            if (captain == null)
                return RedirectToAction("Login", "Account");
           
            return View(captain);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult About([Bind(Include = "Email,PassHash,Note,Note,StudentId,GroupId")] Captain cap)
        {
            _captainsManager.Update(cap);
            return View();
        }
        
    }
}