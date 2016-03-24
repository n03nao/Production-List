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
        private const int DEFAULT_WSIZE = 256;		//�f�t�H���g�`��̈�T�C�Y

        private StreamReader inputfile = null;

        private Color backColor;							//�w�i�F
        private PointLight pLight;							//�_����

        private List<Material> materials;					//�}�e���A�����X�g
        private List<BaseShape> shapes;						//���̃��X�g

        private Bitmap raytraceImage;

        private Graphics graphics;

        private string dataFile;
        private int size;

        //�R���X�g���N�^
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
            paintScreen();//�`��
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

            if (args.Length > 1)	//�E�C���h�E�T�C�Y���w�肳��Ă���ꍇ
            {
                try
                {
                    paintwindow = new RayTraceMain(args[0], Int32.Parse(args[1]));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("�E�C���h�E�T�C�Y�𐮐��Ŏw�肵�Ă�������");
                    Environment.Exit(0);
                }
            }
            else if (args.Length > 0)	//�E�C���h�E�T�C�Y���w�肳��Ă��Ȃ��ꍇ�f�t�H���g�T�C�Y�ŃE�C���h�E����
            {
                paintwindow = new RayTraceMain(args[0], DEFAULT_WSIZE);
            }
            else
            {
                Console.WriteLine("�ǂݍ��ރt�@�C�����w�肵�Ă�������");
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
            /*		Matrix Z = new Matrix(); //�ϊ��s��
                    Z.m11 = Math.cos(Math.PI/4);	Z.m12 = 0;	Z.m13 = 0;
                    Z.m21 = 0;			Z.m22 = 1;	Z.m23 = 0;
                    Z.m31 = 0;			Z.m32 = 0;	Z.m33 = Math.sin(Math.PI/4);
            */
            /*
                    //���_�ʒu�x�N�g��
                    Vector3D E = new Vector3D(700, 0, 700);
                    E = Matrix.tVectorMatrix(E, Z);	//���_�ϊ��x�N�g���s���	
            */

            //�`��J�n������ۑ�
            prevT = DateTime.Now.Millisecond;

            //�u���b�N�̑傫�����w��
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
                            //���_�ʒu�x�N�g��
                            Vector3D E = new Vector3D(0, 0, 700);
                            E = Z.RunRotate(E);

                            //�����x�N�g�����v�Z
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

                            //��������
                            for (int i = 0; i < shapes.Count; i++)
                            {
                                //���̃��X�g�������o��
                                BaseShape tmpShape = shapes[i];

                                //��������
                                t = tmpShape.calcT(E, V);

                                //���̂ƏՓ˂��Ă���
                                if ((t >= 0) && (t < minT))
                                {
                                    //�ł��߂����̏����X�V
                                    minT = t;
                                    hitObject = i;
                                    minTShape = tmpShape;
                                }
                            }

                            //�Փ˂������̂����݂���ꍇ�V�F�[�f�B���O�v�Z
                            if (hitObject >= 0)
                            {
                                //��_�v�Z
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

            //�`�掞�ԕ\��
            Console.WriteLine(DateTime.Now.Millisecond - prevT + "[ms]");
        }

        //�w�肳�ꂽ�f�[�^�t�@�C���ifileName�j��ǂݍ���
        public void readSceneFile(String fileName)
        {
            try
            {
                inputfile = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS"));

                //�_�����f�[�^�Ǎ���
                pLight = new PointLight(inputfile.ReadLine());

                //�w�i�F�f�[�^�Ǎ���
                backColor = inputColor(inputfile.ReadLine());

                //�}�e���A�����Ǎ���
                int numMaterial = Int32.Parse(inputfile.ReadLine());

                //�}�e���A���Ǎ���
                for (int i = 0; i < numMaterial; i++)
                    materials.Add(new Material(inputfile.ReadLine()));

                //Ellipse���Ǎ���
                int numEllipse = Int32.Parse(inputfile.ReadLine());

                //Ellipse�Ǎ���
                for (int i = 0; i < numEllipse; i++)
                    shapes.Add(new Ellipse(inputfile.ReadLine()));

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("�w�肳�ꂽ�t�@�C��[" + fileName + "]��������܂���");
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

        //�F�𕶎�����ǂݍ��� 
        public Color inputColor(String data)
        {
            int r = 0;
            int g = 0;
            int b = 0;

            try
            {
                string[] delimiter = { " " };
                string[] s = data.Split(delimiter, StringSplitOptions.None);	//�X�y�[�X���Ƃɕ����񕪊�
                if (s.Length >= 3)
                {						//���͂��ꂽ����3�ȏ�Ȃ�ǂݍ���
                    r = Int32.Parse(s[0]) & 255;	//255�ȏ�̒l���͂�h�~
                    g = Int32.Parse(s[1]) & 255;
                    b = Int32.Parse(s[2]) & 255;
                }
                else
                {
                    throw new ArgumentException("[raytracemain]���͒l�̌�������܂���:" + data);
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
