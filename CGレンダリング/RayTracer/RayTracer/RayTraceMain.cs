using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace RayTracer
{
    class RayTraceMain : Form
    {
        private const int DEFAULT_WSIZE = 256;		//デフォルト描画領域サイズ

        private StreamReader inputfile = null;

        private Color backColor;							//背景色
        private PointLight pLight;							//点光源

        private List<Material> materials;					//マテリアルリスト
        private List<BaseShape> shapes;						//物体リスト

        private Bitmap raytraceImage;

        private Graphics graphics;

        private string dataFile;
        private int size;

        //コンストラクタ
        public RayTraceMain(String dataFile, int size)
        {
            this.dataFile = dataFile;
            this.size = size;
            this.raytraceImage = new Bitmap(size, size);

            materials = new List<Material>();
            shapes = new List<BaseShape>();

            Text = "RayTracer";
            Load += new EventHandler(RayTraceMain_Load);
            Paint += new PaintEventHandler(RayTraceMain_Paint);
            FormClosed += new FormClosedEventHandler(RayTraceMain_FormClosed);
            readSceneFile(dataFile);
            Application.Run(this);
        }

        void RayTraceMain_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(size, size);
            graphics = CreateGraphics();
            paintScreen();//描画
        }

        void RayTraceMain_Paint(object sender, PaintEventArgs e)
        {
            graphics.DrawImage(raytraceImage, 0, 0);
        }

        void RayTraceMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            raytraceImage.Dispose();
            graphics.Dispose();
        }

        static void Main(string[] args)
        {
            RayTraceMain paintwindow;

            if (args.Length > 1)	//ウインドウサイズが指定されている場合
            {
                try
                {
                    paintwindow = new RayTraceMain(args[0], Int32.Parse(args[1]));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("ウインドウサイズを整数で指定してください");
                    Environment.Exit(0);
                }
            }
            else if (args.Length > 0)	//ウインドウサイズが指定されていない場合デフォルトサイズでウインドウ生成
            {
                paintwindow = new RayTraceMain(args[0], DEFAULT_WSIZE);
            }
            else
            {
                Console.WriteLine("読み込むファイルを指定してください");
                Environment.Exit(0);
            }
        }

        public void paintScreen()
        {
            int xp, yp;
            int halfScreenWidth = size / 2;
            int halfScreenHeight = size / 2;
            long prevT;

            ViewPointRotate Z = new ViewPointRotate(/*Math.PI/4,Math.PI/4*/0, 0);//
            /*		Matrix Z = new Matrix(); //変換行列
                    Z.m11 = Math.cos(Math.PI/4);	Z.m12 = 0;	Z.m13 = 0;
                    Z.m21 = 0;			Z.m22 = 1;	Z.m23 = 0;
                    Z.m31 = 0;			Z.m32 = 0;	Z.m33 = Math.sin(Math.PI/4);
            */
            /*
                    //視点位置ベクトル
                    Vector3D E = new Vector3D(700, 0, 700);
                    E = Matrix.tVectorMatrix(E, Z);	//視点変換ベクトル行列積	
            */

            //描画開始時刻を保存
            prevT = DateTime.Now.Millisecond;

            //ブロックの大きさを指定
            int xb = 16;
            int yb = xb;

            for (int yh = 0; yh < size / yb; yh++)
            {
                for (int xw = 0; xw < size / xb; xw++)
                {

                    int yps = yh * yb; int ype = (yh + 1) * yb /*-1*/;
                    int xps = xw * xb; int xpe = (xw + 1) * xb /*-1*/;

                    for (yp = yps; yp < ype; yp++)
                    {
                        for (xp = xps; xp < xpe; xp++)
                        {
                            //視点位置ベクトル
                            Vector3D E = new Vector3D(0, 0, 700);
                            E = Z.RunRotate(E);

                            //視線ベクトルを計算
                            Vector3D V = new Vector3D(xp - halfScreenWidth,
                                                      -yp + halfScreenHeight, /*(-xp + halfScreenWidth)*/0);

                            V = Z.RunRotate(V);

                            V = Vector3D.normalize(Vector3D.sub(V, E));

                            int hitNumber = 0;
                            raytraceImage.SetPixel(xp, yp, backColor);

                            int hitObject = -1;
                            double t = 0;
                            double minT = Double.MaxValue;
                            BaseShape minTShape = null;

                            //交差判定
                            for (int i = 0; i < shapes.Count; i++)
                            {
                                //物体リストから一つ取り出す
                                BaseShape tmpShape = shapes[i];

                                //交差判定
                                t = tmpShape.calcT(E, V);

                                //物体と衝突している
                                if ((t >= 0) && (t < minT))
                                {
                                    //最も近い物体情報を更新
                                    minT = t;
                                    hitObject = i;
                                    minTShape = tmpShape;
                                }
                            }

                            //衝突した物体が存在する場合シェーディング計算
                            if (hitObject >= 0)
                            {
                                //交点計算
                                Vector3D hitPos = Vector3D.add(Vector3D.scale(minT, V), E);

                                Material usingMaterial = materials[minTShape.getUsingMaterialIndex()];
                                Color pcol = minTShape.calcShading(pLight, hitPos, E, usingMaterial);
                                raytraceImage.SetPixel(xp, yp, pcol);

                                hitNumber++;
                            }
                            else
                                raytraceImage.SetPixel(xp, yp, backColor);
                        }
                    }

                }
            }

            //描画時間表示
            Console.WriteLine(DateTime.Now.Millisecond - prevT + "[ms]");
        }

        //指定されたデータファイル（fileName）を読み込む
        public void readSceneFile(String fileName)
        {
            try
            {
                inputfile = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS"));

                //点光源データ読込み
                pLight = new PointLight(inputfile.ReadLine());

                //背景色データ読込み
                backColor = inputColor(inputfile.ReadLine());

                //マテリアル個数読込み
                int numMaterial = Int32.Parse(inputfile.ReadLine());

                //マテリアル読込み
                for (int i = 0; i < numMaterial; i++)
                    materials.Add(new Material(inputfile.ReadLine()));

                //Ellipse個数読込み
                int numEllipse = Int32.Parse(inputfile.ReadLine());

                //Ellipse読込み
                for (int i = 0; i < numEllipse; i++)
                    shapes.Add(new Ellipse(inputfile.ReadLine()));

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("指定されたファイル[" + fileName + "]が見つかりません");
                Environment.Exit(0);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                Environment.Exit(0);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(0);
            }
        }

        //色を文字列より読み込む 
        public Color inputColor(String data)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            try
            {
                string[] delimiter = { " " };
                string[] s = data.Split(delimiter, StringSplitOptions.None);	//スペースごとに文字列分割
                if (s.Length >= 3)
                {						//入力された数が3つ以上なら読み込み
                    r = Int32.Parse(s[0]) & 255;	//255以上の値入力を防止
                    g = Int32.Parse(s[1]) & 255;
                    b = Int32.Parse(s[2]) & 255;
                }
                else
                {
                    throw new ArgumentException("[raytracemain]入力値の個数が足りません:" + data);
                }
            }
            catch (ArgumentException e)
            {
                throw e;
            }

            return Color.FromArgb(r, g, b);
        }
    }
}
