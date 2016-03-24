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
using BulletSharp;

namespace SharpDXApplicaton1
{
    class MyGame7 : Game
    {
        GraphicsDeviceManager deviceManager;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        
        Model model, model1;
        //Model [] models = new Model[9];

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

        /*Vector3 vG = new Vector3(0.0f, -9.8f, 0.0f); //加速度g        
        Vector3 vX0 = new Vector3(0.0f, 5.0f, 0.0f); //初期位置x0
        Vector3 vV0 = new Vector3(5.0f, 0.0f, 0.0f); //初期速度v0
        */

        KeyboardManager keyboardManager;

        DbvtBroadphase broadphase;
        DefaultCollisionConfiguration collisionConfiguration;
        CollisionDispatcher dispatcher;
        SequentialImpulseConstraintSolver solver;
        DiscreteDynamicsWorld dynamicsWorld;

        CollisionShape groundShape;
        DefaultMotionState groundMotionState;
        RigidBodyConstructionInfo groundRigidBodyCI;
        RigidBody groundRigidBody;

        CollisionShape fallShape;
        DefaultMotionState fallMotionState;
        RigidBodyConstructionInfo fallRigidBodyCI;
        RigidBody fallRigidBody;

        CollisionShape boxShape;
        DefaultMotionState [] boxMotionState = new DefaultMotionState[10];
        RigidBodyConstructionInfo  boxRigidBodyCI;
        RigidBody [] boxRigidBody = new RigidBody[10];

        public MyGame7()
        {
            
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 800;
            deviceManager.PreferredBackBufferHeight = 600;

            mouseManager = new MouseManager(this);
            keyboardManager = new KeyboardManager(this);

        }

        protected override void UnloadContent()
        {
            for (int i=0; i<10; i++) {
                //box
                dynamicsWorld.RemoveRigidBody(boxRigidBody[i]);
                boxMotionState[i].Dispose();
                boxRigidBody[i].Dispose();
                boxShape.Dispose();
            }

            //fall
            dynamicsWorld.RemoveRigidBody(fallRigidBody);
            fallMotionState.Dispose();
            fallRigidBody.Dispose();
            fallShape.Dispose();

            dynamicsWorld.RemoveRigidBody(groundRigidBody);
            groundMotionState.Dispose();
            groundRigidBody.Dispose();
            groundShape.Dispose();

            dynamicsWorld.Dispose();
            solver.Dispose();
            dispatcher.Dispose();
            collisionConfiguration.Dispose();
            broadphase.Dispose();

            base.UnloadContent();
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
            Window.Title = "MyGame7";
            IsMouseVisible = true;
            

            //t = 0;//追加

            base.Initialize();
        }

        protected override void LoadContent()
        {
          
            Content.RootDirectory = "Content"; //追加
            spriteBatch = ToDisposeContent(new SpriteBatch(GraphicsDevice)); //追加
            spriteFont = Content.Load<SpriteFont>("Arial16"); //追加
         
            model = Content.Load<Model>("sphere");
            
            model1 = Content.Load<Model>("block");
           
            
            grid = ToDisposeContent(new Grid3D(GraphicsDevice, 10, 10, 10, 10, 10)); //追加

            //空間の初期化
            broadphase = new DbvtBroadphase();
            collisionConfiguration = new DefaultCollisionConfiguration();
            dispatcher = new CollisionDispatcher(collisionConfiguration);
            solver = new SequentialImpulseConstraintSolver();
            dynamicsWorld = new DiscreteDynamicsWorld(dispatcher, broadphase, solver, collisionConfiguration);
            dynamicsWorld.Gravity = new Vector3(0, -10, 0);

            //床の生成 
            groundShape = new StaticPlaneShape(new Vector3(0, 1, 0), 0); //形状の生成，平面なので法線と原点からの距離を指定
            groundMotionState = new DefaultMotionState(Matrix.Identity); //初期位置姿勢を設定
            groundRigidBodyCI = new RigidBodyConstructionInfo(0, groundMotionState, groundShape);
            groundRigidBody = new RigidBody(groundRigidBodyCI);
            groundRigidBody.Restitution = 0.9f; //反発係数を設定

            dynamicsWorld.AddRigidBody(groundRigidBody); //空間に追加

            //球の生成
            fallShape = new SphereShape(1); //形状の生成，半径1の球
            fallMotionState = new DefaultMotionState(Matrix.Translation(new Vector3(1, 10, -15))); //初期位置姿勢を設定
            float fallMass = 3; //質量を指定
            Vector3 fallInertia;
            fallShape.CalculateLocalInertia(fallMass, out fallInertia); //形状と質量から物体の慣性を計算する
            fallRigidBodyCI = new RigidBodyConstructionInfo(fallMass, fallMotionState, fallShape, fallInertia);
            fallRigidBody = new RigidBody(fallRigidBodyCI);
            fallRigidBody.Restitution = 0.5f; //反発係数を設定
            fallRigidBody.LinearVelocity = Vector3.UnitZ * 10;
            //fallRigidBody.AngularVelocity = Vector3.UnitX * 5;
            dynamicsWorld.AddRigidBody(fallRigidBody); //空間に追加

           
            for (int i=0; i<10; i++) {
                //直方体の生成
                boxShape = new BoxShape(1, 5, 1);
                boxMotionState[0] = new DefaultMotionState(Matrix.Translation(new Vector3(0, 5, 5))); //初期位置姿勢を設定
                boxMotionState[1] = new DefaultMotionState(Matrix.Translation(new Vector3(-2, 5, 7)));
                boxMotionState[2] = new DefaultMotionState(Matrix.Translation(new Vector3(2, 5, 7)));
                boxMotionState[3] = new DefaultMotionState(Matrix.Translation(new Vector3(-4, 5, 9)));
                boxMotionState[4] = new DefaultMotionState(Matrix.Translation(new Vector3(0, 5, 9)));
                boxMotionState[5] = new DefaultMotionState(Matrix.Translation(new Vector3(4, 5, 9)));
                boxMotionState[6] = new DefaultMotionState(Matrix.Translation(new Vector3(-6, 5, 11)));
                boxMotionState[7] = new DefaultMotionState(Matrix.Translation(new Vector3(-2, 5, 11)));
                boxMotionState[8] = new DefaultMotionState(Matrix.Translation(new Vector3(2, 5, 11)));
                boxMotionState[9] = new DefaultMotionState(Matrix.Translation(new Vector3(6, 5, 11)));

                float boxMass = 1; //質量を指定
                Vector3 boxInertia;
                boxShape.CalculateLocalInertia(boxMass, out boxInertia); //形状と質量から物体の慣性を計算する
                boxRigidBodyCI = new RigidBodyConstructionInfo(boxMass, boxMotionState[i], boxShape, boxInertia);
                boxRigidBody[i] = new RigidBody(boxRigidBodyCI);
                boxRigidBody[i].Restitution = 0.5f; //反発係数を設定
                dynamicsWorld.AddRigidBody(boxRigidBody[i]); //空間に追加
            }
           

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);


            SetupLights(gameTime, model);
     
            SetupLights(gameTime, model1);
       
            

            KeyboardState ks = keyboardManager.GetState();

            dynamicsWorld.StepSimulation((float)gameTime.ElapsedGameTime.TotalSeconds);

            //fallRigidBody.LinearVelocity = Vector3.UnitZ * 5;

            
            /*bool stop = ks.IsKeyDown(Keys.W);//キーボードのWを押すと一時停止
            bool reset = ks.IsKeyDown(Keys.Q);//キーボードのQを押すとリセット

            Vector3 vZ = Vector3.UnitZ;
            Vector3 vY = Vector3.UnitY;
            Vector3 vP = new Vector3(0.0f, 0.0f, 0.0f);
            //float t = (float)gameTime.TotalGameTime.TotalSeconds;         
            //vP = (1.0f / 2.0f) * t * t * vG + t * vV0 + vX0;

           
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

            //反射
            if (x.Y < 0)
            {
                v = Vector3.Reflect(v, Vector3.UnitY);//vを反射させる
                x.Y = 0;//物体のy座標を0にする //ボールを床にめり込ませないため
            }

            vP = x;
            
            if (stop == true)//止める
            {
                v = vV0;
                x = vX0;
            }
            else
            {
                vV0 = v;//vを前フレームの速度として保存
                vX0 = x;//xを前フレームの位置として保存
                v = new Vector3(0, 0, 0);
                x = vX0;

            }

            
            if (reset == true)//リセットする
            {
                vV0 = new Vector3(5.0f, 0.0f, 0.0f); //初期速度v0
                vX0 = new Vector3(0.0f, 5.0f, 0.0f); //初期位置x0
               
            }

            mLocal = CreateWorld(vZ, vY, vP);
            */


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

            Vector3 pos = new Vector3(20, 20, 20 + bspos/100);
           
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
            model.Draw(GraphicsDevice, fallRigidBody.MotionState.WorldTransform, view, projection);
            for (int i=0; i<10; i++) {
                model1.Draw(GraphicsDevice, boxRigidBody[i].MotionState.WorldTransform, view, projection);
            }
           
          
            

            //model.Draw(GraphicsDevice, mLocal, view, projection);




            base.Draw(gameTime);
        }
    }
}
