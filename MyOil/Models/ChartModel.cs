using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyOil.Models
{
    public class ChartModel
    {
        public string time { get; set; }
        public decimal cost{get;set;}
        public decimal quant { get; set; }
    }
}