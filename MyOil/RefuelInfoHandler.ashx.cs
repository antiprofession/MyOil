using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyOil
{
    /// <summary>
    /// 处理客户端上传的数据
    /// </summary>
    public class RefuelInfoHandler : IHttpHandler
    {

        OilEntities db = new OilEntities();

        public void ProcessRequest(HttpContext context)
        {
            OilConsumeInfo oci = new OilConsumeInfo();
            oci.user_UserId = int.Parse(context.Request["ID"]);
            oci.Oilprice = Decimal.Parse(context.Request["price"]);
            oci.oilQuantity = Decimal.Parse(context.Request["quantity"]);
            oci.totalCost =  Decimal.Parse(context.Request["cost"]);
            oci.gs_id = int.Parse(context.Request["gsid"]);
            oci.gs_id = int.Parse(context.Request["gtid"]);
            oci.refuelTime = context.Request["time"];
            
            context.Response.ContentType = "text/plain";
            try{
                db.OilConsumeInfoes.AddObject(oci);
                db.SaveChanges();
                context.Response.Write("OK");
            }
            catch(Exception e)
            {
                context.Response.Write("F");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}