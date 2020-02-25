using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspnetNote.MVC6.DataContext;
using AspnetNote.MVC6.Models;
using AspnetNote.MVC6.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetNote.MVC6.Controllers
{
    public class AccountController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 로그인
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                using (var db = new AspnetNoteDbContext())
                {
                    // Linq - 메서드 체이닝
                    // => : A Go to B
                    var user = db.Users.FirstOrDefault(u => u.UserId.Equals(model.UserId) &&
                                                        u.UserPassword.Equals(model.UserPassword));

                    if(user != null)
                    {
                        // 로그인 성공했을 때
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);

                        // 로그인 성공페이지로 이동
                        return RedirectToAction("LoginSuccess", "Home");                        
                    }                    
                }
                ModelState.AddModelError(string.Empty, "User ID or Password is wrong.");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// 회원가입
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 회원가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {
            if(ModelState.IsValid)
            {
                using(var db = new AspnetNoteDbContext())
                {
                    db.Users.Add(model);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
