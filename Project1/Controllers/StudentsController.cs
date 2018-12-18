using Project1.Managers;
using Project1.Models;

using System.Net;
using System.Web.Mvc;

namespace Project1.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentManager _studentManager;
        private ICaptainsManager _captainsManager;
        private IGroupsManager _groupsManager;

        public StudentsController(IStudentManager studentManager, ICaptainsManager captainsManager, IGroupsManager groupsManager)
        {
            _groupsManager = groupsManager;
            _studentManager = studentManager;
            _captainsManager = captainsManager;
        }

        // GET: Students
        public ActionResult Index()
        {
            Captain captain = _captainsManager.GetCurrent();
            if(captain == null)
                return RedirectToAction("Login", "Account");

            var students = _studentManager.GetStudents(captain.GroupId);
            return View(students);
        }

        // GET: Students/Details/5
        public ActionResult Details()
        {
            Captain captain = _captainsManager.GetCurrent();
            if (captain == null)
                return RedirectToAction("Login", "Account");

            var students = _studentManager.GetStudents(captain.GroupId);
            return View(students);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            Captain captain = _captainsManager.GetCurrent();
            if (captain == null)
                return RedirectToAction("Login", "Account");

            return View();
        }
         
        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Patronymic,Birthday,Phone,Address,Email,Group_Id")] Student student)
        {
            if (ModelState.IsValid)
            {
                Captain captain = _captainsManager.GetCurrent();
                student.Group_Id = captain.GroupId;
                _studentManager.Create(student);
                return RedirectToAction("Index");
            }
            
            return View(student);
        }        

        // GET: Students/Edit/5
        public ActionResult Edit(string id)
        {
            Captain captain = _captainsManager.GetCurrent();
            if (captain == null)
                return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = _studentManager.GetStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Patronymic,Birthday,Phone,Address,Email,Group_Id")] Student student)
        {            
            if (ModelState.IsValid)
            {
                _studentManager.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }


        // GET: Students/Edit/5
        public ActionResult EditInfo(string id)
        {
            Captain captain = _captainsManager.GetCurrent();
            if (captain == null)
                return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentManager.GetStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditInfo([Bind(Include = "Id,Name,Surname,Patronymic,Birthday,Phone,Address,Email,Group_Id")] Student student)
        {
            if (ModelState.IsValid)
            {
                _studentManager.Update(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(string id)
        {
            Captain captain = _captainsManager.GetCurrent();
            if (captain == null)
                return RedirectToAction("Login", "Account");


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = _studentManager.GetStudent(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            _studentManager.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
