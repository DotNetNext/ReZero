using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Text;
using System.Linq;

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
        private Random objRandom = new Random();

        #region setting

        /// <summary>
        /// //验证码长度
        /// </summary>
        public int SetLength = 4;
        /// <summary>
        /// 验证码字符串
        /// </summary>
        public string SetVerifyCodeText { get; set; }
        /// <summary>
        /// 是否加入小写字母
        /// </summary>
        public bool SetAddLowerLetter = false;
        /// <summary>
        /// 是否加入大写字母
        /// </summary>
        public bool SetAddUpperLetter = false;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int SetFontSize = 18;
        /// <summary>
        ///  //字体颜色
        /// </summary>
        public Color SetFontColor = Color.Blue;
        /// <summary>
        /// 字体类型
        /// </summary>
        public string SetFontFamily = "Verdana";
        /// <summary>
        /// 背景色
        /// </summary>
        public Color SetBackgroundColor = Color.AliceBlue;
        /// <summary>
        /// 前景噪点数量
        /// </summary>
        public int SetForeNoisePointCount = 2;
        /// <summary>
        /// 随机码的旋转角度
        /// </summary>
        public int SetRandomAngle = 40;

        /// <summary>
        /// 是否随机字体颜色
        /// </summary>
        public bool SetIsRandomColor = false;
        /// <summary>
        /// 图片宽度
        /// </summary>
        private int SetWith
        {
            get
            {
                return this.SetVerifyCodeText.Length * SetFontSize;
            }
        }
        /// <summary>
        /// 图片高度
        /// </summary>
        private int SetHeight
        {
            get
            {
                return Convert.ToInt32((60.0 / 100) * SetFontSize + SetFontSize);
            }
        }
        #endregion

        #region Constructor Method
        public VerifyCodeSugar()
        {
            this.GetVerifyCodeText();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 得到验证码字符串
        /// </summary>
        private void GetVerifyCodeText()
        {

            //没有外部输入验证码时随机生成
            if (String.IsNullOrEmpty(this.SetVerifyCodeText))
            {
                StringBuilder objStringBuilder = new StringBuilder();

                //加入数字1-9
                for (int i = 1; i <= 9; i++)
                {
                    objStringBuilder.Append(i.ToString());
                }

                //加入大写字母A-Z，不包括O
                if (this.SetAddUpperLetter)
                {
                    char temp = ' ';

                    for (int i = 0; i < 26; i++)
                    {
                        temp = Convert.ToChar(i + 65);

                        //如果生成的字母不是'O'
                        if (!temp.Equals('O'))
                        {
                            objStringBuilder.Append(temp);
                        }
                    }
                }

                //加入小写字母a-z，不包括o
                if (this.SetAddLowerLetter)
                {
                    char temp = ' ';

                    for (int i = 0; i < 26; i++)
                    {
                        temp = Convert.ToChar(i + 97);

                        //如果生成的字母不是'o'
                        if (!temp.Equals('o'))
                        {
                            objStringBuilder.Append(temp);
                        }
                    }
                }

                //生成验证码字符串
                {
                    int index = 0;

                    for (int i = 0; i < SetLength; i++)
                    {
                        index = objRandom.Next(0, objStringBuilder.Length);

                        this.SetVerifyCodeText += objStringBuilder[index];

                        objStringBuilder.Remove(index, 1);
                    }
                }
            }
        }

        /// <summary>
        /// 得到验证码图片
        /// </summary>
        private Bitmap GetVerifyCodeImage()
        {
            Bitmap result = null;

            //创建绘图
            result = new Bitmap(SetWith, SetHeight);

            using (Graphics objGraphics = Graphics.FromImage(result))
            {
                objGraphics.SmoothingMode = SmoothingMode.HighQuality;

                //清除整个绘图面并以指定背景色填充
                objGraphics.Clear(this.SetBackgroundColor);

                //创建画笔
                using (SolidBrush objSolidBrush = new SolidBrush(this.SetFontColor))
                {
                    this.AddForeNoisePoint(result);

                    this.AddBackgroundNoisePoint(result, objGraphics);

                    //文字居中
                    StringFormat objStringFormat = new StringFormat(StringFormatFlags.NoClip);

                    objStringFormat.Alignment = StringAlignment.Center;
                    objStringFormat.LineAlignment = StringAlignment.Center;

                    //字体样式
                    Font objFont = new Font(this.SetFontFamily,
                     objRandom.Next(this.SetFontSize - 3, this.SetFontSize), FontStyle.Regular);

                    //验证码旋转，防止机器识别
                    char[] chars = this.SetVerifyCodeText.ToCharArray();

                    for (int i = 0; i < chars.Length; i++)
                    {
                        //转动的度数
                        float angle = objRandom.Next(-this.SetRandomAngle, this.SetRandomAngle);

                        objGraphics.TranslateTransform(12, 12);
                        objGraphics.RotateTransform(angle);
                        objGraphics.DrawString(chars[i].ToString(),
                        objFont, objSolidBrush, -2, 2, objStringFormat);
                        objGraphics.RotateTransform(-angle);
                        objGraphics.TranslateTransform(2, -12);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 添加前景噪点
        /// </summary>
        /// <param name="objBitmap"></param>
        private void AddForeNoisePoint(Bitmap objBitmap)
        {
            for (int i = 0; i < objBitmap.Width * this.SetForeNoisePointCount; i++)
            {
                objBitmap.SetPixel(objRandom.Next(objBitmap.Width),
                objRandom.Next(objBitmap.Height), this.SetFontColor);
            }
        }

        /// <summary>
        /// 添加背景噪点
        /// </summary>
        /// <param name="objBitmap"></param>
        /// <param name="objGraphics"></param>
        private void AddBackgroundNoisePoint(Bitmap objBitmap, Graphics objGraphics)
        {
            using (Pen objPen = new Pen(Color.Azure, 0))
            {
                for (int i = 0; i < objBitmap.Width * 2; i++)
                {
                    objGraphics.DrawRectangle(objPen, objRandom.Next(objBitmap.Width),
                     objRandom.Next(objBitmap.Height), 1, 1);
                }
            }

        }

        /// <summary>
        /// 获取随机颜色
        /// </summary>
        /// <returns></returns>
        private Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            // 对于C#的随机数，没什么好说的
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            // 为了在白色背景上显示，尽量生成深色
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }
        #endregion

        #region Public Method 

        /// <summary>
        /// 获取问题
        /// </summary>
        /// <param name="questionList">默认数字加减验证</param>
        /// <returns></returns>
        public KeyValuePair<string, string> GetQuestion(Dictionary<string, string> questionList = null)
        {
            if (questionList == null)
            {
                questionList = new Dictionary<string, string>();
                var operArray = new string[] { "+", "*", "num" };
                var left = objRandom.Next(0, 10);
                var right = objRandom.Next(0, 10);
                var oper = operArray[objRandom.Next(0, operArray.Length)];
                if (oper == "+")
                {
                    string key = string.Format("{0}+{1}=?", left, right);
                    string val = (left + right).ToString();
                    questionList.Add(key, val);
                }
                else if (oper == "*")
                {
                    string key = string.Format("{0}×{1}=?", left, right);
                    string val = (left * right).ToString();
                    questionList.Add(key, val);
                }
                else
                {
                    var num = objRandom.Next(1000, 9999); ;
                    questionList.Add(num.ToString(), num.ToString());
                }
            }
            return questionList.ToList()[objRandom.Next(0, questionList.Count)];
        }
        #endregion
    }
}
