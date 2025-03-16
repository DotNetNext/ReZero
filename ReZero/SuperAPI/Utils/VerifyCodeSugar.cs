using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing.Imaging;
using System.Collections.Concurrent;

namespace ReZero.SuperAPI 
{

    /// <summary>
    /// ** 描述：验证码类
    /// ** 创始时间：2015-6-30
    /// ** 修改时间：-
    /// ** 修改人：sunkaixuan
    /// </summary>
    public class VerifyCodeSugar
    {
        private static readonly char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        private static readonly Random random = new Random();
        internal static (string,byte[]) Create()
        {
            string code = GenerateCode(4);
            byte[] imageBytes = GenerateImage(code);
            return (code,imageBytes);
        }

        private static string GenerateCode(int length)
        {
            StringBuilder sb = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[random.Next(chars.Length)]);
            }
            return sb.ToString();
        }

        private static byte[] GenerateImage(string code)
        {
            using Bitmap bitmap = new Bitmap(100, 40);
            using Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            using Font font = new Font("Arial", 20, FontStyle.Bold | FontStyle.Italic);
            using LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            g.DrawString(code, font, brush, 2, 2);
            using MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
