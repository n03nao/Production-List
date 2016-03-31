using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChromaKey.Converters;
using ChromaKey.Filters;

namespace ChromaKey
{
	// ChromaKeyクラスから作らなければいけない部分だけ抜き出したもの
	partial class ChromaKey
	{
		/// <summary>
		/// 画像をクリックしたときに呼ばれる関数。
		/// 目的: RGBとHSVの値をtextBoxに表示する
		/// </summary>
		/// <param name="color">クリックされた場所の色</param>
		partial void PictureClick(Color color)
		{
			textBox1.Text = color.R.ToString();
			textBox2.Text = color.G.ToString();
			textBox3.Text = color.B.ToString();

			int h, s, v;
			HSVConverter.RGBToHSV(color.R, color.G, color.B, out h, out s, out v);
			
            textBox4.Text = h.ToString();
            textBox5.Text = s.ToString();
            textBox6.Text = v.ToString();

        }

        /// <summary>
        /// グレースケールフィルタのメニューをクリックしたときに呼ばれる関数
        /// 目的: すべての画像に適用させる
        /// </summary>
        partial void ApplyGrayScale()
		{
			images[videoCount] = GrayScaleFilter.Filtering(images[videoCount]);
			pictureBox1.Image = images[videoCount];
		}

		partial void ApplyMovingAverage()
		{
			images[videoCount] = MovingAverageFilter.Filtering(images[videoCount]);
			pictureBox1.Image = images[videoCount];
		}

		partial void ApplyChromaKey()
		{
            for (int i=0; i<images.Length; i++) {
                images[i] = ChromaKeyFilter.Filtering(images[i], images2[0]);
                Console.WriteLine("{0} frame", i);
                pictureBox1.Image = images[i];
            }
			
		}
	}
}
