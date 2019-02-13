using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mdmOverview.Models;
using mdmOverview.Models.MoyPark;

namespace mdmOverview.Controllers
{
    public class HomeController : Controller
    {
        private DAL dal = new DAL();
        public ActionResult Index()
        {
            dtoRawInterfaceDataSummary dto = new dtoRawInterfaceDataSummary();
            dto = dal.getHomeData();
            return View(dto);
        }


        public ActionResult Detail(string materialid)
        {
            dtoProduct dto = new dtoProduct();
            dto = dal.getProduct(materialid);
            return View(dto);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}