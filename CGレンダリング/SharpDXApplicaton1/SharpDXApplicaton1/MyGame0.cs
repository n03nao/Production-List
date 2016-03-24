namespace SharpDXApplicaton1
{
    // Use these namespaces here to override SharpDX.Direct3D11
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Graphics;
    using SharpDX.Toolkit.Input;
    using System.Collections.Generic;
    using SharpDXToolkitHelper;
    using SharpDX;
    using System;

    /// <summary>
    /// Simple MyGame1 game using SharpDX.Toolkit.
    /// </summary>
    public class MyGame0 : Game
    {
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        Model model;
        Model model1;
        Matrix mLocal;
        float time = 0;
        Matrix world;
        Matrix projection;
        Matrix view;
        KeyboardManager keyboardManager;
        MouseManager mouseManager;
        MouseState prevMouseState;//追加
        float cameraTheta;
        float cameraPhi;
        float lightTheta;
        float lightPhi;
        Grid3D grid;
        float t;//追加
        List<Vector3> points = new List<Vector3>();//追加
        int segment = 0;//追加
        Vector3 vG = new Vector3(0.0f, -9.8f, 0.0f); //加速度g
        Vector3 vV0 = new Vector3(5.0f, 0.0f, 0.0f); //初期速度v0
        Vector3 vX0 = new Vector3(0.0f, 5.0f, 0.0f); //初期位置x0
        Vector3 vvP = new Vector3(5.0f, 0.0f, 0.0f);//速度保存
        Vector3 vPP = new Vector3(0.0f, 5.0f, 0.0f);//位置保存
        Vector3 re = new Vector3(0.0f, 1.0f, 0.0f);

        /*DbvtBroadphase broadphase;
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
        DefaultMotionState boxMotionState;
        RigidBodyConstructionInfo boxRigidBodyCI;
        RigidBody boxRigidBody;

        private GraphicsDeviceManager graphicsDeviceManager;

        HingeConstraint hingeConstraint;*/



        /// <summary>
        /// Initializes a new instance of the <see cref="MyGame1" /> class.
        /// </summary>
        public MyGame0()
        {
            // Creates a graphics manager. This is mandatory.
            //graphicsDeviceManager = new GraphicsDeviceManager(this);

            // Setup the relative directory to the executable directory
            // for loading contents with the ContentManager
            Content.RootDirectory = "Content";
            mouseManager = new MouseManager(this);
            keyboardManager = new KeyboardManager(this);
        }

        protected override void Initialize()
        {
            // Modify the title of the window
            Window.Title = "MyGame1";
            IsMouseVisible = true;

            points.Add(new Vector3(0, 0, -5.0f));
            points.Add(new Vector3(0.0f, 0, 10.0f));
            points.Add(new Vector3(0.0f, 0, 5.0f));
            points.Add(new Vector3(0.0f, 0, 10.0f));
            points.Add(points[2]);
            points.Add(points[3]);
            points.Add(new Vector3(5.0f, 2.0f, 5.0f));
            points.Add(new Vector3(0.0f, 0, -10.0f));
            points.Add(points[6]);
            points.Add(points[7]);
            points.Add(new Vector3(0, 4.0f, 0));
            points.Add(new Vector3(-10.0f, 0, 0));
            points.Add(points[10]);
            points.Add(points[11]);
            points.Add(new Vector3(-5.0f, 4.0f, 0));
            points.Add(new Vector3(-10.0f, 0, 0));

            t = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            model = Content.Load<Model>("sphere");
            model1 = Content.Load<Model>("block");
            spriteBatch = ToDisposeContent(new SpriteBatch(GraphicsDevice)); //追加
            spriteFont = Content.Load<SpriteFont>("Arial16"); //追加
            grid = ToDisposeContent(new Grid3D(GraphicsDevice, 10, 10, 10, 10, 10));

            /*broadphase = new DbvtBroadphase();
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
            fallMotionState = new DefaultMotionState(Matrix.Translation(new Vector3(0, 1, -10))); //初期位置姿勢を設定
            float fallMass = 1; //質量を指定
            Vector3 fallInertia;
            fallShape.CalculateLocalInertia(fallMass, out fallInertia); //形状と質量から物体の慣性を計算する
            fallRigidBodyCI = new RigidBodyConstructionInfo(fallMass, fallMotionState, fallShape, fallInertia);
            fallRigidBody = new RigidBody(fallRigidBodyCI);
            fallRigidBody.Restitution = 0.5f; //反発係数を設定
            fallRigidBody.AngularVelocity = Vector3.UnitZ * 0;
            dynamicsWorld.AddRigidBody(fallRigidBody); //空間に追加

            boxShape = new BoxShape(1, 5, 1);
            //boxMotionState = new DefaultMotionState(Matrix.Translation(new Vector3(0, 5, 5))); //初期位置姿勢を設          
            boxMotionState = new DefaultMotionState(Matrix.RotationZ((float)Math.PI / 2) * Matrix.Translation(new Vector3(0, 1, 5))); //初期位置姿勢を設定
            float boxMass = 1; //質量を指定
            Vector3 boxInertia;
            boxShape.CalculateLocalInertia(boxMass, out boxInertia); //形状と質量から物体の慣性を計算する
            boxRigidBodyCI = new RigidBodyConstructionInfo(boxMass, boxMotionState, boxShape, boxInertia);
            boxRigidBody = new RigidBody(boxRigidBodyCI);
            boxRigidBody.Restitution = 0.5f; //反発係数を設定
            hingeConstraint = new HingeConstraint(boxRigidBody, 5 * Vector3.UnitY, Vector3.UnitX);
            dynamicsWorld.AddConstraint(hingeConstraint);
            dynamicsWorld.AddRigidBody(boxRigidBody); //空間に追加   */        
            base.LoadContent();
        }
         protected override void Update(GameTime gameTime)
        {
            //SetupLights(gameTime,model);
            SetupLights2(gameTime, model);
            //SetupLight(gameTime, model);
            float aspectRatio = (float)GraphicsDevice.BackBuffer.Width
                                  / (float)GraphicsDevice.BackBuffer.Height;

            time++;
            /*
            double cos = Math.Cos(time*Math.PI/180);
            double sin = Math.Sin(time*Math.PI/180);
            Vector3 cameraPosition = new Vector3(10.0f  (float)cos, 10.0f, 10.0f  (float)sin);
            */

            Vector3 cameraPosition = new Vector3(0.0f, 10.0f, 10.0f);

            projection = Matrix.PerspectiveFovRH(
                          ToRadians(45.0f), aspectRatio, 1.0f, 1000.0f);
            view = Matrix.LookAtRH(cameraPosition, Vector3.Zero, Vector3.Up);

            MouseState ms = mouseManager.GetState();
            float dx = ms.X - prevMouseState.X;
            float dy = ms.Y - prevMouseState.Y;
            Window.Title = string.Format("x:{0:+0.00;-0.00} y:{1:+0.00;-0.00}", dx, dy);

            //dynamicsWorld.StepSimulation((float)gameTime.ElapsedGameTime.TotalSeconds);


            if (ms.RightButton.Down)
            {
                cameraTheta += ms.Y - prevMouseState.Y;
                cameraPhi += ms.X - prevMouseState.X;
                if (cameraTheta > 1.5f)
                    cameraTheta = 1.5f;
                else if (cameraTheta < -1.5f)
                    cameraTheta = -1.5f;

                if (cameraPhi > 1.5f)
                    cameraPhi = 1.5f;
                else if (cameraPhi < -1.5f)
                    cameraPhi = -1.5f;
            }

            int wheel = ms.WheelDelta;
            Vector3 pos = new Vector3(15.0f, 10.0f, wheel / 120 + 40);
            Matrix roty = Matrix.RotationX(cameraTheta);
            Matrix rotx = Matrix.RotationY(cameraPhi);
            pos = Vector3.TransformCoordinate(pos, roty);
            pos = Vector3.TransformCoordinate(pos, rotx);
            view = Matrix.LookAtRH(pos, Vector3.Zero, Vector3.Up);

            double cos = Math.Cos(time * Math.PI / 180);
            double sin = Math.Sin(time * Math.PI / 180);

            /*アニメーション
                  float speedPerSeoncd = 1.0f;
                  t += speedPerSeoncd * (float)gameTime.ElapsedGameTime.TotalSeconds;

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

                  int iseg = segment * 4;

            
                  Vector3 vZ = Vector3.UnitZ;
                  Vector3 vY = Vector3.UnitY;
                  Vector3 vP = Vector3.Hermite(points[0 + iseg], points[1 + iseg], points[2 + iseg], points[3 + iseg], t);
                  */

            Vector3 vZ = Vector3.UnitZ;
            Vector3 vY = Vector3.UnitY;
            Vector3 vP = new Vector3(0.0f, 0.0f, 0.0f);

            /*
            Vector3 sessen = Vector3.Normalize(vP-vvP);
            vZ = sessen;
            */

            //前フレームからの経過時間
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //vP = (1.0f / 2.0f)  t  t  vG + t  vV0 + vX0;

            //v = gt + v0　式(1)
            Vector3 v = vG * t + vvP;

            //x = ((v + v0) / 2) * t + x0 式(3)
            Vector3 x = ((v + vvP) / 2) * t + vPP;

            //反射
            if (x.Y < 0)
            {
                v = Vector3.Reflect(v, re);
                x.Y = 0; //ボールを床にめり込ませないため
            }

            vP = x;

            KeyboardState ks = keyboardManager.GetState();
            bool lightEnabled = ks.IsKeyDown(Keys.S);


            if (lightEnabled == true)
            {
                v = vvP;
                x = vPP;
            }
            else
            {
                vvP = v;
                vPP = x;
                v = new Vector3(0, 0, 0);
                x = vPP;

            }

            bool reset = ks.IsKeyDown(Keys.R);
            if (reset == true)
            {
                vV0 = new Vector3(5.0f, 0.0f, 0.0f); //初期速度v0
                vX0 = new Vector3(0.0f, 5.0f, 0.0f); //初期位置x0
                vvP = new Vector3(5.0f, 0.0f, 0.0f);//速度保存
                vPP = new Vector3(0.0f, 5.0f, 0.0f);//位置保存
            }





            mLocal = CreateWorld(vZ, vY, vP);

            world = Matrix.Identity;
            world = mLocal * world;

            prevMouseState = ms;

            //vvP = vP;
            //mLocal = Matrix.Identity;

            /*//バット
            bool bat = ks.IsKeyDown(Keys.T);
            if (bat)
            {
                boxRigidBody.Activate();
                boxRigidBody.AngularVelocity = Vector3.UnitY * 10;
            }
            else
            {
                boxRigidBody.Activate();
                boxRigidBody.AngularVelocity = Vector3.UnitY * 0; ;
            }
            //ボール
            bool boal = ks.IsKeyDown(Keys.B);
            if (boal)
            {
                fallRigidBody.Activate();
                fallRigidBody.AngularVelocity = Vector3.UnitX * 100;
            }
            base.Update(gameTime);
            */
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

        protected override void Draw(GameTime gameTime)
        {

            // Use time in seconds directly
            var time = (float)gameTime.TotalGameTime.TotalSeconds;

            // Clears the screen with the Color.CornflowerBlue
            GraphicsDevice.Clear(Color.CornflowerBlue);
            grid.Draw(Matrix.Identity, view, projection);

            //model.Draw(GraphicsDevice, world, view, projection);
            //DrawModel(model, world, view, projection, gameTime);
            //model.Draw(GraphicsDevice, mLocal, view, projection);

            //model.Draw(GraphicsDevice, fallRigidBody.MotionState.WorldTransform, view, projection);
            //model1.Draw(GraphicsDevice, boxRigidBody.MotionState.WorldTransform, view, projection);

            base.Draw(gameTime);
        }

        void DrawModel(Model model, Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            for (int i = 0; i < 20; i++)
            {
                double cos = Math.Cos(i * Math.PI / 10);
                double sin = Math.Sin(i * Math.PI / 10);
                Vector3 vZ = new Vector3((float)sin, 0, (float)cos);
                Vector3 vY = Vector3.UnitY;
                Vector3 vP = new Vector3(5.0f * (float)sin, 0, 5.0f * (float)cos);

                mLocal = CreateWorld(vZ, vY, vP);

                world = Matrix.Identity;
                world = mLocal * world;
                model.Draw(GraphicsDevice, world, view, projection);
            }
        }

        float ToRadians(float degree)
        {
            return (degree / 180.0f) * (float)Math.PI;
        }

        void SetWireframe()
        {
            var rds = new SharpDX.Direct3D11.RasterizerStateDescription();
            rds.CullMode = SharpDX.Direct3D11.CullMode.Back;
            rds.FillMode = SharpDX.Direct3D11.FillMode.Wireframe;
            RasterizerState rs = RasterizerState.New(GraphicsDevice, rds);
            GraphicsDevice.SetRasterizerState(rs);
        }

        void SetupLights(GameTime gameTime, Model model)
        {
            BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];

            KeyboardState ks = keyboardManager.GetState();

            bool lightEnabled0 = ks.IsKeyDown(Keys.Q);
            bool lightEnabled1 = ks.IsKeyDown(Keys.W);
            bool lightEnabled2 = ks.IsKeyDown(Keys.E);

            double cos = Math.Cos(time * Math.PI / 180);
            double sin = Math.Sin(time * Math.PI / 180);
            //Vector3 cameraPosition0 = new Vector3(0.0f, 1.0f(float)cos, 1.0f(float)sin);
            //Vector3 cameraPosition1 = new Vector3(1.0f(float)cos, 0.0f, 1.0f(float)sin);
            //Vector3 cameraPosition2 = new Vector3(1.0f(float)sin, 1.0f(float)cos, 0.0f);

            effect.LightingEnabled = true;
            effect.DirectionalLight0.Enabled = lightEnabled0;
            effect.DirectionalLight0.DiffuseColor = new Vector3(1, 0, 0);
           // effect.DirectionalLight0.Direction = cameraPosition0;
            effect.DirectionalLight1.Enabled = lightEnabled1;
            effect.DirectionalLight1.DiffuseColor = new Vector3(0, 1, 0);
            //effect.DirectionalLight1.Direction = cameraPosition1;
            effect.DirectionalLight2.Enabled = lightEnabled2;
            effect.DirectionalLight2.DiffuseColor = new Vector3(0, 0, 1);
            //effect.DirectionalLight2.Direction = cameraPosition2;
        }

        void SetupLight(GameTime gameTime, Model model)
        {
            BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];

            MouseState ms = mouseManager.GetState();
            float dx = ms.X - prevMouseState.X;
            float dy = ms.Y - prevMouseState.Y;

            if (ms.LeftButton.Down)
            {
                lightTheta += ms.Y - prevMouseState.Y;
                lightPhi += ms.X - prevMouseState.X;
                if (lightTheta > 1.5f)
                    lightTheta = 1.5f;
                else if (lightTheta < -1.5f)
                    lightTheta = -1.5f;

                if (lightPhi > 1.5f)
                    lightPhi = 1.5f;
                else if (lightPhi < -1.5f)
                    lightPhi = -1.5f;
            }

            Vector3 pos = new Vector3(0, 0, 1);
            Matrix roty = Matrix.RotationX(lightTheta);
            Matrix rotx = Matrix.RotationY(lightPhi);
            pos = Vector3.TransformCoordinate(pos, roty);
            pos = Vector3.TransformCoordinate(pos, rotx);


            effect.LightingEnabled = true;
            effect.DirectionalLight0.Enabled = true;
            effect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
            effect.DirectionalLight0.Direction = pos;


        }

        void SetupLights2(GameTime gameTime, Model model)
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

    }
}