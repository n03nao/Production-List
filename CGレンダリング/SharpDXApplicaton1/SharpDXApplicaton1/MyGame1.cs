using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Content;

namespace SharpDXApplicaton1
{
    class MyGame1 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        
        Model model;

        Matrix world;
        Matrix projection;
        Matrix view;

       
        public MyGame1()
        {
            
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 800;
            deviceManager.PreferredBackBufferHeight = 600;

        }

        void SetWireframe()
        {
            var rds = new SharpDX.Direct3D11.RasterizerStateDescription();
            rds.CullMode = SharpDX.Direct3D11.CullMode.Back;
            rds.FillMode = SharpDX.Direct3D11.FillMode.Wireframe;
            RasterizerState rs = RasterizerState.New(GraphicsDevice, rds);
            GraphicsDevice.SetRasterizerState(rs);
        }

        float ToRadians(float degree)
        {
            return (degree / 180.0f) * (float)Math.PI;
        }

        protected override void Initialize()
        {


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

            double time = gameTime.TotalGameTime.TotalSeconds;
            
            
            Vector3 cp = new Vector3(0.0f, 10.0f, 10.0f); //もしくはVector3.UnitZ;//カメラポジションの初期値
            Matrix m = Matrix.RotationY(ToRadians(0.0f + (float)time * 70));//timeだけだと遅いから70かけている
            Vector3 rotvz = Vector3.TransformCoordinate(cp, m);//cpの位置からmだけ動かす
            

            float aspectRatio = (float)GraphicsDevice.BackBuffer.Width
                                  / (float)GraphicsDevice.BackBuffer.Height;

            Vector3 cameraPosition =rotvz;//カメラポジションに移動分を代入
            

            world = Matrix.Identity;
            projection = Matrix.PerspectiveFovRH(
                          ToRadians(45.0f), aspectRatio, 1.0f, 1000.0f);//領域
            view = Matrix.LookAtRH(cameraPosition, Vector3.Zero, Vector3.Up);//カメラポジションから（0，0，0）を見る。Upはどこが上かを示している。


            /*　cameraPositionに直接変換をかけても動かない

           Vector3 cameraPosition = new Vector3(0.0f, 10.0f, 10.0f); //もしくはVector3.UnitZ;//カメラポジションの初期値
           Matrix m = Matrix.RotationY(ToRadians(0.0f + (float)time * 70));//timeだけだと遅いから70かけている
           Vector3 rotvz = Vector3.TransformCoordinate(cameraPosition, m);//cameraPositionの位置からmだけ動かす


           float aspectRatio = (float)GraphicsDevice.BackBuffer.Width
                                 / (float)GraphicsDevice.BackBuffer.Height; 

           world = Matrix.Identity;
           projection = Matrix.PerspectiveFovRH(
                         ToRadians(45.0f), aspectRatio, 1.0f, 1000.0f);
           view = Matrix.LookAtRH(cameraPosition, Vector3.Zero, Vector3.Up);//カメラポジションから（0，0，0）を見る。Upはどこが上かを示している。
           */




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
          
            deviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            SetWireframe();
            model.Draw(GraphicsDevice, world, view, projection);

            base.Draw(gameTime);
        }
        //int drawCount;
        //int upCount;
    }
}
