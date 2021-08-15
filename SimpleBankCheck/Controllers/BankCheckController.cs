using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBankCheck.Controllers
{
    public class BankCheckController : Controller
    {
        // GET: BankCheck
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View("DetailsView");
        }
    }
}