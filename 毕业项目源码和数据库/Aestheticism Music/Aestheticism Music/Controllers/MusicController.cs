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
        public ActionResult Create( MusicList music,HttpPostedFileBase MusicListPath)
        {
            if (MusicListPath != null)
            {
                //判断文件大小
                if (MusicListPath.ContentLength > 0 && MusicListPath.ContentLength < 4194304)
                {

                    //获取上传文件路径
                    string fileName = Path.GetFileName(MusicListPath.FileName);
                    //获取文件后缀名【两种方式-------截取==jpg，，，GetExtension== .jpg】
                    //string suff = fileName.Substring(fileName.LastIndexOf(".")+1);
                    string suff = Path.GetExtension(fileName);


                    //判断文件格式
                    if (suff == ".mp3")
                    {
                        //保存上传文件的路径
                        MusicListPath.SaveAs(Server.MapPath("~/Content/Music/" + MusicListPath.FileName));
                        //获取上传文件名，用于后期拿图片
                        ViewBag.mp3 = MusicListPath.FileName;
                        //存入数据库字段中
                        music.MusicListPath = MusicListPath.FileName;
                    }
                    else
                    {
                        return Content("<script>alert('文件格式不正确！');history.back(-1)</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('文件格式不正确！');history.back(-1)</script>");
                }
            }
            else
            {
                return Content("<script>alert('文件格式不正确！');history.back(-1)</script>");
            }
            music.MusicListPath = MusicListPath.FileName.ToString();
            db.MusicList.Add(music);
            db.SaveChanges();
            return Content("<script>window.location.href='/Music/Index/" + music.MusicListID + "'</script>");

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

        /// <summary>
        /// 播放歌曲
        /// </summary>
        /// <returns></returns>
        public ActionResult Aduio()
        {
            return View();
        }
    }
}