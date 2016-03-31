using System.Linq;

namespace ChromaKey.Converters
{
	static class HSVConverter
	{
        /// <summary>
        /// RGBのint値をHSVにして各int変数に格納します。
        /// 入力値のint r, g, b;にそれぞれ値が入っていて、出力用のint h, s, v;が用意されているとき、
        /// HSVConverter.RGBToHSV(r, g, b, out h, out s, out v);
        /// のように書くことで関数内でh, s, vに代入された値が呼び出し側の変数に格納されます。
        /// </summary>
        /// <param name="r">入力のR値</param>
        /// <param name="g">入力のG値</param>
        /// <param name="b">入力のB値</param>
        /// <param name="h">出力のH変数</param>
        /// <param name="s">出力のS変数</param>
        /// <param name="v">出力のV変数</param>
        public static void RGBToHSV(int r, int g, int b, out int h, out int s, out int v)
        {
            var normalR = r / 255.0;
            var normalG = g / 255.0;
            var normalB = b / 255.0;

            var array = new[] { normalR, normalG, normalB };
            var max = array.Max();
            var min = array.Min();

            var normalS = max - min;
            var normalV = max;
            var congruenceH = 0.0;

            if (max == min)
            {
                congruenceH = 0;
            }
            else if (min == normalB)
            {
                congruenceH = 60 * (normalG - normalR) / normalS + 60;
            }
            else if (min == normalR)
            {
                congruenceH = 60 * (normalB - normalG) / normalS + 180;
            }
            else if (min == normalG)
            {
                congruenceH = 60 * (normalR - normalB) / normalS + 300;
            }

            h = (int)congruenceH;
            s = (int)(normalS * 255);
            v = (int)(normalV * 255);
        }
    }
}
