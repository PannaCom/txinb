using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using txinb.Models;
using txinb.Helpers;
using System.Threading.Tasks;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;

namespace txinb.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
    }
}