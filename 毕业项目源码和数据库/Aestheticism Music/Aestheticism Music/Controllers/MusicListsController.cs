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
            int pageSize = 8;

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

        // 添加
        public ActionResult Create()
        {
            ViewBag.SingerID = new SelectList(db.Singer, "SingerID", "SingerName");
            ViewBag.UserID = new SelectList(db.UserMusic, "UserID", "UserName");
            return View();
        }

        // POST: MusicLists/Create
        // 添加
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusicList music, HttpPostedFileBase MusicListPath)
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
                        return Content("<script>alert('文件格式不正确！');history.go(-1)</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('文件大小不符合要求！');history.go(-1)</script>");
                }
            }
            else
            {
                return Content("<script>alert('请上传文件！');history;go(-1)</script>");
            }
            music.MusicListPath = MusicListPath.FileName.ToString();
            db.MusicList.Add(music);
            db.SaveChanges();
            return Content("<script>window.location.href='/MusicLists/Index/" + music.MusicListID + "'</script>");
        }

        //修改
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
        // 修改
        [HttpPost]
        public ActionResult Edit(/*int MusicListID, string MusicListName,string MusicListFileSize*/MusicList musicList, HttpPostedFileBase MusicListPath/*,string SingerName,string UserName*/)
        {

            //MusicList musicList = db.MusicList.Find(MusicListID);
            //Singer singer = db.Singer.SingleOrDefault(p=>p.SingerName==SingerName);
            //UserMusic user = db.UserMusic.SingleOrDefault(p=>p.UserName==UserName);
            ViewBag.singer = db.Singer.ToList();
            ViewBag.user = db.UserMusic.ToList();
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
                        musicList.MusicListPath = MusicListPath.FileName;
                        db.SaveChanges();
                    }
                    else
                    {
                        return Content("<script>alert('文件格式不正确！');history.go(-1)</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('文件大小不符合要求！');history.go(-1)</script>");
                }
            }
            else
            {
                return Content("<script>alert('请上传文件！');history.go(-1)</script>");
            }
            //musicList.MusicListPath = MusicListPath.FileName.ToString();
            //musicList.MusicListName = MusicListName;
            //musicList.MusicListFileSize = MusicListFileSize;
            //musicList.SingerID = singer.SingerID;
            //musicList.UserID = user.UserID;
            db.Entry(musicList).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
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
            List< MusicList> musicList = db.MusicList.Where(p=>p.MusicListID==id).ToList();
            if (musicList.Count>0)
            {
                return Content("<script>alert('请删除歌曲的绑定，再来删除歌曲哦！');history.back(-1)</script>");
            }
            else
            {
                var list = db.MusicList.Find(id);
                db.MusicList.Remove(list);
                db.SaveChanges();
                return Content("<script>alert('删除成功！');history.back(-1)</script>");
            }
            
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
