using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;
        public MenuController(CheeseDbContext dbContext)//passed data fro Db static
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            ViewBag.h1 = "List of Cheese Menu";
            List<Menu> menuSet = context.Menus.ToList();
            return View(menuSet);
        }

        public IActionResult Add()
        {
            AddMenuViewModel newMenu = new AddMenuViewModel();
            return View(newMenu);
        }
        [HttpPost]
        public IActionResult Add(AddMenuViewModel passMenu)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu { Name = passMenu.Name };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
                //return View(string.Format("/menu/ViewMenu?id={0}", newMenu.ID));
            }
            return View(passMenu);
        }
        [HttpGet]//Defauly MVC routing is "/{controller}/{action}/{id}"
        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context.CheeseMenus.Include(c => c.Cheese)
                 .Where(cm => cm.MenuID == id).ToList();
            Menu menu = context.Menus.SingleOrDefault(m => m.ID == id);
            ViewMenuViewModel viewMenu = new ViewMenuViewModel
            {
                Items = items,
                Menu = menu
            };
            return View(viewMenu);       
        }

        [HttpGet]
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            AddMenuItemViewModel viewMenu = 
                new AddMenuItemViewModel(menu, context.Cheeses.ToList());
            return View(viewMenu);
        }
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel newItems)
        {
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItem = context.CheeseMenus
                    .Where(cm => cm.CheeseID == newItems.CheeseID)
                    .Where(cm => cm.MenuID == newItems.MenuID).ToList();

                if (existingItem.Count() == 0)
                {
                    Menu menu = context.Menus.Single(m => m.ID == newItems.MenuID);
                    var cheese = context.Cheeses.Single(m => m.ID == newItems.CheeseID);
                    CheeseMenu newMenu = new CheeseMenu
                    {

                        Cheese = cheese,
                        Menu = menu,
                    };
                    context.CheeseMenus.Add(newMenu);
                    context.SaveChanges();
                    return Redirect(string.Format("/Menu/ViewMenu/{0}",newItems.MenuID));
                }
            }
            return View(newItems);
        }
    }

}