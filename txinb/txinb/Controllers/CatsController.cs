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
    public class CatsController : Controller
    {
        private txinbEntities db = new txinbEntities();
        // GET: Cats
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(CatVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Errored"] = "Vui lòng kiểm tra lại các trường.";
                return RedirectToRoute("AdminAddCat");
            }

            try
            {
                cat _new = new cat();
                _new.cat_name = model.cat_name ?? null;
                _new.cat_parent_id = model.cat_parent_id ?? null;
                _new.cat_url = _new.cat_name != null ? configs.unicodeToNoMark(_new.cat_name) : null;
                _new.cat_pos = model.cat_pos ?? null;
                db.cats.Add(_new);
                await db.SaveChangesAsync();

                TempData["Updated"] = "Thêm danh mục thành công";
                return RedirectToRoute("AdminAddCat");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm danh mục mới");
                configs.SaveTolog(ex.ToString());
                return View(model);
            }

        }

        public PartialViewResult lstCatPartial()
        {
            List<LstCat> data = db.cats.Select(x => new LstCat()
            {
                CatId = x.cat_id,
                CatName = x.cat_name,
                ParentCatId = x.cat_parent_id,
                CatPos = x.cat_pos,
                CatURL = x.cat_url
            }).OrderBy(x => x.CatPos).ToList();

            var presidents = data.Where(x => x.ParentCatId == null).FirstOrDefault();
            SetChildrenCat(presidents, data);
            return PartialView("_lstCatPartial", presidents);
        }

        private void SetChildrenCat(LstCat model, List<LstCat> danhmuc)
        {
            var childs = danhmuc.Where(x => x.ParentCatId == model.CatId).ToList();
            if (childs.Count > 0)
            {
                foreach (var child in childs)
                {
                    SetChildrenCat(child, danhmuc);
                    model.LstCats.Add(child);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}