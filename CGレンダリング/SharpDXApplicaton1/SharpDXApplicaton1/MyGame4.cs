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
    class MyGame4 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        
        Model model;


        Matrix [] locals = new Matrix[20];//子ども達
        Matrix world; //親
        Matrix projection;
        Matrix view;
        float theta;
        float xt, zt, xv, zv, s;



        MouseManager mouseManager;

        MouseState prevMouseState;

        float cameraThetaX, cameraThetaY;
        float bspos;

        Grid3D grid; //追加
      

        public MyGame4()
        {
            
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 800;
            deviceManager.PreferredBackBufferHeight = 600;


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

        protected override void Initialize()//起動後1度だけ呼び出される
        {
            // Modify the title of the window
            Window.Title = "MyGame4";
            IsMouseVisible = true;

            //子ども20個の生成
            for (int j = 0; j < locals.Length; j++)
            {

                float r = 5;//半径
                //①角度の求め方
                //theta += 360/ 20;
                //double rad = ToRadians((float)theta);//ラジアンに変換

                //②
                theta += 2 * (float)Math.PI / 20;

                //位置の変化
                xt = r * (float)Math.Sin(theta);
                zt = r * (float)Math.Cos(theta);

                //向きの変化
                xv = (float)Math.Cos(theta);//sinを微分するとcos
                zv = -(float)Math.Sin(theta);//cosを微分すると-sin

                Vector3 vZ = new Vector3(xv, 0, zv);//向き
                Vector3 vY = Vector3.UnitY;
                Vector3 vP = new Vector3(xt, 0, zt);//位置

                
                locals[j] = CreateWorld(vZ, vY, vP);
            }

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
            

            s += 0.01f;//スピード
            world = Matrix.RotationY(s) * Matrix.RotationZ(s);//親の回転


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



            model.Draw(GraphicsDevice, world, view, projection);//親を描画 
            for (int i=0; i<locals.Length; i++) {
                model.Draw(GraphicsDevice, locals[i] * world, view, projection);//子ども達と親を描画
            }
            


            base.Draw(gameTime);
        }
    }
}
