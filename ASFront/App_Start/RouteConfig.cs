using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASFront
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Application", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute("GetClientsGroupsAcra",
       "acra/GetClientsGroupsAcra/",
        new { controller = "acra", action = "GetClientsGroupsAcra" },
        new[] { "ASFront.Controllers" });

            routes.MapRoute("CalculateFamilyCost",
                 "IncomeExpenses/CalculateFamilyCost/",
                  new { controller = "IncomeExpenses", action = "CalculateFamilyCost" },
                  new[] { "ASFront.Controllers" });

            routes.MapRoute("CalculateAgro",
                            "IncomeExpenses/CalculateAgro/",
                             new { controller = "IncomeExpenses", action = "CalculateAgro" },
                             new[] { "ASFront.Controllers" });

            routes.MapRoute("CalculateShortTermLoans",
              "Balances/CalculateShortTermLoans/",
               new { controller = "Balances", action = "CalculateShortTermLoans" },
               new[] { "ASFront.Controllers" });


            routes.MapRoute("CalculateMediumLongTermLoans",
              "Balances/CalculateMediumLongTermLoans/",
               new { controller = "Balances", action = "CalculateMediumLongTermLoans" },
               new[] { "ASFront.Controllers" });



            routes.MapRoute("CalculateInventory",
             "Balances/CalculateInventory/",
              new { controller = "Balances", action = "CalculateInventory" },
              new[] { "ASFront.Controllers" });


            routes.MapRoute("ListRoles",
           "Lists/Roles",
            new { controller = "Lists", action = "Roles" },
            new[] { "ASFront.Controllers" });

            routes.MapRoute("ListUserBranches",
           "Lists/UserBranches",
            new { controller = "Lists", action = "UserBranches" },
            new[] { "ASFront.Controllers" });


            routes.MapRoute("GetAppNotifications",
           "Lists/GetAppNotifications",
            new { controller = "Lists", action = "GetAppNotifications" },
            new[] { "ASFront.Controllers" });


            routes.MapRoute("ListRegion",
          "Lists/Region",
           new { controller = "Lists", action = "Region" },
           new[] { "ASFront.Controllers" });


            routes.MapRoute("ListCity",
          "Lists/City/{regName}",
           new { controller = "Lists", action = "City", regName = UrlParameter.Optional },
           new[] { "ASFront.Controllers" });


            routes.MapRoute("ListStreet",
          "Lists/Street/{cityName}",
           new { controller = "Lists", action = "Street", cityName = UrlParameter.Optional },
           new[] { "ASFront.Controllers" });

            routes.MapRoute("ListGender",
                    "Lists/Gender",
                     new { controller = "Lists", action = "Gender" },
                     new[] { "ASFront.Controllers" });


        }
    }
}
