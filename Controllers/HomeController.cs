using Microsoft.AspNetCore.Mvc;
using StaffBase.Models;

namespace StaffBase.Controllers
{
    public class HomeController : Controller
    {
        IEmployeeRepository repo;
        public HomeController(IEmployeeRepository r)
        {
            repo = r;
        }
        public ActionResult Index()
        {
            return View(repo.GetStaff());
        }

        public ActionResult Details(int id)
        {
            Employee employee = repo.Get(id);
            if (employee != null)
                return View(employee);
            return NotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            repo.Create(employee);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Employee employee = repo.Get(id);
            if (employee != null)
                return View(employee);
            return NotFound();
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            repo.Update(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
            Employee employee = repo.Get(id);
            if (employee != null)
                return View(employee);
            return NotFound();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}