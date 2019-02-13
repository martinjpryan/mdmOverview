using mdmOverview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mdmOverview.Controllers
{
    public class SummaryController : Controller
    {
        private DAL dal = new DAL();
        public ActionResult Index()
        {
            dtoRawInterfaceOverallSummary dto = new dtoRawInterfaceOverallSummary();
            dto = dal.getSummaryData();
            return View(dto);
        }

    }
}