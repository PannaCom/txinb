using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using txinb.Models;
using PagedList;
using PagedList.Mvc;

namespace txinb.Controllers
{
    public class HomeController : Controller
    {
        private txinbEntities db = new txinbEntities();

        public ActionResult Index()
        {
            return View();
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

        public ActionResult NotFoundPage(string aspxerrorpath)
        {
            if (!string.IsNullOrEmpty(aspxerrorpath))
            {
                return RedirectToRoute("NotFound");
            }
            return View();
        }

        public ActionResult LoadProductNewHot()
        {
            var model = (from o in db.products where o.status == true && o.product_new_type == 2 && o.product_photo2 != null orderby o.updated_date descending select o).ToList().Take(10).ToList();
            return PartialView("_SectionProductHot", model);
        }

        public ActionResult ProductDetail(long? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("NotFound");
            }

            var _product = (from o in db.products where o.status == true && o.product_id == id select o).FirstOrDefault();
            if (_product == null)
            {
                return RedirectToRoute("NotFound");
            }

            return View(_product);
        }

        public ActionResult LoadProductInvolve(long? id)
        {
            var model = (from s in db.products where s.product_id != id && s.status == true orderby s.updated_date descending select s).ToList().Take(10).ToList();
            return PartialView("_LoadProductInvolve", model);
        }

        public ActionResult LoadProductNew()
        {
            var model = (from s in db.products where s.status == true orderby s.updated_date descending select s).ToList().Take(10).ToList();
            return PartialView("_LoadProductNew", model);
        }

        public ActionResult ProductCat(int? id, int? pg)
        {
            if (id == null || id == 0)
            {
                return RedirectToRoute("NotFound");
            }

            var _cat = (from o in db.cats where o.cat_id == id select o).FirstOrDefault();
            if (_cat == null)
            {
                return RedirectToRoute("NotFound");
            }

            ViewBag.TenDanhMuc = _cat.cat_name;
            ViewBag.URLDanhMuc = _cat.cat_url;
            ViewBag.IdDanhMuc = id;

            int pageSize = 10;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;
            var data = (from q in db.products where q.cat_id == id && q.status == true select q);
            if (data == null)
            {
                return View(data);
            }

            return View(data.ToList().ToPagedList(pageNumber, pageSize));
        }

        public ActionResult LoadMenu()
        {
            var menu = (from c in db.cats where c.cat_parent_id != null select c);
            string _menu = "";
            foreach (var item in menu)
            {
                var li = string.Format("<li><a href='danh-muc/{0}-{1}'>{2}</a>", item.cat_url, item.cat_id, item.cat_name);
                _menu += li + "</li>";
            }
            return PartialView("_MenuPartial", _menu);
        }

        

    }
}