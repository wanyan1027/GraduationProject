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

namespace Aestheticism_Music.Controllers
{
    public class MusicListsController : Controller
    {
        private AestheticismMSEntities db = new AestheticismMSEntities();

        // GET: MusicLists
        public ActionResult Index(int?page=null, string MusicListName = "")
        {

            List<MusicList> music = db.MusicList.Where(p => MusicListName == "" || p.MusicListName.Contains(MusicListName)).OrderByDescending(p => p.MusicListID).ToList();

            //当前页码  
            // ?? 空合并运算符，用于为可为空的值类型和引用类型定义默认值。
            //如果不为空，则返回左侧操作数；否则返回右侧操作数。
            int pageNum = page ?? 1;

            //每页显示多少条
            int pageSize = 3;

            //通过ToPagedList扩展方法进行分页
            //参数：当前页、每页显示的页数
            IPagedList<MusicList> musicPagedList = music.ToPagedList(pageNum, pageSize);
            return View(musicPagedList);
        }

        //[HttpPost]
        //public ActionResult Index(string MusicListName="")
        //{
        //    List<MusicList> list = db.MusicList.Where(p => MusicListName == "" || p.MusicListName.Contains(MusicListName)).ToList();
        //    ViewBag.name = MusicListName;
        //    return View(list);
        //}

        // GET: MusicLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicList musicList = db.MusicList.Find(id);
            if (musicList == null)
            {
                return HttpNotFound();
            }
            return View(musicList);
        }

        // GET: MusicLists/Create
        public ActionResult Create()
        {
            ViewBag.SingerID = new SelectList(db.Singer, "SingerID", "SingerName");
            ViewBag.UserID = new SelectList(db.UserMusic, "UserID", "UserName");
            return View();
        }

        // POST: MusicLists/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MusicListID,MusicListName,MusicListFileSize,MusicListPath,SingerID,UserID")] MusicList musicList)
        {
            if (ModelState.IsValid)
            {
                db.MusicList.Add(musicList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SingerID = new SelectList(db.Singer, "SingerID", "SingerName", musicList.SingerID);
            ViewBag.UserID = new SelectList(db.UserMusic, "UserID", "UserName", musicList.UserID);
            return View(musicList);
        }

        // GET: MusicLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicList musicList = db.MusicList.Find(id);
            if (musicList == null)
            {
                return HttpNotFound();
            }
            ViewBag.SingerID = new SelectList(db.Singer, "SingerID", "SingerName", musicList.SingerID);
            ViewBag.UserID = new SelectList(db.UserMusic, "UserID", "UserName", musicList.UserID);
            return View(musicList);
        }

        // POST: MusicLists/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MusicListID,MusicListName,MusicListFileSize,MusicListPath,SingerID,UserID")] MusicList musicList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(musicList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SingerID = new SelectList(db.Singer, "SingerID", "SingerName", musicList.SingerID);
            ViewBag.UserID = new SelectList(db.UserMusic, "UserID", "UserName", musicList.UserID);
            return View(musicList);
        }

        // GET: MusicLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MusicList musicList = db.MusicList.Find(id);
            if (musicList == null)
            {
                return HttpNotFound();
            }
            return View(musicList);
        }

        // POST: MusicLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MusicList musicList = db.MusicList.Find(id);
            db.MusicList.Remove(musicList);
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
