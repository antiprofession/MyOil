using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
/**
namespace MyOil.Models
{
    public class OilEntity:DbContext
    {
        public DbSet<GasolineStations> gasolineStations { get; set; }
        public DbSet<GasolineTypes> gasolineTypes { get; set; }
        public DbSet<OilConsumeInfoes> oilConsumeInfoes { get; set; }
        public DbSet<SideMenus> sideMenus { get; set; }
        public DbSet<SubMenus> subMenus { get; set; }
    }

    public class GasolineStations
    {
        public int id { get; set; }
        public float lge { get; set; }
        public float lae { get; set; }
        public string place { get; set; }
        public string description { get; set; }
    }

    public class GasolineTypes
    {
        public int id { get; set; }
        public string type { get; set; }
    }

    public class OilConsumeInfoes
    {
        public int id { get; set; }
        public float totalCost { get; set; }
        public float OilPrice { get; set; }
        public float oilQuantity { get; set; }
        public string refuelTime { get; set; }
        public int user_UserId { get; set; }
        public GasolineStations gs { get; set; }
        public GasolineTypes gt {get;set;}
    }

    public class SideMenus
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<SubMenus> subMenus { get; set; }
    }

    public class SubMenus
    {
        public int id { get; set; }
        public string Title { get; set; }
        public SideMenus Menu { get; set; }
    }
}
**/