using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ScaleModelsStore.Models;
using ScaleModelsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ScaleModelsStore.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Manage
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Password has been successfully changed."
                : message == ManageMessageId.ChangeEmailSuccess ? "E-mail address successfully changed"
                : message == ManageMessageId.ChangePhoneSuccess ? "Phone number successfully changed"
                : message == ManageMessageId.ChangeAddressSuccess ? "Address successfully changed"
                : "";

            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            string status;
            if(user==null)
            {
                ViewBag.ErrorMessage = "User not found";
                return View("Error");
            }
            if (user.EmailConfirmed)
                status = "Confirmed";
            else
                status = "Not confirmed";
            

            var viewModel = new UserInfoViewModel
            {
                LastName = User.Identity.GetLastName(),
                FirstName = User.Identity.GetFirstName(),
                Email = User.Identity.GetEmail(),       
                EmailStatus=status,
                PhoneNumber = UserManager.GetPhoneNumber(userId),
                PostalCode = User.Identity.GetPostalCode(),
                Country = User.Identity.GetCountry(),
                City = User.Identity.GetCity(),
                Address = User.Identity.GetAddress()
            };
            return View(viewModel);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if(result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);

        }

        public ActionResult ChangeEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                user.UserName = model.NewEmail;
                user.Email = model.NewEmail;

                await UserManager.UpdateAsync(user);
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeEmailSuccess });
            }

            return View(model);
        }

        public ActionResult ChangePhone()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Phone = UserManager.GetPhoneNumber(userId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePhone(ChangePhoneViewModel model)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.Phone = UserManager.GetPhoneNumber(userId);

            if (!ModelState.IsValid)
                return View(model);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                user.PhoneNumber = model.NewPhone;

                await UserManager.UpdateAsync(user);
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePhoneSuccess });
            }

            return View(model);
        }

        public ActionResult ChangeAddress()
        {
            ViewBag.Countries = new SelectList(Utils.CountriesList(), "Key", "Value").OrderBy(p => p.Text).ToList();                        
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeAddress(ChangeAddressViewModel model)
        {
            ViewBag.Countries = new SelectList(Utils.CountriesList(), "Key", "Value").OrderBy(p => p.Text).ToList();
            if (!ModelState.IsValid)
                return View(model);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                user.PostalCode = model.PostalCode;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                await UserManager.UpdateAsync(user);
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeAddressSuccess });
            }

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult AccountMenu()
        {
            return PartialView();
        }

        #region Helpers
        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

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

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePhoneSuccess,
            ChangePasswordSuccess,
            ChangeEmailSuccess,
            ChangeAddressSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}