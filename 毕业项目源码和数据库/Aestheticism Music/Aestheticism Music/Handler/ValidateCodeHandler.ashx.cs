using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace CodeFirstSystem1.Handler
{
    /// <summary>
    /// ValidateCodeHandler 的摘要说明
    /// </summary>
    public class ValidateCodeHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 主体方法用来生成验证码
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //定义多种颜色
            Color[] color = {Color.AliceBlue,
                Color.Pink,
                Color.Purple,
                Color.Black,
                Color.LightBlue,
                Color.LightGray,
                Color.LightCyan,
                Color.LightSkyBlue,
                Color.LightGreen,
                Color.Brown,
                Color.LightSteelBlue,
                Color.SandyBrown,
                Color.White
            };
            //设置画布，长100px 宽36px
            Image image = new Bitmap(120, 45);
            //将画布变成可绘制
            Graphics graphics = Graphics.FromImage(image);
            //设置画布的背景颜色
            graphics.Clear(Color.FromArgb(160, 102, 211));
            //准备随机数
            Random random = new Random(DateTime.Now.Millisecond);
            //生成四个字符，97-122小写字母 65-90大写字母 48-57是数字
            int Num1 = random.Next(97, 122);
            int Num2 = random.Next(65, 90);
            int Num3 = random.Next(97, 122);
            int Num4 = random.Next(48, 57);

            //将四个随机字符拼成一串验证码
            string validateCode = string.Format("{0}{1}{2}{3}", (char)Num1, (char)Num2, (char)Num3, Num4 - 48);
            //将验证码存入到Session中，用于登录判断
            context.Session["validateCode"] = validateCode;
            //写入画布字符
            Font font = new Font("微软雅黑", 24);
            //定义画笔，设置画笔的颜色
            //第一支画笔
            Brush brush1 = new SolidBrush(color[random.Next(0, color.Length - 1)]);
            graphics.DrawString(((char)Num1).ToString(), font, brush1, 5, -1);
            //第二支画笔
            Brush brush2 = new SolidBrush(color[random.Next(0, color.Length - 1)]);
            graphics.DrawString(((char)Num2).ToString(), font, brush2, 30, 4);
            //第三支画笔
            Brush brush3 = new SolidBrush(color[random.Next(0, color.Length - 1)]);
            graphics.DrawString(((char)Num3).ToString(), font, brush3, 60, 0);
            //第四支画笔
            Brush brush4 = new SolidBrush(color[random.Next(0, color.Length - 1)]);
            graphics.DrawString(((char)Num4).ToString(), font, brush4, 70, 8);
            //解析为图片类型
            context.Response.ContentType = "image/jpeg";
            //保存图片，图片流的格式输出出去
            image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            //释放图片资源
            image.Dispose();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}