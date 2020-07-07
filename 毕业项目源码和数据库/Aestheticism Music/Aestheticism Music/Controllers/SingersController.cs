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
using PagedList.Mvc;

namespace Aestheticism_Music.Controllers
{
    public class SingersController : Controller
    {
        private AestheticismMSEntities db = new AestheticismMSEntities();

        // GET: Singers
        public ActionResult Index(int? page = null, string SingerName = "")
        {
            List<Singer> user = db.Singer.Where(p => SingerName == "" || p.SingerName.Contains(SingerName)).OrderByDescending(p => p.SingerID).ToList();
            //当前页码  
            // ?? 空合并运算符，用于为可为空的值类型和引用类型定义默认值。
            //如果不为空，则返回左侧操作数；否则返回右侧操作数。
            int pageNum = page ?? 1;

            //每页显示多少条
            int pageSize = 5;

            //通过ToPagedList扩展方法进行分页
            //参数：当前页、每页显示的页数
            IPagedList<Singer> userPagedList = user.ToPagedList(pageNum, pageSize);
            return View(userPagedList);
        }

        // GET: Singers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singer.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // GET: Singers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Singers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SingerID,SingerName,SingerSex,SingerPohoto")] Singer singer, HttpPostedFileBase file)
        {
            if (file != null)
            {
                //判断文件大小
                if (file.ContentLength > 0 && file.ContentLength < 4194304)
                {

                    //获取上传文件路径
                    string fileName = Path.GetFileName(file.FileName);
                    //获取文件后缀名【两种方式-------截取==jpg，，，GetExtension== .jpg】
                    //string suff = fileName.Substring(fileName.LastIndexOf(".")+1);
                    string suff = Path.GetExtension(fileName);


                    //判断文件格式
                    if (suff == ".gif" || suff == ".jpg" || suff == ".png")
                    {
                        //保存上传文件的路径
                        file.SaveAs(Server.MapPath("~/Content/img/" + file.FileName));
                        //获取上传文件名，用于后期拿图片
                        ViewBag.img = file.FileName;
                        //存入数据库字段中
                        singer.SingerPohoto = file.FileName;
                    }
                    else
                    {
                        return Content("<script>alert('文件格式不正确！');history.back(-1)</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('文件大小不符合要求！');history.back(-1)</script>");
                }
            }
            else
            {
                return Content("<script>alert('请上传文件！');history.back(-1)</script>");
            }
            singer.SingerPohoto = file.FileName.ToString();
                db.Singer.Add(singer);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: Singers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singer.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // POST: Singers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SingerID,SingerName,SingerSex,SingerPohoto")] Singer singer, HttpPostedFileBase file)
        {
            if (file != null)
            {
                //判断文件大小
                if (file.ContentLength > 0 && file.ContentLength < 4194304)
                {

                    //获取上传文件路径
                    string fileName = Path.GetFileName(file.FileName);
                    //获取文件后缀名【两种方式-------截取==jpg，，，GetExtension== .jpg】
                    //string suff = fileName.Substring(fileName.LastIndexOf(".")+1);
                    string suff = Path.GetExtension(fileName);


                    //判断文件格式
                    if (suff == ".gif" || suff == ".jpg" || suff == ".png")
                    {
                        //保存上传文件的路径
                        file.SaveAs(Server.MapPath("~/Content/img/" + file.FileName));
                        //获取上传文件名，用于后期拿图片
                        ViewBag.img = file.FileName;
                        //存入数据库字段中
                        singer.SingerPohoto = file.FileName;
                    }
                    else
                    {
                        return Content("<script>alert('文件格式不正确！');history.back(-1)</script>");
                    }
                }
                else
                {
                    return Content("<script>alert('文件大小不符合要求！');history.back(-1)</script>");
                }
            }
            else
            {
                return Content("<script>alert('请上传文件！');history.back(-1)</script>");
            }
            singer.SingerPohoto = file.FileName.ToString();
            db.Entry(singer).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Singers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Singer singer = db.Singer.Find(id);
            if (singer == null)
            {
                return HttpNotFound();
            }
            return View(singer);
        }

        // POST: Singers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List< Singer> singer  = db.Singer.Where(p=>p.SingerID==id).ToList();
            if (singer.Count>0)
            {
                return Content("<script>alert('歌手是不能删除的哦！');history.back(-1)</script>");
            }
            else
            {
                var singers = db.Singer.Find(id);
                db.Singer.Remove(singers);
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
