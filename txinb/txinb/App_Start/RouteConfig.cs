using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace txinb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               "Admin",
               "admin",
               new { controller = "Admin", action = "Index" }
             );

            routes.MapRoute(
              "LoginAccount",
              "admin/login",
              new { controller = "Account", action = "Login" }
            );

            //AdminAddCat 
            routes.MapRoute(
             "AdminAddCat",
             "admin/cat/add",
             new { controller = "Cats", action = "Add" }
           );

            routes.MapRoute(
            "AdminEditCat",
            "admin/cat/edit/{id}",
            new { controller = "Cats", action = "Edit", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "AdminDeleteCat",
            "admin/cat/delete/{id}",
            new { controller = "Cats", action = "Delete", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "AdminListCat",
            "admin/cat/list",
            new { controller = "Cats", action = "List" }
          );

            //AdminAddProduct
            routes.MapRoute(
             "AdminAddProduct",
             "admin/product/add",
             new { controller = "Products", action = "Add" }
           );

            routes.MapRoute(
            "AdminEditProduct",
            "admin/product/edit/{id}",
            new { controller = "Products", action = "Edit", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "AdminDeleteProduct",
            "admin/product/delete/{id}",
            new { controller = "Products", action = "Delete", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "AdminListProduct",
            "admin/product/list",
            new { controller = "Products", action = "List" }
          );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
