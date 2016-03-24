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
    class Game1 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        Vector2 DU, V0, S, Size;//2次元情報を格納する
        int width, height;
       
        

        public Game1()
        {
            
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 800;
            deviceManager.PreferredBackBufferHeight = 600;

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

            width = GraphicsDevice.BackBuffer.Width;//画面の横幅
            height = GraphicsDevice.BackBuffer.Height;//画面の縦幅  
            DU = new Vector2(0.0f, 20.0f);//DrawとUpdateの文の位置
            V0 = new Vector2(0.0f, 0.0f);//初期位置
            S = new Vector2(1.0f, 1.0f);//移動量
            Size = spriteFont.MeasureString("Hello SharpDX.Toolkit.");//文字列の幅と高さ（2次元情報）

          
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            upCount++;

            V0 += S;//xとyに移動量を足している

            if (V0.X <= 0 || V0.X >= width - Size.X ) {
                S.X *= -1;//反転
            }
            if (V0.Y <= 0 || V0.Y >= height - Size.Y)
            {
                S.Y *= -1;//反転
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {     
            drawCount++;

            deviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(); //追加
            spriteBatch.DrawString(spriteFont, "Hello SharpDX.Toolkit.", V0, Color.White); //V0の位置に追加
            spriteBatch.DrawString(spriteFont, "Update Called: " + upCount + "Dwaw Called:" + drawCount, DU, Color.White); //DUの位置に追加
            spriteBatch.End(); //追加

            base.Draw(gameTime);
        }
        int drawCount;
        int upCount;
    }
}
