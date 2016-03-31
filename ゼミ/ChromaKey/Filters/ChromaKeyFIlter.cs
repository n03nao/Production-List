using ChromaKey.Converters;
using System.Drawing;

namespace ChromaKey.Filters
{
	static class ChromaKeyFilter
	{
		public static Bitmap Filtering(Bitmap originalImage, Bitmap backgroundImage)
		{
			var width = originalImage.Width;
			var height = originalImage.Height;
			var convertImage = new Bitmap(originalImage);
            var bgImage = new Bitmap(backgroundImage);


            for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					//画素の色を取得
					var originalColor = originalImage.GetPixel(x, y);
                    var backgroundColor = backgroundImage.GetPixel(x, y);

                    int r = originalColor.R;
					int g = originalColor.G;
					int b = originalColor.B;

                    int h, s, v;
                    HSVConverter.RGBToHSV(r, g, b, out h, out s, out v);

                    //ここの条件分岐を考える
                    if (h > 200 && h < 250 && s > 30)
                    {
                        convertImage.SetPixel(x, y, backgroundColor);
                    }
                }
			}

			return convertImage;
		}
	}
}
