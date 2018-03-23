using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {// Model binding will bind similar named item
        public int MenuID { get; set; }
        [Required,Display(Name = "Pick")]
        public int CheeseID { get; set; }

        public Menu Menu { get; set; }
        [Display(Name = "Pick your Cheese:")]
        public List<SelectListItem> Cheeses { get; set; } = new List<SelectListItem>();

        public AddMenuItemViewModel() { }// For model binding

        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            Menu = menu;
            foreach (var cheese in cheeses)
            {// Add a newly initialize SelectListItem to the list Called Cheeses <Big "C"
                Cheeses.Add(new SelectListItem
                {// when the user select the entry Text, the value will be posted
                    // in the asp-for="ViewModelField"
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
            }
        }
    }
}
