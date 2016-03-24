using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Content;
using SharpDX.Toolkit.Input;
using SharpDXToolkitHelper; //追加

namespace SharpDXApplicaton1
{
    class MyGame6 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        
        Model model;

        Matrix [] world = new Matrix[20] ;
        Matrix mLocal; //追加
        Matrix projection;
        Matrix view;
        Vector3 p = new Vector3(0, 0, 5);
        List<Vector3> points = new List<Vector3>();//追加
       


        MouseManager mouseManager;

        MouseState prevMouseState;

        float cameraThetaX, cameraThetaY;
        float bspos;

        Grid3D grid; //追加

        Vector3 vG = new Vector3(0.0f, -9.8f, 0.0f); //加速度g
        
        Vector3 vX0 = new Vector3(0.0f, 5.0f, 0.0f); //初期位置x0
        Vector3 vV0 = new Vector3(5.0f, 0.0f, 0.0f); //初期速度v0

        KeyboardManager keyboardManager;


        public MyGame6()
        {
            
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 800;
            deviceManager.PreferredBackBufferHeight = 600;

            mouseManager = new MouseManager(this);
            keyboardManager = new KeyboardManager(this);

        }


        void SetupLights(GameTime gameTime, Model model)
        {
            foreach (ModelMesh m in model.Meshes)
            {
                foreach (Effect e in m.Effects)
                {
                    BasicEffect effect = (BasicEffect)e;
                    effect.EnableDefaultLighting();
                }
            }
        }
        Matrix CreateWorld(Vector3 vZ, Vector3 vY, Vector3 vP)
        {
            Matrix m;
            Vector3 vX = Vector3.Cross(vY, vZ);

            m.M11 = vX.X; m.M12 = vX.Y; m.M13 = vX.Z; m.M14 = 0.0f;
            m.M21 = vY.X; m.M22 = vY.Y; m.M23 = vY.Z; m.M24 = 0.0f;
            m.M31 = vZ.X; m.M32 = vZ.Y; m.M33 = vZ.Z; m.M34 = 0.0f;
            m.M41 = vP.X; m.M42 = vP.Y; m.M43 = vP.Z; m.M44 = 1.0f;
           
            return m;
        }



        float ToRadians(float degree)
        {
            return (degree / 180.0f) * (float)Math.PI;
        }

        protected override void Initialize()
        {
            // Modify the title of the window
            Window.Title = "MyGame5";
            IsMouseVisible = true;
            //points.Add(new Vector3(0, 0, -5.0f));//追加
            //points.Add(new Vector3(0, 0, 5.0f));//追加


            //t = 0;//追加

            base.Initialize();
        }

        protected override void LoadContent()
        {
          
            Content.RootDirectory = "Content"; //追加
            spriteBatch = ToDisposeContent(new SpriteBatch(GraphicsDevice)); //追加
            spriteFont = Content.Load<SpriteFont>("Arial16"); //追加
         
            model = Content.Load<Model>("box");
            grid = ToDisposeContent(new Grid3D(GraphicsDevice, 10, 10, 10, 10, 10)); //追加


            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);


            SetupLights(gameTime, model);

            KeyboardState ks = keyboardManager.GetState();


            bool stop = ks.IsKeyDown(Keys.W);//キーボードのWを押すと一時停止
            bool reset = ks.IsKeyDown(Keys.Q);//キーボードのQを押すとリセット

            Vector3 vZ = Vector3.UnitZ;
            Vector3 vY = Vector3.UnitY;
            Vector3 vP = new Vector3(0.0f, 0.0f, 0.0f);
            //float t = (float)gameTime.TotalGameTime.TotalSeconds;         
            

           
            //前フレームからの経過時間
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;


            //v = gt + v0　式(1)//抵抗なし
            //Vector3 v = vG * t + vV0;

            //v = g/k + (v0 - g/k)e^-kt//抵抗k有り
            float k = 0.5f;//抵抗値
            double e = Math.E;//eの値
            double e2 = Math.Pow(e, -k * t);//eの-kt乗（e^-kt）
            Vector3 v = vG / k + (vV0 - vG / k) * (float)e2;

            //x = ((v + v0) / 2) * t + x0 式(3)
            Vector3 x = ((v + vV0) / 2) * t + vX0;

            vP = (1.0f / 2.0f) * t * t * vG + t * vV0 + vX0;

            //反射
            if (vP.Y < 0)
            {
                v = Vector3.Reflect(v, Vector3.UnitY);//vを反射させる
                vP.Y = 0;//物体のy座標を0にする //ボールを床にめり込ませないため
            }

            mLocal = CreateWorld(vZ, vY, vP);

            if (stop == true)//止める
            {
                t = 0;//時間経過を0にする
            }
            else
            {
                vV0 = v;//vを前フレームの速度として保存
                vX0 = x;//xを前フレームの位置として保存
               // v = new Vector3(0, 0, 0);
                //x = vX0;

            }

            
            if (reset == true)//リセットする、最初の位置・速度に戻す
            {
                vV0 = new Vector3(5.0f, 0.0f, 0.0f); //初期速度v0
                vX0 = new Vector3(0.0f, 5.0f, 0.0f); //初期位置x0
               
            }

           


           



            //mouse操作
            MouseState ms = mouseManager.GetState();
            float dx = ms.X - prevMouseState.X;
            float dy = ms.Y - prevMouseState.Y;
            Window.Title = string.Format("x:{0:+0.00;-0.00} y:{1:+0.00;-0.00}", dx, dy);
            prevMouseState = ms;

            if (ms.RightButton.Down)
            {
                cameraThetaY += dy;
                cameraThetaX += dx;

                if (cameraThetaY > 1.5f)
                    cameraThetaY = 1.5f;
                else if (cameraThetaY < -1.5f)
                    cameraThetaY = -1.5f;
            }

            float bs = ms.WheelDelta;
           
            bspos += bs;

            Vector3 pos = new Vector3(10, 10, 10 + bspos/100);
           
            Matrix rotyY = Matrix.RotationX(cameraThetaY);//x軸を中心に回転
            Matrix rotyX = Matrix.RotationY(cameraThetaX);//y軸を中心に回転
            pos = Vector3.TransformCoordinate(pos, rotyY);
            pos = Vector3.TransformCoordinate(pos, rotyX);//xを加えている
   
            view = Matrix.LookAtRH(pos, Vector3.Zero, Vector3.Up);
            

            float aspectRatio = (float)GraphicsDevice.BackBuffer.Width
                                  / (float)GraphicsDevice.BackBuffer.Height;

            projection = Matrix.PerspectiveFovRH(
                          ToRadians(45.0f), aspectRatio, 1.0f, 1000.0f);//領域

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
           


            deviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            grid.Draw(Matrix.Identity, view, projection); //追加

            //SetWireframe();

            model.Draw(GraphicsDevice, mLocal, view, projection);
            
            


            base.Draw(gameTime);
        }
    }
}
