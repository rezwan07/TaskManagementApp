using System.Web.Mvc;
using TaskManagementApp.Data;
using TaskManagementApp.Models;

namespace TaskManagementApp.Controllers
{
    public class TaskController : Controller
    {
        private DatabaseHelper dbHelper = new DatabaseHelper();

        // GET: Task
        public ActionResult Index()
        {
            var tasks = dbHelper.GetAllTasks();
            return View(tasks);
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                dbHelper.AddTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            var task = dbHelper.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                dbHelper.UpdateTask(task);
                return RedirectToAction("Index");
            }
            return View(task);
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            var task = dbHelper.GetTaskById(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            dbHelper.DeleteTask(id);
            return RedirectToAction("Index");
        }
    }
}