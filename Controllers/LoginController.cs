using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SampleApp.Enums;
using SampleApp.Helpers;
using SampleApp.Interface;
using SampleApp.ViewModels;
using System.Text;

namespace SampleApp.Controllers
{
    public class LoginController(IUsersLoginFacade usersLoginFacade, IMenuFacade menuFacade, IMapper mapper) : Controller
    {
        private readonly IUsersLoginFacade _usersLoginFacade = usersLoginFacade;
        private readonly IMenuFacade _menuFacade = menuFacade;
        private readonly IMapper _mapper = mapper;

        public ActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind("Username,Password")] UsersLoginDto usersLoginDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string condition = "RowStatus == @0 and Username == @1";
                    var parameters = new List<object> { (short)DBRowStatus.Active, usersLoginDto.Username };
                    var list = await _usersLoginFacade.GetByParameter(condition, parameters);
                    var userInDb = list.SingleOrDefault();

                    if (userInDb is null)
                        ViewBag.Message = "User tidak ditemukan.";
                    else
                    {
                        var hasher = new PasswordHasher<IdentityUser>();
                        IdentityUser identityUser = new(userInDb.Username);

                        if (hasher.VerifyHashedPassword(identityUser, userInDb.Password, usersLoginDto.Password) != PasswordVerificationResult.Success)
                            ViewBag.Message = "Username atau password salah.";
                        else
                        {
                            userInDb.LastLogin = DateTime.Now;
                            await _usersLoginFacade.Update(userInDb);
                            HttpContext.Session.Set(ConstantHelper.SESSION_USERID, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_mapper.Map<UsersLoginDto>(userInDb))));
                            HttpContext.Session.Set(ConstantHelper.SESSION_MENUS, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_mapper.Map<List<MenuDto>>(await _menuFacade.GetByUserID(userInDb.Id)))));
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Message = UtilityHelper.GetExceptionMessage(e);
                }
            }
            else
            {
                StringBuilder msg = new();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        msg.AppendLine(error.ErrorMessage);
                    }
                }

                ViewBag.Message = msg.ToString();
            }

            return View(usersLoginDto);
        }
    }
}
