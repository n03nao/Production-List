using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;

namespace ChromaKey.Converters
{
	class VideoConverter
	{
		/// <summary>
		/// 入力されたビデオをBitmapとしてすべてメモリ上に読み込みます。
		/// </summary>
		/// <param name="videoFile"></param>
		/// <returns></returns>
		public static Bitmap[] ReadBitmaps(FileInfo videoFile)
		{
			using (var cap = new VideoCapture(videoFile.FullName))
			{
				return ToFrame(cap).ToArray();
			}
		}

		private static IEnumerable<Bitmap> ToFrame(VideoCapture video)
		{
			while (true)
			{
				using (var frame = new Mat())
				{
					video.Read(frame);
					if (frame.Empty()) yield break;

					yield return frame.ToBitmap();
				}
			}
		}

		public static void WriteBitmapsToVideo(Bitmap[] bitmaps, FileInfo saveFile)
		{
			using (
				var writer = new VideoWriter(saveFile.FullName, FourCC.Default, 30,
					new OpenCvSharp.CPlusPlus.Size(bitmaps[0].Width, bitmaps[0].Height)))
			{
				foreach (var b in bitmaps)
				{
					using (var mat = b.ToMat())
					{
						writer.Write(mat);
					}
				}
			}
		}
	}
}
