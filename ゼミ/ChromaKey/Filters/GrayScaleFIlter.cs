using System.Drawing;

namespace ChromaKey.Filters
{
	static class GrayScaleFilter
	{
		public static Bitmap Filtering(Bitmap originalImage)
		{
			var width = originalImage.Width;
			var height = originalImage.Height;
			var convertImage = new Bitmap(originalImage);

			for (var y = 0; y < height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					//画素の色を取得
					var originalColor = originalImage.GetPixel(x, y);

					int r = originalColor.R;
					int g = originalColor.G;
					int b = originalColor.B;

					var luminance = (r + g + b) / 3;

					var convertColor = Color.FromArgb(luminance, luminance, luminance);

					convertImage.SetPixel(x, y, convertColor);
				}
			}

			return convertImage;
		}
	}
}
