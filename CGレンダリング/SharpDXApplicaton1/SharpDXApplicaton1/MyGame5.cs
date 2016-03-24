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
    class MyGame5 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        
        Model model;

        Matrix [] world = new Matrix[20] ;
        Matrix mLocal; //追加
        Matrix projection;
        Matrix view;
        float theta = 0;
        Vector3 p = new Vector3(0, 0, 5);
        Matrix pX, pZ;
        float xt, zt, xv, zv, s;
        float t;//追加
        List<Vector3> points = new List<Vector3>();//追加
        int segment = 0;//追加


        MouseManager mouseManager;

        MouseState prevMouseState;

        float cameraThetaX, cameraThetaY;
        float bspos;

        Grid3D grid; //追加
      

        public MyGame5()
        {
            
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 800;
            deviceManager.PreferredBackBufferHeight = 600;

            points.Add(new Vector3(0.0f, 0, -5.0f));//P0
            points.Add(new Vector3(0.0f, 0, 10.0f));//P0の接線ベクトル
            points.Add(new Vector3(0.0f, 0, 5.0f));//P1
            points.Add(new Vector3(0.0f, 0, 10.0f));//P1の接線ベクトル

            points.Add(points[2]);//P1
            points.Add(points[3]);//P1の接線ベクトル
            points.Add(new Vector3(5.0f, 2.0f, 5.0f));//P2
            points.Add(new Vector3(0.0f, 0, -10.0f));//P2の接線ベクトル

            points.Add(points[6]);//P2
            points.Add(points[7]);//P2の接線ベクトル
            points.Add(new Vector3(0.0f, 4.0f, 0.0f));//P3
            points.Add(new Vector3(-10.0f, 0, 0.0f));//P3の接線ベクトル

            points.Add(points[10]);//P3
            points.Add(points[11]);//P3の接線ベクトル
            points.Add(new Vector3(-5.0f, 4.0f, 0.0f));//P4
            points.Add(new Vector3(-10.0f, 0, 0.0f));//P4の接線ベクトル

            mouseManager = new MouseManager(this);

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


            t = 0;//追加

            base.Initialize();
        }

        protected override void LoadContent()
        {
          
            Content.RootDirectory = "Content"; //追加
            spriteBatch = ToDisposeContent(new SpriteBatch(GraphicsDevice)); //追加
            spriteFont = Content.Load<SpriteFont>("Arial16"); //追加
         
            model = Content.Load<Model>("arrows");
            grid = ToDisposeContent(new Grid3D(GraphicsDevice, 10, 10, 10, 10, 10)); //追加


            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);


            SetupLights(gameTime, model);

            //課題
            float speedPerSeoncd = 1.0f;
            t += speedPerSeoncd * (float)gameTime.ElapsedGameTime.TotalSeconds;//道のり＝速さ×時間

            if (t > 1.0f)
            {
                segment++;

                if (segment >= (points.Count / 4))
                {
                    t = 0;
                    segment = 0;
                }
                else
                    t -= 1.0f;
            }


            
            Vector3 vY = Vector3.UnitY;
            int iseg = segment * 4;
            Vector3 vP = Vector3.Hermite(points[0 + iseg],
                                            points[1 + iseg],
                                            points[2 + iseg],
                                            points[3 + iseg],
                                            t);//現在のフレーム
            Vector3 vP2 = Vector3.Hermite(points[0 + iseg],
                                           points[1 + iseg],
                                           points[2 + iseg],
                                           points[3 + iseg],
                                           t-0.1f);//1フレーム前
            Vector3 line = vP - vP2;//接線
            Vector3 vZ = Vector3.Normalize(line);//正規化

            mLocal = Matrix.Identity;
            mLocal = CreateWorld(vZ, vY, vP);



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

            Vector3 pos = new Vector3(0, 0, 10 + bspos/100);
           
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
