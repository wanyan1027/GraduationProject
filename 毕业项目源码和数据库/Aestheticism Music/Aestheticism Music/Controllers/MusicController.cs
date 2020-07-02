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
    public class MusicController : Controller
    {
        AestheticismMSEntities db = new AestheticismMSEntities();
        // GET: Music
        public ActionResult Index(int id)
        {
            var list = db.MusicList.Include(m => m.Singer).Include(m => m.UserMusic);
            var dis = db.Discuss.Include(m => m.UserMusic).Include(m => m.MusicList).ToList();
            UserMusic user = db.UserMusic.Find(id);
            var singer = db.Singer.ToList();
            ViewBag.singer = singer;
            ViewBag.dis = dis;
            ViewBag.list = list;
            ViewBag.user = user;
            return View(list.ToList());
        }

        [HttpPost]
        public ActionResult Index(string MusicListName="")
        {
            List<MusicList> list = db.MusicList.Where(p => MusicListName == "" || p.MusicListName.Contains(MusicListName)).ToList();
            ViewBag.name = MusicListName;
            return View(list);
        }

        /// <summary>
        /// 添加歌曲
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            db.MusicList.ToList();
            return View();
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(UserMusic user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 转换为json对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Detail(int id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            //将tea对象转Json对象
            var json = Json(db.UserMusic.Find(id));
            return json;
        }

        public ActionResult Document()
        {
            return View();
        }
    }
}