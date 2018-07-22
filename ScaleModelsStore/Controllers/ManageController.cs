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
            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Password has been successfully changed."
                : message == ManageMessageId.ChangeEmailSuccess ? "E-mail address successfully changed"
                : message == ManageMessageId.ChangePhoneSuccess ? "Phone number successfully changed"
                : message == ManageMessageId.ChangeAddressSuccess ? "Address successfully changed"
                : message == ManageMessageId.AddAddressSuccess ? "Address successfully added"
                : "";

            var userId = User.Identity.GetUserId();
            var user = UserManager.FindById(userId);
            string status;
            if(user==null)
            {
                ViewBag.ErrorMessage = "User not found";
                return View("Error");
            }

            var addressList = storeDb.Addresses.Where(a => a.UserId == userId).ToList();

            if (user.EmailConfirmed)
                status = "Confirmed";
            else
                status = "Not confirmed";

            var viewModel = new UserInfoViewModel
            {
                LastName = User.Identity.GetLastName(),
                FirstName = User.Identity.GetFirstName(),
                Email = User.Identity.GetEmail(),
                EmailStatus = status,
                PhoneNumber = UserManager.GetPhoneNumber(userId),
                PostalCode = (addressList.Count != 0) ? addressList.FirstOrDefault().PostalCode : "",
                Country = (addressList.Count != 0) ? addressList.FirstOrDefault().Country : "",
                City = (addressList.Count != 0) ? addressList.FirstOrDefault().City : "",
                Address = (addressList.Count != 0) ? addressList.FirstOrDefault().AddressString : ""
            };

            ViewBag.Addresses = new SelectList(Utils.GetAddressDropDown(addressList), "Key", "Value");
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

        public ActionResult AddAddress()
        {
            ViewBag.Countries = new SelectList(Utils.CountriesList(), "Key", "Value").OrderBy(p => p.Text).ToList();                        
            return View();
        }              
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddAddress(AddAddressViewModel model)
        {
            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

            ViewBag.Countries = new SelectList(Utils.CountriesList(), "Key", "Value").OrderBy(p => p.Text).ToList();
            if (!ModelState.IsValid)
                return View(model);
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                var address = new Address
                {
                    UserId = User.Identity.GetUserId(),
                    ShortDescription = model.Description,
                    PostalCode = model.PostalCode,
                    Country = model.Country,
                    City = model.City,
                    AddressString = model.Address
                };                
                await UserManager.UpdateAsync(user);
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                storeDb.Addresses.Add(address);
                storeDb.SaveChanges();

                return RedirectToAction("Index", new { Message = ManageMessageId.AddAddressSuccess });
            }

            return View(model);
        }

        public ActionResult ChangeAddress()
        {
            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

            var userId = User.Identity.GetUserId();
            var addressList = storeDb.Addresses.Where(a => a.UserId == userId).ToList();
            if (addressList.Count == 0)
            {
                ViewBag.ErrorMessage = "Your address list is empty add new address first";
                return View("Error");
            }
            ViewBag.Addresses = new SelectList(Utils.GetAddressDropDown(addressList), "Key", "Value");
            ViewBag.Countries = new SelectList(Utils.CountriesList(), "Key", "Value").OrderBy(p => p.Text).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeAddress(EditAddressViewModel model)
        {
            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();
            var userId = User.Identity.GetUserId();
            var addressList = storeDb.Addresses.Where(a => a.UserId == userId).ToList();
            ViewBag.Addresses = new SelectList(Utils.GetAddressDropDown(addressList), "Key", "Value");
            ViewBag.Countries = new SelectList(Utils.CountriesList(), "Key", "Value").OrderBy(p => p.Text).ToList();
            if (!ModelState.IsValid)
                return View(model);            
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var address = storeDb.Addresses.SingleOrDefault(a => a.UserId == userId && a.ShortDescription == model.Description);
            if (user != null&&address!=null)
            {
                address.PostalCode = model.PostalCode;
                address.Country = model.Country;
                address.City = model.City;
                address.AddressString = model.Address;

                await UserManager.UpdateAsync(user);
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
          
                storeDb.SaveChanges();

                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeAddressSuccess });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult GetAddress(string description)
        {
            ScaleModelsStoreEntities storeDb = new ScaleModelsStoreEntities();

            var userId = User.Identity.GetUserId();
            var address = storeDb.Addresses.SingleOrDefault(a => a.UserId == userId && a.ShortDescription == description);

            var viewModel = new UserInfoViewModel
            {
                PostalCode = address != null ? address.PostalCode : "",
                Country = address != null ? address.Country : "",
                City = address != null ? address.City : "",
                Address = address != null ? address.AddressString : ""
            };

            return Json(viewModel);
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
            AddAddressSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}