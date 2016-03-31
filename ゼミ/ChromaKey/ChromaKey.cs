using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChromaKey.Converters;
using ChromaKey.Filters;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ChromaKey
{
	public partial class ChromaKey : Form
	{
		public ChromaKey()
		{
			InitializeComponent();
		}

		private Bitmap[] images;
        public Bitmap[] images2;
        private int videoCount;

		#region File
		//画像を開く
		private void File_OpenPictureClick(object sender, EventArgs e)
		{
			try
			{
				var dialog = new CommonOpenFileDialog
				{
					IsFolderPicker = false,
					DefaultDirectory = Application.StartupPath,
					EnsureReadOnly = false,
					AllowNonFileSystemItems = false,
					DefaultExtension = ".jpg",
					Title = "画像ファイルを選択してください",
					RestoreDirectory = true
				};

				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					images = new[] { new Bitmap(dialog.FileName) };
					pictureBox1.Image = images[0];
				}
			}
			catch
			{
				MessageBox.Show("画像ファイルを選択してください", "ファイル読み込みエラー");
			}
		}

		private void File_OpenVideoClick(object sender, EventArgs e)
		{
			try
			{
				var dialog = new CommonOpenFileDialog
				{
					IsFolderPicker = false,
					DefaultDirectory = Application.StartupPath,
					EnsureReadOnly = false,
					AllowNonFileSystemItems = false,
					DefaultExtension = ".avi",
					Title = "動画ファイルを選択してください",
					RestoreDirectory = true
				};

				if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
				{
					images = VideoConverter.ReadBitmaps(new FileInfo(dialog.FileName));
					pictureBox1.Image = images[(videoCount = 0)];
					frameLabel.Text = string.Format("0 / {0} Frame", images.Length);
				}
			}
			catch
			{
				MessageBox.Show("動画ファイルを選択してください", "ファイル読み込みエラー");
			}
		}

		//画像を保存する
		private void File_SavePictureClick(object sender, EventArgs e)
		{
			if (pictureBox1.Image == null)
			{
				MessageBox.Show("画像ファイルが読み込まれていません", "ファイル読み込みエラー");
				return;
			}
			var dialog = new CommonSaveFileDialog
			{
				DefaultExtension = ".jpg",
				RestoreDirectory = true,
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				pictureBox1.Image.Save(dialog.FileName);
			}
		}

		private void File_SaveVideoClick(object sender, EventArgs e)
		{
			if (images == null)
			{
				MessageBox.Show("動画ファイルが読み込まれていません", "ファイル読み込みエラー");
				return;
			}
			var dialog = new CommonSaveFileDialog
			{
				DefaultExtension = ".avi",
				RestoreDirectory = true,
			};
			if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
			{
				VideoConverter.WriteBitmapsToVideo(images, new FileInfo(dialog.FileName));
			}
		}

		//アプリケーションを終了する
		private void File_ExitClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion File

		partial void PictureClick(Color color);

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
			if (images == null) { return; }
			var color = images[videoCount].GetPixel(e.X, e.Y);

			PictureClick(color);

			var canvas = new Bitmap(pickedColorBox.Width, pickedColorBox.Height);
			using (var graphics = Graphics.FromImage(canvas))
			{
				graphics.Clear(color);
				pickedColorBox.Image = canvas;
			}
		}

		#region Filter
		//グレースケール変換
		partial void ApplyGrayScale();

		private void Filter_GrayScaleClick(object sender, EventArgs e)
		{
			if (images == null)
			{
				MessageBox.Show("ファイルが読み込まれていません", "ファイル読み込みエラー");
				return;
			}
			ApplyGrayScale();
		}

		//移動平均フィルタ
		partial void ApplyMovingAverage();

		private void Filter_MovingAverageClick(object sender, EventArgs e)
		{
			if (images == null)
			{
				MessageBox.Show("ファイルが読み込まれていません", "ファイル読み込みエラー");
				return;
			}
			ApplyMovingAverage();
		}

		partial void ApplyChromaKey();

		private void Filter_ChromaKeyClick(object sender, EventArgs e)
		{
			if (images == null)
			{
				MessageBox.Show("ファイルが読み込まれていません", "ファイル読み込みエラー");
				return;
			}
			ApplyChromaKey();
		}
		#endregion Filter

		private void nextFrameButton_Click(object sender, EventArgs e)
		{
			if (images == null || images.Length == 0) return;

			videoCount = (videoCount + 1) % images.Length;
			pictureBox1.Image = images[videoCount];
			frameLabel.Text = string.Format("{0} / {1} Frame", videoCount, images.Length - 1);
		}

		private async void playButton_Click(object sender, EventArgs e)
		{
			playButton.Enabled = false;
			var context = SynchronizationContext.Current;
			await Task.Run(() =>
			{
				do
				{
					context.Send(o =>
					{
						nextFrameButton_Click(sender, e);
					}, null);
					Thread.Sleep(33);
				} while (videoCount != 0);
			});
			playButton.Enabled = true;
		}

        //背景画像を開く
        private void fileOpenBackgroundPictureClick(object sender, EventArgs e)
        {
            try
            {
                var dialog = new CommonOpenFileDialog
                {
                    IsFolderPicker = false,
                    DefaultDirectory = Application.StartupPath,
                    EnsureReadOnly = false,
                    AllowNonFileSystemItems = false,
                    DefaultExtension = ".jpg",
                    Title = "背景画像ファイルを選択してください",
                    RestoreDirectory = true
                };

                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    images2 = new[] { new Bitmap(dialog.FileName) };
                    pictureBox1.Image = images2[0];
                }
            }
            catch
            {
                MessageBox.Show("背景画像ファイルを選択してください", "ファイル読み込みエラー");
            }
        }
    }
}
