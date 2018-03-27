using ASFront.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;


public static class GenericPrincipalExtensions
{


    public static string CurrentUserID
    {
        get
        {

            return string.IsNullOrEmpty(HttpContext.Current.User.Identity.GetUserId()) ? "0" : HttpContext.Current.User.Identity.GetUserId();
        }
    }

    public static string CurrentUserName
    {
        get
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                string _userName = string.Empty;
                //_userName = DCu.ArisUsersListView.Where(p => p.Id == CurrentUserID).Select(p => p.FullName).FirstOrDefault() ?? string.Empty;

                //return HttpContext.Current.User.Identity.Name;


                //if (HttpContext.Current.Session["CurrentUserName"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["CurrentUserName"].ToString()))
                //    _userName= HttpContext.Current.Session["CurrentUserName"].ToString();
                //else
                {
                    _userName = db.UserASProfiles.Where(p => p.UserId == CurrentUserID).Select(p => (p.FirstName + " " + p.LastName + " " + p.Patronymic)).FirstOrDefault() ?? string.Empty;
                    //HttpContext.Current.Session["CurrentUserName"] = _userName;
                }

                return _userName;
            }
            else return string.Empty;
        }
    }


    public static string FullName(this IPrincipal user)
    {

        if (user.Identity.IsAuthenticated)
        {

            ////ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
            //ClaimsIdentity claimsIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            //foreach (var claim in claimsIdentity.Claims)
            //{
            //    if (claim.Type == "FullName")
            //        return claim.Value;
            //}
            //return "";



            //if (HttpContext.Current.Session["CurrentUserName"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["CurrentUserName"].ToString()))
            //    return HttpContext.Current.Session["CurrentUserName"].ToString();
            //else
            {
                //HttpContext.Current.Session["CurrentUserName"] = CurrentUserName;
                return CurrentUserName;
            }
        }
        else
            return "";
    }



    public static string IsAdmin(this IPrincipal user)
    {

        if (user.Identity.IsAuthenticated)
        {

            ////ClaimsIdentity claimsIdentity = user.Identity as ClaimsIdentity;
            //ClaimsIdentity claimsIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;

            //foreach (var claim in claimsIdentity.Claims)
            //{
            //    if (claim.Type == "FullName")
            //        return claim.Value;
            //}
            //return "";



            //if (HttpContext.Current.Session["CurrentUserName"] != null && !string.IsNullOrEmpty(HttpContext.Current.Session["CurrentUserName"].ToString()))
            //    return HttpContext.Current.Session["CurrentUserName"].ToString();
            //else
            {
                //HttpContext.Current.Session["CurrentUserName"] = CurrentUserName;
                return CurrentUserName;
            }
        }
        else
            return "";
    }




}

