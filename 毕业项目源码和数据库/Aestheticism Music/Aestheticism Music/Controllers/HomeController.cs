using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aestheticism_Music.Models;

namespace Aestheticism_Music.Controllers
{
    public class HomeController : Controller
    {
        AestheticismMSEntities db = new AestheticismMSEntities();
        public ActionResult Index()
        {
            List<Administer> Alist = db.Administer.ToList();
            ViewBag.Alist = Alist;
            return View(Alist);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Exit()
        {
            //清空Session
            Session["user"] = null;
            //跳转到登录界面
            return View("Index");
        }
        /// <summary>
        /// 注销管理员
        /// </summary>
        /// <returns></returns>
        public ActionResult ExitAdmin()
        {
            //清空Session
            Session["admin"] = null;
            //跳转到登录界面
            return View("MangerLogin");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult Register(UserMusic user)
        {
            db.UserMusic.Add(user);
            db.Configuration.ValidateOnSaveEnabled = false;
            int count = db.SaveChanges();
            db.Configuration.ValidateOnSaveEnabled = true;
            return RedirectToAction("Login", "Home");
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserLoginName, string UserLoginPwd, string Code)
        {
            var user = db.UserMusic.SingleOrDefault(p=>p.UserLoginName==UserLoginName&&p.UserLoginPwd==UserLoginPwd);
            if (user != null/* && (Session["validateCode"].ToString().ToLower() == Code)*/)
            {
                Session["User"] = user;
                return Content("<script>window.location.href='/Music/Index/" + user.UserID + "'</script>");
            }
            else
            {
                return Content("<script>alert('账号密码或者验证码错误！');history.go(-1)</script>");
            }
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <returns></returns>
        public ActionResult MangerLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MangerLogin(string AdministerLoginName,string AdministerLoginPwd)
        {
            var admin = db.Administer.SingleOrDefault(p => p.AdministerLoginName == AdministerLoginName && p.AdministerLoginPwd == AdministerLoginPwd);
            Session["admin"] = admin;
            if (admin != null)
            {
                return RedirectToAction("Index", "Administers");
            }
            else
            {
                return Content("<script>alert('账号密码或者验证码错误！');history.go(-1)</script>");
            }
        }


    }
}