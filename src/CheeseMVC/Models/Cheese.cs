﻿using System.Collections.Generic;

namespace CheeseMVC.Models
{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public CheeseCategory Category { get; set; }
        public int CategoryID { get; set; }//Must be named this way for EF
        // CategoryID = id in Category Table. It is a foreign key
        public int ID { get; set; }

        public IList<CheeseMenu> CheeseMenus { get; set; }
    }
}
