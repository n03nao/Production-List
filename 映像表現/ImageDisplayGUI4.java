package no8;



import java.awt.BorderLayout;	//Frame関係
import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Point;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;			//リスナーの設定に必要
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.image.BufferedImage;//画素にアクセスできる画像クラス
import java.io.File;				//画像をファイル名で読み込むときに必要
import java.io.IOException;			//画像読み込みのエラー処理

import javax.imageio.ImageIO;	//画像の読込
import javax.swing.BoxLayout;	//GUIのレイアウト
import javax.swing.JButton;	//ボタン
import javax.swing.JCheckBox;	//チェックボックス
import javax.swing.JFrame;	//JFrameクラス
import javax.swing.JLabel;	//ラベル
import javax.swing.JPanel;	//パネル（GUIなどの機能をまとめておくなど）
import javax.swing.JSpinner;	//スピナー(数字入力)
import javax.swing.SpinnerNumberModel;	//スピナーの設定
import javax.swing.event.ChangeEvent;	//スピナーの値が変わった時の設定
import javax.swing.event.ChangeListener;//スピナーの値の変化を受け取る

public class ImageDisplayGUI4 extends JPanel implements ActionListener, ChangeListener, MouseListener{
	BufferedImage imgIn = null;
	BufferedImage imgFo = null;
	BufferedImage imgCopy = null;
	BufferedImage imgST = null;

	Ar paneImg;		//JPanelを継承し、画像を表示するクラス
	ArST paneST;

	JButton bu1,bu2,bu3,bu4,bu5,bu6;	//ボタン
	JCheckBox check;	//チェックボタン
	JSpinner sp,sp2,sp3,sp4,sp5;		//スピナー
	JLabel label,label2,label3,label4,label5;		//ラベル。スピナー用
	int thres = 200;	//ハイライトを飛ばす処理で使う閾値
	int num = 2;
	int interval0 = 30;
	int radius0 = 5;
	int sk = 125;

	//ImageDisplayクラスのコンストラクタ。実行して、最初の一度だけよばれる
	ImageDisplayGUI4()throws IOException{
		//Panel：画像表示用とボタン用の2つのパネルを作る
		paneImg = new Ar();
		paneImg.addMouseListener(this);
		int w = imgIn.getWidth();//追加
		int h = imgIn.getHeight(); //追加
		paneST = new ArST(w,h);//追加

		JPanel paneGui = new JPanel();
		paneGui.setPreferredSize(new Dimension(512, 150));//追加
		//Button：グレイスケールやハイライトを行うボタンを作る
		bu1 = new JButton("GrayScale");
		bu2 = new JButton("Hifhlight");
		bu3 = new JButton("ALL");
		bu4 = new JButton("Binarization");
		bu5 = new JButton("Laplacian Filter");
		bu6 = new JButton("Screentone");
		paneGui.add(bu1);//作ったボタンはパネルに追加する
		paneGui.add(bu2);
		paneGui.add(bu3);
		paneGui.add(bu4);
		paneGui.add(bu5);
		paneGui.add(bu6);
		bu1.addActionListener(this);//リスナーに登録し、イベントを受け取る
		bu2.addActionListener(this);
		bu3.addActionListener(this);
		bu4.addActionListener(this);
		bu5.addActionListener(this);
		bu6.addActionListener(this);

		//Spinner
		//↓どのようなスピナーか設定。
		//↓引数は左から、初期値・最小値・最大値・ステップ幅。
		SpinnerNumberModel model = new SpinnerNumberModel(thres, 0, 255, 1);
		SpinnerNumberModel model2 = new SpinnerNumberModel(num, 0, 10, 1);
		SpinnerNumberModel model3 = new SpinnerNumberModel(interval0, 1, 50, 1);
		SpinnerNumberModel model4 = new SpinnerNumberModel(radius0, 1, 50, 1);
		SpinnerNumberModel model5 = new SpinnerNumberModel(sk, 0, 255, 1);
		sp = new JSpinner(model);
		sp2 = new JSpinner(model2);
		sp3 = new JSpinner(model3);
		sp4 = new JSpinner(model4);
		sp5 = new JSpinner(model5);

		sp.addChangeListener(this);//リスナーに登録し、イベントを受け取る
		sp2.addChangeListener(this);
		sp3.addChangeListener(this);
		sp4.addChangeListener(this);
		sp5.addChangeListener(this);

		label = new JLabel("Highlight/Threshold");//スピナーの横に説明用のラベルを作成
		label2 = new JLabel("K");
		label3 = new JLabel("Screentone/interval");
		label4 = new JLabel("Screentone/radius");
		label5 = new JLabel("Screentone/alpha");

		paneGui.add(label);//ラベルとスピナーをボタン用パネルに加える
		paneGui.add(sp);
		paneGui.add(label2);
		paneGui.add(sp2);
		paneGui.add(label3);
		paneGui.add(sp3);
		paneGui.add(label4);
		paneGui.add(sp4);
		paneGui.add(label5);
		paneGui.add(sp5);

		//checkBox
		check = new JCheckBox("composition");//上記のパターンと同じ
		paneGui.add(check);
		check.addActionListener(this);

		//Layout
		setLayout(new BoxLayout(this,BoxLayout.Y_AXIS));  //二つのパネルを縦に配置したい
		add(paneImg);   //Frameの上部に 画像用パネルを置く
		add(paneGui);   //下部にボタンを配置する

		setLayout(new BoxLayout(this,BoxLayout.Y_AXIS));  //二つのパネルを縦に配置したい
		add(paneImg);   //Frameの上部に 画像用パネルを置く
		add(paneGui);   //その下にボタンを配置する
		add(paneST);   //(追加)最下部にスクリーントーンの表示スペースを作る

	}
	//ボタンを押したときに呼び出されるメソッド
	public void actionPerformed(ActionEvent e)
	{
		if(e.getSource() == bu1){	//ボタン１が押されたら
			grayScale(imgIn);	//グレイスケール化メソッドを呼び出す
			imgCopy.setData(imgIn.getData());
			repaint();		//画像の再表示(ArクラスのpaintComponentなどpaint関連が呼び出される)
		}
		else if(e.getSource() == bu2){	//ボタン２が押されたら
			imgIn.setData(imgCopy.getData());
			highlight(imgIn, thres);
			repaint();
		}
		else if(e.getSource() == check){	//チェックボタンが押されたら
			repaint();
		}
		else if(e.getSource() == bu3){
			highlight(imgIn, thres);
			check.setSelected( true );
			repaint();
		}
		else if(e.getSource() == bu4){
			imgIn.setData(imgCopy.getData());
			Binarization(imgIn, num);
			repaint();
		}
		else if(e.getSource() == bu5){
			smoothFilter(imgIn);
			repaint();
		}else if(e.getSource() == bu6){
			makeScreentone(imgST, interval0,radius0,sk);
			repaint();
			//第2、3引数は今はとりあえず適当な数字で与えているが、あとから変数で定義して、スピナーで扱う
		}
	}
	//スピナーの値が変化したときに呼び出されるメソッド
	public void stateChanged(ChangeEvent e)
	{
		thres = (Integer) sp.getValue();	//スピナーの値を閾値として取得
		num = (Integer) sp2.getValue();
		interval0 = (Integer) sp3.getValue();
		radius0 = (Integer) sp4.getValue();
		sk = (Integer) sp5.getValue();
		if(e.getSource() == sp){
			imgIn.setData(imgCopy.getData());
			highlight(imgIn, thres);	//取得した閾値を使ってハイライトを飛ばす
		}
		else if(e.getSource() == sp2){
			imgIn.setData(imgCopy.getData());
			Binarization(imgIn, num);
		}
		else if(e.getSource() == sp3){
			int m_interval = (Integer) sp3.getValue();
			int m_radius = (Integer) sp4.getValue();
			int m_sk = (Integer) sp5.getValue();
			makeScreentone(imgST, m_interval, m_radius,m_sk);
		}
		else if(e.getSource() == sp4){
			//makeScreentone(imgIn, interval0,radius0);
			int m_interval = (Integer) sp3.getValue();
			int m_radius = (Integer) sp4.getValue();
			int m_sk = (Integer) sp5.getValue();
			makeScreentone(imgST, m_interval, m_radius,m_sk);
		}
		else if(e.getSource() == sp5){
			int m_interval = (Integer) sp3.getValue();
			int m_radius = (Integer) sp4.getValue();
			int m_sk = (Integer) sp5.getValue();
			makeScreentone(imgST, m_interval, m_radius,m_sk);
		}
		repaint();			//再表示
	}

	public static void main(String[] args) throws IOException {
		//フレームの作成
		JFrame frame = new JFrame("main window");	//メインのウィンドウを作る。""内はウィンドウタイトル
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);  //×ボタンでW終われるようにする。

		ImageDisplayGUI4 f = new ImageDisplayGUI4();
		frame.add(f,BorderLayout.CENTER);
		frame.pack();
		frame.setVisible(true);
	}

	//K値化するメソッド。
	private void Binarization(BufferedImage img, int num) {
		int w = img.getWidth();//基本的にmainからコピーしてくればよいが、
		int h = img.getHeight();//このメソッド内の全てのimgInをimgに変えることを忘れないように！！

		//画像を処理する
		int K = num;			//Kはfor文の外側で与える

		for(int i=0 ; i < w; i++){
			for(int j=0 ; j < h; j++){

				int c = img.getRGB(i, j);
				int r = c>>16	&0xff;

				//ここでif文の分岐をforとifで置き換え
				for(int l= 0 ; l < K; l++){
					if( r <= 255 / K *(l+1) )
					{
						int col = (int)( 255 / (K-1)*l );//キャストがいることに注意
						int rgb =  255 <<24 | col <<16 | col <<8 | col;
						img.setRGB(i,j,rgb);

						break;
					}
				}
			}
		}
		}

	//ハイライトを飛ばすメソッド。
	private static void highlight(BufferedImage img, int thre){
		//前回やったので、自分で内容をコピーする。
		//ただし、閾値にthreを使って書き換えること。

		int w = img.getWidth();//基本的にmainからコピーしてくればよいが、
		int h = img.getHeight();//このメソッド内の全てのimgInをimgに変えることを忘れないように！！


		for(int j = 0 ; j < h ; j++)	//縦横の2重for文で各画素にアクセス
			for(int i = 0 ; i < w ; i++)
			{
				int c = img.getRGB(i, j);	//j行i列のRGB成分を取り出す①
				int r =c>>16	&0xff;		//RGB成分からRだけを取り出す②
				int g = c>>8	&0xff;		//RGB成分からGだけを取り出す②
				int b =  c	&0xff;		//RGB成分からBだけを取り出す②
				int ntsc = (int)(0.298912 * (double)r +
						  0.586611 * (double)g +
						  0.114478 * (double)b);
				if(ntsc >thre){
					ntsc = 255;
				}
				int rgb =  255 <<24 | ntsc <<16 | ntsc <<8 | ntsc;	//r,g,b各成分をRGB成分として一つにまとめる③
				img.setRGB(i,j,rgb);	//j行i列の画素をrgb色に設定する④
			}
	}
	//グレイスケール化メソッド。
	private static void grayScale(BufferedImage img) {
		//前回やったので、自分で内容をコピーする。
		int w = img.getWidth();//基本的にmainからコピーしてくればよいが、
		int h = img.getHeight();//このメソッド内の全てのimgInをimgに変えることを忘れないように！！

		//画像を処理する
		for(int j = 0 ; j < h ; j++)	//縦横の2重for文で各画素にアクセス
		for(int i = 0 ; i < w ; i++)
		{
			int c = img.getRGB(i, j);	//j行i列のRGB成分を取り出す①
			int r =c>>16	&0xff;		//RGB成分からRだけを取り出す②
			int g = c>>8	&0xff;		//RGB成分からGだけを取り出す②
			int b =  c	&0xff;		//RGB成分からBだけを取り出す②
			int ntsc = (int)(0.298912 * (double)r +
					  0.586611 * (double)g +
					  0.114478 * (double)b);
	//		int ntsc = (r+g+b) /3;
			int rgb = 255 <<24 | ntsc <<16 | ntsc <<8 | ntsc;	//r,g,b各成分をRGB成分として一つにまとめる③
			img.setRGB(i,j,rgb);	//j行i列の画素をrgb色に設定する④
		}
	}
	private void smoothFilter(BufferedImage img) {

		//画像サイズ取得
		int w = img.getWidth();
		int h = img.getHeight();

		//結果をすぐに画像に代入してしまうと、あとの処理に影響があるので、
		//別に答えを入れる画像データを用意する
		BufferedImage imgFilter = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);

		//平滑化処理
		//端っこには隣がないので、0<=i<w, 0<=j<hではなく、1<=i<w-1, 1<=j<h-1で処理
		for(int i= 1 ; i < w-1 ; i++){
			for(int j= 1 ; j < h-1 ; j++)
			{
			//8近傍画素＋真ん中の画素の平均を求める
			//近傍探索のために更にfor文を使って平均値を求めよう
			//	int ave = 0;
				int b=0;

				for(int ii = i-1 ; ii <= i+1 ; ii++ ){
					for(int jj = j-1 ; jj <= j+1 ; jj++ )
					{
						int c = img.getRGB(ii, jj);
						int r = c>>16	&0xff;


					if(ii==i && jj==j){
						b +=  r * (-8);
					}else {b += r * 1;}


						//ave += r*(1.0f/9.0f);//(1/9)掛けて合計すると平均になる
					}
				}
				if(b >100){
					b = 0;
				}else {b = 255;}

				int rgb = b <<16 | b <<8 | b;
				imgFilter.setRGB(i,j,rgb);
			}
		}
		for(int s=1; s<w-1; s++){
			for(int t=1; t<h-1; t++){
				int c = imgFilter.getRGB(s, t);
				int r = c>>16	&0xff;
				if(r==0){
					int rgb = 255 << 24 | r <<16 | r <<8 | r;
				  img.setRGB(s,t,rgb);
				}

			}
		}
		//img.setData(imgFilter.getData());	//imgFilterをimgにコピー
	}
	private static void makeScreentone(BufferedImage img, int interval0, int radius0,int sk)
	{
		int w = img.getWidth();
		int h = img.getHeight();

		//スクリーントーン用の配列準備と初期化
		//BufferedImage imgST = new BufferedImage(w, h, BufferedImage.TYPE_INT_RGB);
		for(int i=0 ; i < w; i++)//真っ白に初期化
			for(int j=0 ; j < h; j++)
				img.setRGB(i, j, (255 <<16 | 255 <<8 | 255));

		//以下、最初の二つのfor文で一つの丸の場所を決めていく
		for( int j=0 ;j<h ; j+=interval0 )//←(a) ○ピクセル毎に0からhまで見ていく
		{
			int rr=0;//←(d)のヒント
			if(j%2==1){//○○が奇数（または偶数）のとき
				rr=interval0/2;//rrは以下の何処かで使う
			}

			for( int i=rr ; i<w  ;i+=interval0 ){//←(a) ○ピクセル毎に0からwまで見ていく
				//以下の2重for文では一つの丸を描く
				for(int ii = i - radius0 ; ii <= i + radius0; ii++){//←(b) Laplacianフィルタなどと似た処理
					for(int jj = j - radius0 ; jj <= j + radius0; jj++)
					{
						//(b) iiとjjが0より小さい、またはw,h以上の数にならないような処理をする
						if(ii <  0)	continue;
						if(ii >= w) 	continue;
						if(jj <  0)	continue;
						if(jj >= h) 	continue;

						//(c) どうやったら円が描けるでしょう？ヒントは距離
						double dist = Math.sqrt((double)((ii-i)*(ii-i) + (jj-j)*(jj-j)));
						if(dist < (double)(radius0))
						{
							int rgb =  sk << 24 | 0 <<16 | 0 <<8 | 0;
							img.setRGB(ii,jj,rgb);
						}
					}
				}

			}
		}
		//確認のためにimgInにコピーする
		//img.setData(imgST.getData());//imgは引数で、imgInを指定しているとして
	}

	//パネル　Ar の定義
	class Ar extends JPanel {

		Ar() throws IOException{
			//画像の読込
			String image = "nisi.png";
			imgIn = ImageIO.read(new File(image));
			imgFo = ImageIO.read(new File("focusLineAlpha.png"));
			System.out.println("imgFo"+imgFo.getType());
			int w = imgIn.getWidth();
			int h = imgIn.getHeight();
			imgCopy = ImageIO.read(new File(image));
//	    	imgCopy = new BufferedImage(w,h,imgIn.getType());
			grayScale(imgCopy);


			setBackground(Color.white);      		//背景色
			setPreferredSize(new Dimension(w,h));   //Frameのサイズ
		}
		public void paintComponent(Graphics g){ //repaintで呼び出される描画メソッド
			super.paintComponent(g);

			//変数の画像を座標(0,0)に表示
			g.drawImage(imgIn, 0, 0, this);		//imgInを描画
			if(check.isSelected())	//チェックボタンにチェックが入っている場合
				g.drawImage(imgFo, 0, 0, this);	//imgFoを描画す る
		}
	}
	class ArST extends JPanel {

		ArST(int w, int h) throws IOException{	//入力画像と同じサイズの模様を作りたいので、画像サイズをコンストラクタの引数にする
			//画像の読込
			imgST = new BufferedImage(w, h, BufferedImage.TYPE_INT_ARGB);	//Arクラスと同じように表示する画像を読み込む

			setBackground(Color.white);    		//背景色
			setPreferredSize(new Dimension(w,100));	//パネルのサイズ。フルサイズでなくても模様が確認できればよいので、少し小さめにしている
		}
		public void paintComponent(Graphics g){ //repaintで呼び出される描画メソッド
			super.paintComponent(g);

			//変数の画像を座標(0,0)に表示
			g.drawImage(imgST, 0, 0, this);		//imgSTを描画
		}
	}
	@Override
	public void mouseClicked(MouseEvent e) {
		System.out.println("mouseClicked");
	}
	@Override
	public void mousePressed(MouseEvent e) {
		System.out.println("mousePressed");
		screentoneMap(e.getPoint(), imgIn);
		repaint();
	}
	@Override
	public void mouseReleased(MouseEvent e) {
		System.out.println("mouseReleased");
	}
	@Override
	public void mouseEntered(MouseEvent e) {
		System.out.println("mouseEntered");
	}
	@Override
	public void mouseExited(MouseEvent e) {
		System.out.println("mouseExited");
	}

	private void screentoneMap(Point po, BufferedImage img) {
		int w = img.getWidth();
		int h = img.getHeight();
		int col = img.getRGB(po.x, po.y);//①
		System.out.println("img"+img.getType());

		System.out.println("imgST"+imgST.getType());
		for(int i=0 ; i < w ; i++)
			for(int j=0 ; j < h ; j++)
			{
				int col0 = imgIn.getRGB(i,j);		//②

				if(col == col0)//マウスでクリックした色と画像の色が同じとき、スクリーントーンをコピーする
				{
					int colST = imgST.getRGB(i,j);
					img.setRGB(i,j, colST);
				}
			}
	}

}

