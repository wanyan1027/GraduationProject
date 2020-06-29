using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aestheticism_Music.Models;

namespace Aestheticism_Music.Controllers
{
    public class UserMusicsController : Controller
    {
        private AestheticismMSEntities db = new AestheticismMSEntities();

        // GET: UserMusics

        public ActionResult Index()
        {
            return View(db.UserMusic.ToList());
        }

        [HttpPost]
        public ActionResult Index(string UserLoginName= "")
        {
           
               List<UserMusic> list = db.UserMusic.Where(p => UserLoginName == "" || p.UserLoginName.Contains(UserLoginName)).ToList();
                ViewBag.name = UserLoginName;
                return View(list);
        }

        // GET: UserMusics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMusic userMusic = db.UserMusic.Find(id);
            if (userMusic == null)
            {
                return HttpNotFound();
            }
            return View(userMusic);
        }

        // GET: UserMusics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserMusics/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserName,UserLoginName,UserLoginPwd,UserSongSheet,UserSex,UserPhone,UserBirthday,UserEmail")] UserMusic userMusic)
        {
            if (ModelState.IsValid)
            {
                db.UserMusic.Add(userMusic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userMusic);
        }

        // GET: UserMusics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMusic userMusic = db.UserMusic.Find(id);
            if (userMusic == null)
            {
                return HttpNotFound();
            }
            return View(userMusic);
        }

        // POST: UserMusics/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserName,UserLoginName,UserLoginPwd,UserSongSheet,UserSex,UserPhone,UserBirthday,UserEmail")] UserMusic userMusic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userMusic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userMusic);
        }

        // GET: UserMusics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMusic userMusic = db.UserMusic.Find(id);
            if (userMusic == null)
            {
                return HttpNotFound();
            }
            return View(userMusic);
        }

        // POST: UserMusics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserMusic userMusic = db.UserMusic.Find(id);
            db.UserMusic.Remove(userMusic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
