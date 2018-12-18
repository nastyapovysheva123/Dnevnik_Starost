using Project1.Managers;
using Project1.Models;

using System.Net;
using System.Web.Mvc;

namespace Project1.Controllers
{
    public class GroupsController : Controller
    {
        private IStudentManager _studentManager;
        private ICaptainsManager _captainsManager;
        private IGroupsManager _groupsManager;

        public GroupsController(IStudentManager studentManager, ICaptainsManager captainsManager, IGroupsManager groupsManager)
        {
            _groupsManager = groupsManager;
            _studentManager = studentManager;
            _captainsManager = captainsManager;
        }

        public ActionResult Create(string id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Group group = _groupsManager.GetGroup(id);
            if (group == null)
                return HttpNotFound();
            return View(group);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Group group)
        {
            if (ModelState.IsValid)
            {
                Captain captain = _captainsManager.GetCurrent();
                _groupsManager.Update(group);
                return RedirectToAction("EditInfo", "Students", new { id = captain.StudentId });
            }
            return View(group);
        }
    }
}
