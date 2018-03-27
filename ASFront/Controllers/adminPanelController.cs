using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASFront.Controllers
{
    [Authorize(Roles ="Admin")]
    public class adminPanelController : Controller
    {
        // GET: adminPanel
        public ActionResult Index()
        {
            return View();
        }
    }
}