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
using SkiaSharp;

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
            int width = 100;
            int height = 40;

            using var bitmap = new SKBitmap(width, height);
            using var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.White);

            // 设置字体样式
            var typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.BoldItalic);
            using var font = new SKFont(typeface, 24); // 替代 Paint.TextSize 和 Paint.Typeface

            // 创建文本图块
            var blob = SKTextBlob.Create(code, font);

            // 绘制文本
            using var paint = new SKPaint
            {
                Color = SKColors.DarkRed,
                IsAntialias = true
            };
            canvas.DrawText(blob, 5, 30, paint); // 推荐新写法

            // 输出 PNG
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            return data.ToArray();
        }
    }
}
