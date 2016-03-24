using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;

namespace SharpDXToolkitHelper
{
    /// <summary>
    /// SharpDX.Toolkit用3次元グリッド描画クラス
    /// Author: Tomoaki MORIYA
    /// </summary>
    /// 
    using SharpDX.Toolkit;
    using SharpDX.Toolkit.Graphics;
    using Buffer = SharpDX.Toolkit.Graphics.Buffer;

    public class Grid3D : IDisposable
    {

        #region Field

        /// <summary>
        /// 
        /// </summary>
        GraphicsDevice device;
        
        /// <summary>
        /// 
        /// </summary>
        VertexPositionColor[] vertices;   
        
        /// <summary>
        /// 
        /// </summary>
        Buffer<VertexPositionColor> vertexBuffer;
        
        /// <summary>
        /// 
        /// </summary>
        BasicEffect basicEffect;


        VertexInputLayout inputLayout;
        
        /// <summary>
        /// 
        /// </summary>
        int numLines;

        #region Grid Color

        /// <summary>
        /// 
        /// </summary>
        public static readonly Color colXAxis = Color.Red;
        
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color colYAxis = Color.Green;
        
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color colZAxis = Color.Blue;
        
        /// <summary>
        /// 
        /// </summary>
        public static readonly Color colGrid = Color.Gray;

        #endregion Grid Color

        #endregion Field

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="numGridWidth"></param>
        /// <param name="numGridDepth"></param>
        public Grid3D(GraphicsDevice device, float width, float height, float depth, 
            int numGridWidth, int numGridDepth)
        {
            //GraphicsDeviceの初期化．
            this.device = device;

            InitilizeGrid(width, height, depth, numGridWidth, numGridDepth);
            InitializeEffects();
            InitializeVertices();
        }

        #endregion Constructor

        /// <summary>
        /// 
        /// </summary>
        private void InitializeEffects()
        {
            basicEffect = new BasicEffect(device);
            basicEffect.VertexColorEnabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeVertices()
        {
            vertexBuffer = Buffer.Vertex.New(device, vertices);
            inputLayout = VertexInputLayout.FromBuffer(0, vertexBuffer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;

            device.SetVertexBuffer(vertexBuffer);
            device.SetVertexInputLayout(inputLayout);

            basicEffect.CurrentTechnique.Passes[0].Apply();
            device.Draw(PrimitiveType.LineList, vertexBuffer.ElementCount);
        }

        //頂点設定用補助関数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="color"></param>
        /// <param name="indexLine"></param>
        /// <param name="dst"></param>
        private void SetLineVertices(Vector3 p1, Vector3 p2, Color color,
            int indexLine, ref VertexPositionColor[] dst)
        {
            int indexP1 = indexLine * 2;
            int indexP2 = (indexLine * 2) + 1;

            dst[indexP1].Position = p1;
            dst[indexP1].Color = color;
            dst[indexP2].Position = p2;
            dst[indexP2].Color = color;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        /// <param name="numGridWidth"></param>
        /// <param name="numGridDepth"></param>
        private void InitilizeGrid(float width, float height, float depth, int numGridWidth, int numGridDepth)
        {
            //頂点の数を算出
            int numAxis = 5;
            numLines = numAxis + numGridWidth * 2 + numGridDepth * 2;

            vertices = new VertexPositionColor[numLines * 2];

            //X,Y,Z軸を設定
            SetLineVertices(Vector3.Zero, new Vector3(width, 0, 0), colXAxis, 0, ref vertices);
            SetLineVertices(Vector3.Zero, new Vector3(-width, 0, 0), colGrid, 1, ref vertices);
            SetLineVertices(Vector3.Zero, new Vector3(0, 0, depth), colZAxis, 2, ref vertices);
            SetLineVertices(Vector3.Zero, new Vector3(0, 0, -depth), colGrid, 3, ref vertices);
            SetLineVertices(Vector3.Zero, new Vector3(0, height, 0), colYAxis, 4, ref vertices);

            //グリッドを設定
            int lines = numAxis;
            float dX = width / numGridWidth;
            float dZ = depth / numGridDepth;

            for (int x = 1; x <= numGridWidth; ++x)
            {
                SetLineVertices(new Vector3(-x * dX, 0, -depth), new Vector3(-x * dX, 0, depth), 
                    colGrid, lines, ref vertices);
                ++lines;

                SetLineVertices(new Vector3( x * dX, 0, -depth), new Vector3( x * dX, 0, depth),
                    colGrid, lines, ref vertices);
                ++lines;
            }

            for (int z = 1; z <= numGridDepth; ++z)
            {
                SetLineVertices(new Vector3(-width, 0, -z * dZ), new Vector3( width, 0, -z * dZ),
                   colGrid, lines, ref vertices);
                ++lines;

                SetLineVertices(new Vector3(-width, 0,  z * dZ), new Vector3(width, 0,   z * dZ),
                    colGrid, lines, ref vertices);
                ++lines;
            }
        }

        public void Dispose()
        {
            basicEffect.Dispose();
            basicEffect = null;

            vertexBuffer.Dispose();
            vertexBuffer = null;
        }
    }
}
