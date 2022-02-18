using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;       
        }
        public IActionResult Index()
        {
            //var objCategoryList = _context.Categories.ToList();

            //If you use IEnumerable, ToList() is not required
            IEnumerable<Category> objCategoryList = _context.Categories;
            return View(objCategoryList);
        }

     //Create
        //GET
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _context.Categories.Add(obj);                           //default Add method
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

         //Edit
            //GET
            public IActionResult Edit(int? id)
            {
                if (id==null || id==0)
                {
                    return NotFound();
                }
                var categoryFromDb = _context.Categories.Find(id);        //default Find method

                //var categoryFromDb = _context.Categories.SingleOrDefault(u => u.Id == id);
                //var categoryFromDb = _context.Categories.FirstOrDefault(u => u.Id == id);

                if (categoryFromDb == null)
                {
                    return NotFound();
                }
                return View(categoryFromDb);
            }

            //POST
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(Category obj)
            {
                if (obj.Name == obj.DisplayOrder.ToString())
                {
                    ModelState.AddModelError("Name", "The DisplayOrder cannot exactly match the Name");
                }
                if (ModelState.IsValid)
                {
                    _context.Categories.Update(obj);     //default Update method
                    _context.SaveChanges();
                    TempData["success"] = "Category updated successfully";
                    return RedirectToAction("Index");
                }
                return View(obj);

            }

        //Delete
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _context.Categories.Find(id);        //default Find method

            //var categoryFromDb = _context.Categories.SingleOrDefault(u => u.Id == id);
            //var categoryFromDb = _context.Categories.FirstOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)      //just changed name as two methods can't have identical signatures
        {
            var obj = _context.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(obj);     //default Update method
            _context.SaveChanges();
            TempData["success"] = "Category deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}
