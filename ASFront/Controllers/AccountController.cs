using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ASFront.Models;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ASFront.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;



        public AccountController()
        {
            context = new Models.ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllUsers()
        {





            var items =
               (
                  from u in context.Users

                  join up in context.UserASProfiles on u.Id equals up.UserId
                  join br in context.Branches on up.BrancheId equals br.Id
                  select new UserProfileView
                  {
                      UserASProfileId = up.UserASProfileId,
                      UserId = u.Id,
                      UserName = u.UserName,
                      Email = u.Email,

                      branchId = br.Id,
                      branch = br.Branch,

                      asName = up.asUserName,
                      asCode = up.asUserCode,
                      asUserId = up.asUserId,
                       
                       FirstName= up.FirstName,
                       LastName = up.LastName,
                       Patronymic = up.Patronymic

                  }



               ).ToList();






            return View(items);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowAllUsersEx()
        {
            //var allUsers = context.Users.Join(context.UserASProfiles,
            //    mu => mu.Id,
            //    up => up.UserId,
            //    (mu, up) => new
            //    {
            //        usrID = mu.Id,
            //        userName = mu.UserName,
            //        uEmail = mu.Email,
            //        branchId = up.Branches.BranchesId,
            //        branch = up.Branches.Branch,
            //        asName = up.asUserName,
            //        asCode = up.asUserCode,
            //        asUserId = up.asUserId
            //    }).OrderBy(x => new { x.branchId });

            //UserProfileView upp;
            List<UserProfileView> upList = (
                  from u in context.Users

                  join up in context.UserASProfiles on u.Id equals up.UserId
                  join br in context.Branches on up.BrancheId equals br.Id
                  select new UserProfileView
                  {
                      UserASProfileId = up.UserASProfileId,
                      UserId = u.Id,
                      UserName = u.UserName,
                      Email = u.Email,

                      branchId = br.Id,
                      branch = br.Branch,

                      asName = up.asUserName,
                      asCode = up.asUserCode,
                      asUserId = up.asUserId ,

                         FirstName = up.FirstName,
                      LastName = up.LastName,
                      Patronymic = up.Patronymic
                  }



               ).ToList();

            return View("ShowAllUsers", upList);
        }



        [Authorize(Roles = "Admin")]
        public ActionResult UserProfile(string id)
        {
            //var usr = context.Users.Where(u => u.Id == id);
            //string userName = string.Empty;
            //string email = string.Empty;
            //foreach (var u in usr)
            //{
            //    userName = u.UserName;
            //    email = u.Email;
            //}



            //UserProfileView upv = new UserProfileView();
            //foreach (var p in prf)
            //{
            //    upv.UserId = id;
            //    upv.asCode = p.asUserCode;
            //    upv.asName = p.asUserName;
            //    upv.asUserId = p.asUserId;
            //    if (p.Branches != null)
            //        upv.branchId = p.Branches.BranchesId;
            //}

            //upv.UserId = id;
            //upv.UserName = userName;
            //upv.Email = email;




            // var prf = context.UserASProfiles.Include("Branches").Where(p => p.UserId == id).ToList();
             if(string.IsNullOrWhiteSpace(id) || !context.Users.Any(i => i.Id == id))
                return RedirectToAction("ShowAllUsers");

            UserProfileView item = new UserProfileView();

            var RoleId = context.Users.Where(i => i.Id == id).Select(s => s.Roles).FirstOrDefault().Select(p => p.RoleId).FirstOrDefault();

            if (context.UserASProfiles.Any(p => p.UserId == id))
            {
                item =
                 (
                    from u in context.Users

                    join up in context.UserASProfiles on u.Id equals up.UserId
                    join br in context.Branches on up.BrancheId equals br.Id

                    

                    where u.Id == id
                    select new UserProfileView
                    {
                        UserASProfileId = up.UserASProfileId,
                        UserId = u.Id,
                        UserName = u.UserName,
                        Email = u.Email,

                        Role= RoleId,

                        branchId = br.Id,
                        branch = br.Branch,

                        asName = up.asUserName,
                        asCode = up.asUserCode,
                        asUserId = up.asUserId,

                        FirstName = up.FirstName,
                        LastName = up.LastName,
                        Patronymic = up.Patronymic
                    }

             ).FirstOrDefault();


            }
            else

            {

                item =
                 (
                    from u in context.Users



                    where u.Id == id
                    select new UserProfileView
                    {
                        UserASProfileId = 0,
                        UserId = u.Id,
                        UserName = u.UserName,
                        Email = u.Email,

                        Role = string.Empty,
                        branchId = 0,
                        branch = string.Empty,

                        asName = string.Empty,
                        asCode = string.Empty,
                        asUserId = string.Empty
                    }

             ).FirstOrDefault();

            }

            ViewBag.br = new SelectList(context.Branches, "Id", "Branch");
            ViewBag.Roles = new SelectList(context.Roles.ToList(), "Id", "Name");

            var brL = context.Branches.Take(5).Select(p => new NameIdView { Id= p.Id, Name=p.Branch }).ToList().AsEnumerable<NameIdView>();


            //item.UserBranches = context.Branches.Take(5).Select(p => p.Id).ToList().ToArray<int>();
            //var SelectedBranches = context.Branches.Take(5).Select(p => p.Id.ToString()).ToList().AsEnumerable<string>();

            //var serializer = new JavaScriptSerializer();
            //var serializedResult = serializer.Serialize(SelectedBranches);

            //var str = JsonConvert.SerializeObject(SelectedBranches);


            //ViewBag.SelectedBranches = serializedResult;    

            item.branchId = context.Branches.Take(5).Select(p => p.Id).FirstOrDefault();

            item.UserBranches = context.BranchUsers.Where(p=>p.UserASProfileId== item.UserASProfileId).Select(p => p.BrancheId).ToList().ToArray<int>();

           

            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public  ActionResult UserProfile(UserProfileView uprf)
        {
            Branches b =
                context.Branches.Where(p => p.Id == uprf.UserBranches.FirstOrDefault()).FirstOrDefault();
            var prf = context.UserASProfiles.Where(p => p.UserId == uprf.UserId).FirstOrDefault();

            string userId = string.Empty;

            if ( uprf.UserBranches == null  || !(uprf.UserBranches != null && uprf.UserBranches.Count() > 0)  )
            {
                ModelState.AddModelError("UserBranches", "Մասնաճյուղերը չեն ընտրված");
            }



                if (ModelState.IsValid && Request.Form["Save"] != null && Request.Form["Save"].Equals(Resources.Page.Save) )
            {
                if (prf != null)
                {
                    UserASProfiles p = context.UserASProfiles.Where(k => k.UserASProfileId == uprf.UserASProfileId).SingleOrDefault();
                    // foreach (var p in prf)
                    {
                        //p.UserId = prf[0].UserId;
                        p.asUserCode = uprf.asCode;
                        p.asUserId = uprf.asUserId;
                        p.asUserName = uprf.asName;

                        p.FirstName = uprf.FirstName;
                        p.LastName = uprf.LastName;
                        p.Patronymic = uprf.Patronymic;
                        p.BrancheId = b.Id;//p.Branches = b;

                        p.uPhoneNumber = uprf.uPhoneNumber;
                        p.telegramId = uprf.telegramId;
                        p.telegramId = uprf.telegramId;


                        //p.Branches = b;
                        context.SaveChanges();
                    }

                    userId = p.UserId; 

                    //UserASProfiles p = context.UserASProfiles.Find();                                
                }
                else
                {
                    UserASProfiles p = new UserASProfiles();
                    p.UserId = uprf.UserId;
                    p.asUserCode = uprf.asCode;
                    p.asUserId = uprf.asUserId;
                    p.asUserName = uprf.asName;

                    p.FirstName = uprf.FirstName;
                    p.LastName = uprf.LastName;
                    p.Patronymic = uprf.Patronymic;

                    p.BrancheId = b.Id;
                    p.Branches = b;
                    p.uPhoneNumber = uprf.uPhoneNumber;
                    p.telegramId = uprf.telegramId;
                    p.telegramId = uprf.telegramId;

                    context.UserASProfiles.Add(p);
                    context.SaveChanges();

                    userId = p.UserId;
                    uprf.UserASProfileId = p.UserASProfileId;

                }
                

                    var RoleId = uprf.Role;

                  var user = UserManager.FindById(userId);
               

               var userRoles =user.Roles.ToList();

                //foreach( var it in userRoles)
                //{
                //    user.Roles.Remove(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole { RoleId = it.RoleId, UserId = userId });
                //}
                //  user.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole { RoleId = RoleId, UserId = userId });
                //UserManager.Update(user);




                var roleName = context.Roles.Where(p => p.Id == uprf.Role).Select(p => p.Name).FirstOrDefault();



                foreach (var it in userRoles)
                {
                    UserManager.RemoveFromRole(userId, context.Roles.Where(p => p.Id == it.RoleId).Select(p => p.Name).FirstOrDefault());
                }
               
                UserManager.Update(user);


                UserManager.AddToRole(userId, roleName);




                var userBranchesList = context.BranchUsers.Where(p => p.UserASProfileId == uprf.UserASProfileId).ToList();

                context.BranchUsers.RemoveRange(userBranchesList);
                context.SaveChanges();


                List<BranchUsers> userBranchesListNew = new List<BranchUsers>();


                   foreach(int brItem in uprf.UserBranches)
                {

                    BranchUsers bu = new BranchUsers();

                    bu.BrancheId = brItem;
                    bu.UserASProfileId = uprf.UserASProfileId;


                    userBranchesListNew.Add(bu);

                }




                context.BranchUsers.AddRange(userBranchesListNew);
                context.SaveChanges();


                // Roles.AddUserToRole(userId, roleName);



                //var item =
                // (
                //    from u in context.Users

                //    join up in context.UserASProfiles on u.Id equals up.UserId
                //    join br in context.Branches on up.branch_BranchesId equals br.BranchesId

                //    where u.Id == id
                //    select new UserProfileView
                //    {
                //        UserASProfileId = up.UserASProfileId,
                //        UserId = u.Id,
                //        UserName = u.UserName,
                //        Email = u.Email,

                //        branchId = br.BranchesId,
                //        branch = br.Branch,

                //        asName = up.asUserName,
                //        asCode = up.asUserCode,
                //        asUserId = up.asUserId



                //    }



                // ).FirstOrDefault();
                //return RedirectToAction("UserProfile", new { id = uprf.UserId });
                return RedirectToAction("ShowAllUsers");
            }

        


            ViewBag.br = new SelectList(context.Branches, "Id", "Branch");
            ViewBag.Roles = new SelectList(context.Roles.ToList(), "Id", "Name");
          
            return View(uprf);


        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddNewUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddNewUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("UserProfile", new { id = user.Id });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterRole()
        {
            ViewBag.Name = new SelectList(context.Roles.ToList(), "Name", "Name");
            ViewBag.UserName = new SelectList(context.Users.ToList(), "UserName", "UserName");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterRole(RegisterViewModel model, ApplicationUser user)
        {
            var userId = context.Users.Where(i => i.UserName == user.UserName).Select(s => s.Id);
            string updateId = "";
            foreach (var i in userId)
            {
                updateId = i.ToString();
            }
            await this.UserManager.AddToRoleAsync(updateId, model.Name);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("", "Application");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}