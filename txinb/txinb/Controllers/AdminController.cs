using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using txinb.Models;

namespace txinb.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            using (txinbEntities db = new txinbEntities())
            {
                ViewBag.SoDanhMuc = db.cats.Count();
                ViewBag.SoSanPham = db.products.Count();
            }
            return View();
        }
    }
}