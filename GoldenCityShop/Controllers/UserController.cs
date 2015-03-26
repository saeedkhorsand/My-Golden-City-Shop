using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using CaptchaMvc.HtmlHelpers;
using DataLayer.Context;
using DomainClasses.Entities;
using DomainClasses.Enums;
using GoldenCitShop.Filters;
using ServiceLayer.Interfaces;
using Utilities.Security;
using ViewModel;

namespace GoldenCityShop.Controllers
{
    [RoutePrefix("Customer")]
    [Route("{action}")]
    public partial class UserController : Controller
    {
        #region Fields
        private readonly IRoleService _roleService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        #endregion //Fields

        #region Constructor
        /// <summary>
        /// use this constructor for Inject Dependencies
        /// </summary>
        /// <param name="userService">User Service DI</param>
        /// <param name="unitOfWork">Unit Of Work DI</param>
        /// <param name="roleService">Role Service DI</param>
        public UserController(IUserService userService, IUnitOfWork unitOfWork, IRoleService roleService,IProductService productService)
        {
            _roleService = roleService;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        #endregion //Constructor

        #region Login
        /// <summary>
        /// action for render login view
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (Request.IsAjaxRequest())
                return PartialView(MVC.User.Views._LoginPartial);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0,VaryByParam = "*")]
        public virtual async Task<ActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                if (Request.IsAjaxRequest())
                    return PartialView(MVC.User.Views._LoginPartial, viewModel);
                return View(viewModel);
            }

            var userName = string.Empty;
            long userId = 0;
            var ip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            var verificationResult =
                 _userService.VerifyUserByPhoneNumber(viewModel.PhoneNumber, viewModel.Password, ref userName, ref userId, ip);

            switch (verificationResult)
            {
                case VerifyUserStatus.VerifiedSuccessfully:
                    {
                        var roleOfTheUser = await _roleService.GetRoleByUserId(userId);

                        // set user role cookie
                        SetAuthCookie(userName, roleOfTheUser.Name, viewModel.RememberMe);

                        await _unitOfWork.SaveAllChangesAsync(false);
                        if (Request.IsAjaxRequest())
                            return JavaScript(IsValidReturnUrl(returnUrl)
                                ? string.Format("window.location ='{0}';", returnUrl)
                                : "window.location.reload();");
                        if (IsValidReturnUrl(returnUrl))
                            return Redirect(returnUrl);
                        return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
                    }
                case VerifyUserStatus.UserIsBaned:
                    ModelState.AddModelError("PhoneNumber", "حساب کاربری شما مسدود است");
                    ModelState.AddModelError("Password", "حساب کاربری شما مسدود است");
                    break;
                default:
                    ModelState.AddModelError("PhoneNumber", "اطلاعات وارد شده صحیح نمی باشند");
                    ModelState.AddModelError("Password", "اطلاعات وارد شده صحیح نمی باشند");
                    break;
            }
            if (Request.IsAjaxRequest())
                return PartialView(MVC.User.Views._LoginPartial, viewModel);
            return View(viewModel);
        }

        #endregion //Login

        #region Validation
        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult ForgetPasswordCheckPhoneNumberExist(string phoneNumber)
        {
            return _userService.ExistsByPhoneNumber(phoneNumber)
                ? Json(true)
                : Json(false);
        }


        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult CheckUserNameIsExist(string userName)
        {
            return _userService.ExistsByUserName(userName)
                ? Json(false)
                : Json(true);
        }

        [HttpPost]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual JsonResult CheckPhoneNumberIsExist(string phoneNumber)
        {
            return _userService.ExistsByPhoneNumber(phoneNumber)
                ? Json(false)
                : Json(true);
        }

        #endregion

        #region Vaidation return url
        [NonAction]
        private bool IsValidReturnUrl(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1;
        }
        #endregion //Validation return url

        #region Register
        /// <summary>
        /// Register action to render partial View. the request just is Ajax
        /// </summary>
        /// <returns>partial view</returns>
        [HttpGet]
        [AjaxOnly]
        [Route("Register")]
        public virtual ActionResult Register()
        {
            return PartialView(MVC.User.Views._RegisterPartial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [Route("Register")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<ActionResult> Register(RegisterViewModel viewModel)
        {
            if (!this.IsCaptchaValid("جواب تصویر صحیح نمیباشد"))
            {
                return PartialView(MVC.User.Views._RegisterPartial, viewModel);
            }
            if (!ModelState.IsValid)
            {
                return PartialView(MVC.User.Views._RegisterPartial, viewModel);
            }
            var newUser = new User
            {
                RegisterDate = DateTime.Now,
                IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                IsBaned = false,
                UserName = viewModel.UserName,
                PhoneNumber = viewModel.PhoneNumber,
                Password = Encryption.EncryptingPassword(_userService.GeneratePassword()),
                Role = _roleService.GetRoleByName("user"),
                LastLoginDate = DateTime.Now
            };

            var addingNewUserResult = _userService.Add(newUser);

            switch (addingNewUserResult)
            {
                case AddUserStatus.PhoneNumberExist:
                    ModelState.AddModelError("PhoneNumber", "شماره همراه وارد شده قبلا درسیستم ثبت شده است.");
                    return PartialView(MVC.User.Views._RegisterPartial, viewModel);
                case AddUserStatus.UserNameExist:
                    ModelState.AddModelError("UserName", "نام کاربری وارد شده توسط کاربران قبلا رزرو شده است.");
                    return PartialView(MVC.User.Views._RegisterPartial, viewModel);
            }

            await _unitOfWork.SaveAllChangesAsync(false);
            ViewBag.SuccessMessage = "اطلاعات شما با موفقیت ثبت  و کد ورود در قالب پیامک به همراه شما ارسال شد";
            return PartialView(MVC.Shared.Views._SuccessPartial);
        }

        #endregion //Register

        #region Authentication
        [NonAction]
        private void SetAuthCookie(string userName, string roleofUser, bool presistantCookie)
        {
            var timeout = presistantCookie ? FormsAuthentication.Timeout.TotalMinutes : 30;

            var now = DateTime.UtcNow.ToLocalTime();
            var expirationTimeSapne = TimeSpan.FromMinutes(timeout);

            var authTicket = new FormsAuthenticationTicket(
                1,
                userName,
                now,
                now.Add(expirationTimeSapne),
                presistantCookie,
                roleofUser,
                FormsAuthentication.FormsCookiePath
                );

            var encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (FormsAuthentication.CookieDomain != null)
            {
                authCookie.Domain = FormsAuthentication.CookieDomain;
            }

            if (presistantCookie)
                authCookie.Expires = DateTime.Now.AddMinutes(timeout);

            Response.Cookies.Add(authCookie);
        }
        #endregion //Authentication

        #region ForgetPassword

        [HttpGet]
        [AjaxOnly]
        [Route("ForgetPass")]
        public virtual ActionResult ForgetPassword()
        {
            return PartialView(MVC.User.Views._ForgetPasswordPartial);
        }

        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        [Route("ForgetPass")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
        {

            if (!this.IsCaptchaValid("جواب تصویر صحیح نمیباشد"))
            {
                return PartialView(MVC.User.Views._ForgetPasswordPartial, viewModel);
            }
            if (!ModelState.IsValid)
            {
                return PartialView(MVC.User.Views._ForgetPasswordPartial, viewModel);
            }

           
              //  return PartialView(MVC.User.Views._ForgetPasswordPartial, viewModel);

            await _unitOfWork.SaveAllChangesAsync(false);
            if (true) return PartialView(MVC.Shared.Views._SuccessPartial);
        }
        #endregion //ForgetPassword

        #region LogOut

        [HttpPost]
        [SiteAuthorize]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(MVC.Home.ActionNames.Index, MVC.Home.Name);
        }

        #endregion

        #region Panel

        public virtual ActionResult Account()
        {
            return View();
        }
        [Route("WishList/{page=1}")]
        public virtual ActionResult WishList(int page=1)
        {
            int total;
            var products = _userService.GetUserWishList(out total, page, 10, User.Identity.Name);
            ViewBag.TotalWishList = total;
            ViewBag.Page = page;
            return View(products);
        }

        public virtual ActionResult Orders()
        {
            return View();
        }

        public virtual ActionResult Comments()
        {
            return View();
        }
        #endregion
    }
}
