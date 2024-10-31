using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSCC.CW1._13786.FrontEnd.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}