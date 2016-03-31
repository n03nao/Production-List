using System.Drawing;

namespace ChromaKey.Filters
{
	public static class MovingAverageFilter
	{
		public static Bitmap Filtering(Bitmap originalImage)
		{
			var width = originalImage.Width;
			var height = originalImage.Height;
			var margin = 1;
			var convertImage = new Bitmap(originalImage);

			for (var y = margin; y < height - margin; y++)
			{
				for (var x = margin; x < width - margin; x++)
				{
					var sumR = 0;
					var sumG = 0;
					var sumB = 0;
					for (var yy = -margin; yy <= margin; yy++)
					{
						for (var xx = -margin; xx <= margin; xx++)
						{
							var originalColor = originalImage.GetPixel(x + xx, y + yy);
							sumR += originalColor.R;
							sumG += originalColor.G;
							sumB += originalColor.B;
						}
					}
					var convertColor = Color.FromArgb(sumR / 9, sumG / 9, sumB / 9);
					convertImage.SetPixel(x, y, convertColor);
				}
			}

			return convertImage;
		}
	}
}
