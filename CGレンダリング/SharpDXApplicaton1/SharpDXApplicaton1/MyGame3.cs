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

namespace SharpDXApplicaton1
{
    class MyGame3 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        
        Model model;

        Matrix world;
        Matrix projection;
        Matrix view;

 
        MouseManager mouseManager;

        MouseState prevMouseState;//追加

        float cameraThetaX, cameraThetaY, lightThetaX, lightThetaY;
        float bspos;


        public MyGame3()
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

                    MouseState ms = mouseManager.GetState();
                    float dx = ms.X - prevMouseState.X;
                    float dy = ms.Y - prevMouseState.Y;
                    //prevMouseState = ms; ←このメソッド内で更新してしまうと、
                    //全て終わった処理がUpdateメソッドに入り、カメラの操作で差分がない状態になってしまう。

                   
                    if (ms.LeftButton.Down)
                    {
                        lightThetaX += dx;
                        lightThetaY += dy;

                    }
                  
                    Vector3 pos = new Vector3(0, 0, 1);
                    Matrix rotyY = Matrix.RotationX(lightThetaY);
                    Matrix rotyX = Matrix.RotationY(lightThetaX);
                    pos = Vector3.TransformCoordinate(pos, rotyY);
                    pos = Vector3.TransformCoordinate(pos, rotyX);
                  

                    //白光源
                    effect.LightingEnabled = true;
                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
                    effect.DirectionalLight0.Direction = pos;
                 
                }
            }




        }
       

       
        float ToRadians(float degree)
        {
            return (degree / 180.0f) * (float)Math.PI;
        }

        protected override void Initialize()
        {
            // Modify the title of the window
            Window.Title = "MyGame1";
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
          
            Content.RootDirectory = "Content"; //追加
            spriteBatch = ToDisposeContent(new SpriteBatch(GraphicsDevice)); //追加
            spriteFont = Content.Load<SpriteFont>("Arial16"); //追加
         
            model = Content.Load<Model>("rings");

           

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);


            SetupLights(gameTime, model);//光の操作をした後にカメラの操作をしている。



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
           
            bspos += bs;//

            //課題1
            Vector3 pos = new Vector3(0, 0, 10 + bspos/100);
           
            Matrix rotyY = Matrix.RotationX(cameraThetaY);//x軸を中心に回転
            Matrix rotyX = Matrix.RotationY(cameraThetaX);//y軸を中心に回転
            pos = Vector3.TransformCoordinate(pos, rotyY);
            pos = Vector3.TransformCoordinate(pos, rotyX);//xを加えている
    

            

            view = Matrix.LookAtRH(pos, Vector3.Zero, Vector3.Up);
            

            float aspectRatio = (float)GraphicsDevice.BackBuffer.Width
                                  / (float)GraphicsDevice.BackBuffer.Height;

            // Vector3 cameraPosition = new Vector3(0.0f, 10.0f, 10.0f);


            world = Matrix.Identity;
            projection = Matrix.PerspectiveFovRH(
                          ToRadians(45.0f), aspectRatio, 1.0f, 1000.0f);//領域

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
           


            deviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //SetWireframe();
            model.Draw(GraphicsDevice, world, view, projection);

            base.Draw(gameTime);
        }
    }
}
