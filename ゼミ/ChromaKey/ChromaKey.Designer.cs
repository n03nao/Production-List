namespace ChromaKey
{
	partial class ChromaKey
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.終了ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.フィルタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.グレースケール変換ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移動平均フィルタToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.クロマキー合成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pickedColorBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.nextFrameButton = new System.Windows.Forms.Button();
            this.frameLabel = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.fileOpenBackgroundPictureClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pickedColorBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPictureToolStripMenuItem,
            this.openVideoToolStripMenuItem,
            this.fileOpenBackgroundPictureClickToolStripMenuItem,
            this.savePictureToolStripMenuItem,
            this.saveVideoToolStripMenuItem,
            this.toolStripSeparator1,
            this.終了ToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // openPictureToolStripMenuItem
            // 
            this.openPictureToolStripMenuItem.Name = "openPictureToolStripMenuItem";
            this.openPictureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openPictureToolStripMenuItem.Text = "開く (画像)";
            this.openPictureToolStripMenuItem.Click += new System.EventHandler(this.File_OpenPictureClick);
            // 
            // openVideoToolStripMenuItem
            // 
            this.openVideoToolStripMenuItem.Name = "openVideoToolStripMenuItem";
            this.openVideoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openVideoToolStripMenuItem.Text = "開く (動画)";
            this.openVideoToolStripMenuItem.Click += new System.EventHandler(this.File_OpenVideoClick);
            // 
            // savePictureToolStripMenuItem
            // 
            this.savePictureToolStripMenuItem.Name = "savePictureToolStripMenuItem";
            this.savePictureToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.savePictureToolStripMenuItem.Text = "保存 (画像)";
            this.savePictureToolStripMenuItem.Click += new System.EventHandler(this.File_SavePictureClick);
            // 
            // saveVideoToolStripMenuItem
            // 
            this.saveVideoToolStripMenuItem.Name = "saveVideoToolStripMenuItem";
            this.saveVideoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveVideoToolStripMenuItem.Text = "保存 (動画)";
            this.saveVideoToolStripMenuItem.Click += new System.EventHandler(this.File_SaveVideoClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 終了ToolStripMenuItem
            // 
            this.終了ToolStripMenuItem.Name = "終了ToolStripMenuItem";
            this.終了ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.終了ToolStripMenuItem.Text = "終了";
            this.終了ToolStripMenuItem.Click += new System.EventHandler(this.File_ExitClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.フィルタToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(887, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip2";
            // 
            // フィルタToolStripMenuItem
            // 
            this.フィルタToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.グレースケール変換ToolStripMenuItem,
            this.移動平均フィルタToolStripMenuItem,
            this.クロマキー合成ToolStripMenuItem});
            this.フィルタToolStripMenuItem.Name = "フィルタToolStripMenuItem";
            this.フィルタToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.フィルタToolStripMenuItem.Text = "フィルタ";
            // 
            // グレースケール変換ToolStripMenuItem
            // 
            this.グレースケール変換ToolStripMenuItem.Name = "グレースケール変換ToolStripMenuItem";
            this.グレースケール変換ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.グレースケール変換ToolStripMenuItem.Text = "グレースケール変換";
            this.グレースケール変換ToolStripMenuItem.Click += new System.EventHandler(this.Filter_GrayScaleClick);
            // 
            // 移動平均フィルタToolStripMenuItem
            // 
            this.移動平均フィルタToolStripMenuItem.Name = "移動平均フィルタToolStripMenuItem";
            this.移動平均フィルタToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.移動平均フィルタToolStripMenuItem.Text = "移動平均フィルタ";
            this.移動平均フィルタToolStripMenuItem.Click += new System.EventHandler(this.Filter_MovingAverageClick);
            // 
            // クロマキー合成ToolStripMenuItem
            // 
            this.クロマキー合成ToolStripMenuItem.Name = "クロマキー合成ToolStripMenuItem";
            this.クロマキー合成ToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.クロマキー合成ToolStripMenuItem.Text = "クロマキー合成";
            this.クロマキー合成ToolStripMenuItem.Click += new System.EventHandler(this.Filter_ChromaKeyClick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(120, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(874, 740);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(211, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // pickedColorBox
            // 
            this.pickedColorBox.Location = new System.Drawing.Point(18, 101);
            this.pickedColorBox.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.pickedColorBox.Name = "pickedColorBox";
            this.pickedColorBox.Size = new System.Drawing.Size(86, 85);
            this.pickedColorBox.TabIndex = 3;
            this.pickedColorBox.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "G";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 76);
            this.label3.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "B";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(46, 30);
            this.textBox1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 19);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(46, 52);
            this.textBox2.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(48, 19);
            this.textBox2.TabIndex = 8;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(46, 76);
            this.textBox3.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(48, 19);
            this.textBox3.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 202);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "H";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(46, 199);
            this.textBox4.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(48, 19);
            this.textBox4.TabIndex = 11;
            // 
            // nextFrameButton
            // 
            this.nextFrameButton.Location = new System.Drawing.Point(19, 314);
            this.nextFrameButton.Name = "nextFrameButton";
            this.nextFrameButton.Size = new System.Drawing.Size(75, 23);
            this.nextFrameButton.TabIndex = 12;
            this.nextFrameButton.Text = "NextFrame";
            this.nextFrameButton.UseVisualStyleBackColor = true;
            this.nextFrameButton.Click += new System.EventHandler(this.nextFrameButton_Click);
            // 
            // frameLabel
            // 
            this.frameLabel.AutoSize = true;
            this.frameLabel.Location = new System.Drawing.Point(17, 400);
            this.frameLabel.Name = "frameLabel";
            this.frameLabel.Size = new System.Drawing.Size(67, 12);
            this.frameLabel.TabIndex = 13;
            this.frameLabel.Text = "0 / 0 Frame";
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(19, 343);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 14;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "S";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "V";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(46, 224);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(48, 19);
            this.textBox5.TabIndex = 1;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(46, 251);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(48, 19);
            this.textBox6.TabIndex = 1;
            // 
            // fileOpenBackgroundPictureClickToolStripMenuItem
            // 
            this.fileOpenBackgroundPictureClickToolStripMenuItem.Name = "fileOpenBackgroundPictureClickToolStripMenuItem";
            this.fileOpenBackgroundPictureClickToolStripMenuItem.Size = new System.Drawing.Size(270, 22);
            this.fileOpenBackgroundPictureClickToolStripMenuItem.Text = "背景(画像)";
            this.fileOpenBackgroundPictureClickToolStripMenuItem.Click += new System.EventHandler(this.fileOpenBackgroundPictureClick);
            // 
            // ChromaKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(887, 706);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.frameLabel);
            this.Controls.Add(this.nextFrameButton);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pickedColorBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ChromaKey";
            this.Text = "ChromaKey";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pickedColorBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openPictureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem savePictureToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem 終了ToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.PictureBox pickedColorBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.ToolStripMenuItem フィルタToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem グレースケール変換ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 移動平均フィルタToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem クロマキー合成ToolStripMenuItem;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.ToolStripMenuItem openVideoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveVideoToolStripMenuItem;
		private System.Windows.Forms.Button nextFrameButton;
		private System.Windows.Forms.Label frameLabel;
		private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.ToolStripMenuItem fileOpenBackgroundPictureClickToolStripMenuItem;
    }
}

