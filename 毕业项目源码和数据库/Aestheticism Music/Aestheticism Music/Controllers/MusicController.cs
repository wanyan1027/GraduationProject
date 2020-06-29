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
        public ActionResult Index()
        {
            var list = db.MusicList.Include(m => m.Singer).Include(m => m.UserMusic);
            var dis = db.Discuss.Include(m => m.UserMusic).Include(m => m.MusicList).ToList();
            var singer = db.Singer.ToList();
            ViewBag.singer = singer;
            ViewBag.dis = dis;
            ViewBag.list = list;
            return View(list.ToList());
        }

        [HttpPost]
        public ActionResult Index(string MusicListName="")
        {
            List<MusicList> list = db.MusicList.Where(p => MusicListName == "" || p.MusicListName.Contains(MusicListName)).ToList();
            ViewBag.name = MusicListName;
            return View(list);
        }

        public ActionResult Document()
        {
            return View();
        }
    }
}