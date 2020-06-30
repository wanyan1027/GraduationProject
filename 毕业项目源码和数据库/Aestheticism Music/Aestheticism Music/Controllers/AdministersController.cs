using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aestheticism_Music.Models;
using PagedList;
using PagedList.Mvc;

namespace Aestheticism_Music.Controllers
{
    public class AdministersController : Controller
    {
        private AestheticismMSEntities db = new AestheticismMSEntities();

        // GET: Administers
        public ActionResult Index(int?page=null)
        {
            List<Administer> admin = db.Administer.OrderByDescending(p => p.AdministerID).ToList();

            //当前页码  
            // ?? 空合并运算符，用于为可为空的值类型和引用类型定义默认值。
            //如果不为空，则返回左侧操作数；否则返回右侧操作数。
            int pageNum = page ?? 1;

            //每页显示多少条
            int pageSize = 3;

            //通过ToPagedList扩展方法进行分页
            //参数：当前页、每页显示的页数
            IPagedList<Administer> adminPagedList = admin.ToPagedList(pageNum, pageSize);
            return View(adminPagedList);
        }

        [HttpPost]
        public ActionResult Index(string AdministerName="")
        {
            List<Administer> list = db.Administer.Where(p => AdministerName == "" || p.AdministerName.Contains(AdministerName)).ToList();
            ViewBag.name = AdministerName;
            return View(list);
        }

        // GET: Administers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administer administer = db.Administer.Find(id);
            if (administer == null)
            {
                return HttpNotFound();
            }
            return View(administer);
        }


        public ActionResult Detail()
        {
            var admin = db.Administer.ToList();
            ViewBag.admin = admin;
            return View();
        }

        // GET: Administers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdministerID,AdministerName,AdministerLoginName,AdministerLoginPwd,AdministerPhone,AdministerEmail")] Administer administer)
        {
            if (ModelState.IsValid)
            {
                db.Administer.Add(administer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(administer);
        }

        // GET: Administers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administer administer = db.Administer.Find(id);
            if (administer == null)
            {
                return HttpNotFound();
            }
            return View(administer);
        }

        // POST: Administers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdministerID,AdministerName,AdministerLoginName,AdministerLoginPwd,AdministerPhone,AdministerEmail")] Administer administer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(administer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(administer);
        }

        // GET: Administers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administer administer = db.Administer.Find(id);
            if (administer == null)
            {
                return HttpNotFound();
            }
            return View(administer);
        }

        // POST: Administers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Administer administer = db.Administer.Find(id);
            db.Administer.Remove(administer);
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
