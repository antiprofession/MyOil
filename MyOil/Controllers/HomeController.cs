using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;


namespace MyOil.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        OilEntities db = new OilEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sider()
        {
            var si = db.SideMenus.ToList();
            return PartialView(si);
        }

        public ActionResult GetChartOfMonth()
        {
            db.OilConsumeInfoes.GroupBy(c =>  c.refuelTime.Substring(0,7) );
            return PartialView();
        }

    }
}
