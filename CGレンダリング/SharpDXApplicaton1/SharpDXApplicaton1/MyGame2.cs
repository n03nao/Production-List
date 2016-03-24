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
    class MyGame2 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        
        Model model;

        Matrix world;
        Matrix projection;
        Matrix view;

        float lightPsition1 = 0;
        float lightPsition2 = 0;

        float t = 0;



        KeyboardManager keyboardManager;

        public MyGame2()
        {
            
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 800;
            deviceManager.PreferredBackBufferHeight = 600;

            keyboardManager = new KeyboardManager(this);

        }

        
        void SetupLights(GameTime gameTime, Model model)
        {
            foreach (ModelMesh m in model.Meshes)
            {
                foreach (Effect e in m.Effects)
                {
                    BasicEffect effect = (BasicEffect)e;
                    KeyboardState ks = keyboardManager.GetState();


                    bool lightEnabledR = ks.IsKeyDown(Keys.Q);//キーボードのQを押すと赤光源が動く
                    bool lightEnabledG = ks.IsKeyDown(Keys.W);//キーボードのWを押すと緑光源が動く
                    bool lightEnabledB = ks.IsKeyDown(Keys.E);//キーボードのEを押すと青光源が動く

                    /*
                     //白光源
                     effect.LightingEnabled = true;
                     effect.DirectionalLight0.Enabled = true;
                     effect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
                     effect.DirectionalLight0.Direction = new Vector3(0, lightPsition1, lightPsition2);
                     */

                    //赤光源
                    effect.LightingEnabled = true;
                    //effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.Enabled = lightEnabledR;//キーボード
                    effect.DirectionalLight0.DiffuseColor = new Vector3(1, 0, 0);
                    effect.DirectionalLight0.Direction = new Vector3(0, lightPsition1, lightPsition2);//（）内は位置ではなく、向きを指定するので長さ１のベクトルを指定する

                    //緑光源
                    effect.LightingEnabled = true;
                    //effect.DirectionalLight1.Enabled = true;
                    effect.DirectionalLight1.Enabled = lightEnabledG;//キーボード
                    effect.DirectionalLight1.DiffuseColor = new Vector3(0, 1, 0);
                    effect.DirectionalLight1.Direction = new Vector3(lightPsition2, 0, lightPsition1);

                    //青光源
                    effect.LightingEnabled = true;
                    //effect.DirectionalLight2.Enabled = true;
                    effect.DirectionalLight2.Enabled = lightEnabledB;//キーボード
                    effect.DirectionalLight2.DiffuseColor = new Vector3(0, 0, 1);
                    effect.DirectionalLight2.Direction = new Vector3(lightPsition1, lightPsition2, 0);

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

            double time = gameTime.TotalGameTime.TotalSeconds;

            SetupLights(gameTime, model);
           

            //光源の移動
            t += 0.02f;       
            lightPsition1 = (float)Math.Cos(t);
            lightPsition2 = (float)Math.Sin(t);






            float aspectRatio = (float)GraphicsDevice.BackBuffer.Width
                                  / (float)GraphicsDevice.BackBuffer.Height;

            Vector3 cameraPosition = new Vector3(0.0f, 10.0f, 10.0f);


            world = Matrix.Identity;
            projection = Matrix.PerspectiveFovRH(
                          ToRadians(45.0f), aspectRatio, 1.0f, 1000.0f);//領域
            view = Matrix.LookAtRH(cameraPosition, Vector3.Zero, Vector3.Up);//カメラポジションから（0，0，0）を見る。Upはどこが上かを示している。


         



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
