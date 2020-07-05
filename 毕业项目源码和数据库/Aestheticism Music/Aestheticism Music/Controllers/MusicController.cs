using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
            var list = db.MusicList.Include(m => m.Singer).Include(m => m.UserMusic).ToList();
            var dis = db.Discuss.Include(m => m.UserMusic).Include(m => m.MusicList).ToList();
            var singer = db.Singer.ToList();
            ViewBag.singer = singer;
            ViewBag.dis = dis;
            ViewBag.list = list;
            return View(list);
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(string MusicListName,string MusicListFileSize,string MusicListPath, string SingerName, string UserName, MusicList music)
        {
            Singer singer = db.Singer.SingleOrDefault(p=>p.SingerName==SingerName);
            UserMusic user = db.UserMusic.SingleOrDefault(p=>p.UserName==UserName);
            MusicList list = new MusicList()
            {
                MusicListName = MusicListName,
                MusicListFileSize=MusicListFileSize,
                MusicListPath=MusicListPath,
                SingerID=singer.SingerID,
                UserID=user.UserID
            };
            db.MusicList.Add(list);
            db.SaveChanges();

            ////音频文件的判断
            //HttpPostedFileBase file = Request.Files["MusicListPath"];
            //if (file != null)
            //{
            //    try
            //    {
            //        var filename = Path.Combine(Request.MapPath("~/Content/Music"), file.FileName);
            //        file.SaveAs(filename);
            //        return Content("<script>alert('上传成功！！');history.go(-1)</script>");
            //    }
            //    catch (Exception ex)
            //    {
            //        return Content(string.Format("上传文件出现异常：{0}", ex.Message));
            //    }

            //}
            //else
            //{
            //    return Content("没有文件需要上传！");
            //}
            return View();

        }


        /// <summary>
        /// 转换为json对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Detail(int? id)
        {
            db.Configuration.LazyLoadingEnabled = false;
            //将tea对象转json对象
            //session["users"] = db.usermusic.find(id);
            var json = Json(db.UserMusic.Find(id));
            return View(json);
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(string UserName,string UserLoginName,string UserLoginPwd,string UserSex, string UserPhone, string UserBirthday, string UserEmail,UserMusic user)
        {
            
            //db.SaveChanges();

            UserMusic UMusic = Session["User"] as UserMusic;
            UMusic.UserName = UserName;
            UMusic.UserLoginName = UserLoginName;
            UMusic.UserLoginPwd = UserLoginPwd;
            UMusic.UserSex = UserSex;
            UMusic.UserPhone = UserPhone;
            UMusic.UserBirthday = UserBirthday;
            UMusic.UserEmail = UserEmail;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Content("<script>window.location.href='/Music/Index/" + UMusic.UserID + "'</script>");
        }

        /// <summary>
        /// 播放页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Listen(int id)
        {
            MusicList list = db.MusicList.Find(id);
            ViewBag.list = list;
            return View(list);
        }

        /// <summary>
        /// 最新专辑播放页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Coffee()
        {
            List<MusicList> list = db.MusicList.ToList();
            ViewBag.list = list;
            return View(list);
        }

        /// <summary>
        /// 最新专辑播放器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ActionResult Two()
        {
            List<MusicList> list = db.MusicList.ToList();
            ViewBag.list = list;
            return View(list);
        }

        /// <summary>
        /// 最新专辑播放器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Room()
        {
            List<MusicList> list = db.MusicList.ToList();
            ViewBag.list = list;
            return View(list);
        }


        /// <summary>
        /// 最新专辑播放器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Fish()
        {
            List<MusicList> list = db.MusicList.ToList();
            ViewBag.list = list;
            return View(list);
        }
    }
}