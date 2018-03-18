using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {   // Pulled from the DB
        private readonly CheeseDbContext context;
        public CategoryController(CheeseDbContext dbContext)//Placing it in Contrsuctor
        {
            context = dbContext;
            
        }
        public IActionResult Index()
        {
            ViewBag.h1 = "List of Cheese Category"; 
            List<CheeseCategory> CategorySet = context.Categories.ToList();
            return View(CategorySet);
        }
        public IActionResult Add()
        {   // You Must initiate a var to get a refernce to an Object
            // else all you are doing is creating a var with a type
            ViewBag.h1 = "Add Category of Cheese";
            AddCategoryViewModel newCategory = new AddCategoryViewModel();
            return View(newCategory);
        }
        [HttpPost]
        public IActionResult Add(AddCategoryViewModel passCategory)
        {
            if (ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory { Name = passCategory.Name };
                context.Categories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/");
            }
            else return View(passCategory);
        }
    }
}