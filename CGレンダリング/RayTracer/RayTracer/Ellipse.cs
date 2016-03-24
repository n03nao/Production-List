using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RayTracer
{
    class Ellipse : BaseShape
    {
        protected double a;
	protected double b;
	protected double c;
	
	protected Vector3D pos;
	protected int materialIndex;
	
	const int NUM_DATA = 7;
	
	//交点を計算
	//E:視点 V:視線ベクトル
	//返り値：衝突位置（衝突していない場合-1）
	public double calcT(Vector3D E, Vector3D V)
	{
		    double alpha, beta, gamma;	//係数、定数項
		    double D;		//判別式
		    double K = 1;
		    Matrix M;
		    Vector3D R0d = Vector3D.sub(E, this.pos);   //R0'

            M = Matrix.scaleMatrix(1.0 / (this.a * this.a) , 1.0 / (this.b * this.b), 1.0 / (this.c * this.c));//球
            //M = Matrix.scaleMatrix(1.0 / (this.a * this.a), -1.0 / (this.b * Math.Abs(this.b)), 1.0 / (this.c * this.c));//一葉双曲面

            alpha = Vector3D.dotProduct(Matrix.tVectorMatrix(V, M), V);

            beta = Vector3D.dotProduct(Matrix.tVectorMatrix(V, M), R0d);

            gamma = Vector3D.dotProduct(Matrix.tVectorMatrix(R0d, M), R0d) - K;

            D = beta * beta - alpha * gamma;//判別式

            if (D<0) {
                return -1;
            }
            else
            {
                return (( -beta -Math.Sqrt(D)) / alpha);
            }
	}
	
	//法線を計算
	//P：交点
	//返り値：法線
	public Vector3D calcNorm(Vector3D P)
	{
		Vector3D n;
		Matrix smat = Matrix.scaleMatrix(1 / (this.a * this.a), 
								  		 1 / (this.b * this.b), 
								  		 1 / (this.c * this.c));
								  
		n = Vector3D.normalize(Matrix.matrixVector(smat, Vector3D.sub(P, this.pos)));

		return n;
	}
	
	//シェーディング計算
	//Q：点光源　P：交点　E：視点
	//mat: 使用するマテリアル
	public Color calcShading(PointLight Q ,Vector3D P, Vector3D E, 
	 						Material material)
	{
		    double ia, id, s, ii, n;//ia：環境光　id：拡散反射光　s：鏡面反射光　ii：点光源の強度
            double dot_L_N;//単位光線ベクトルLと単位法線ベクトルNとの内積の値を格納する変数

		    int pR, pG, pB;
		
		    Vector3D N;//単位法線ベクトル
		    Vector3D L;//単位光線ベクトル
            Vector3D R;//単位反射ベクトル
            Vector3D V;//単位視線ベクトル
		    double r;

            ii = Q.getIntensity();//点光源の強度
            
            L = Vector3D.sub(Q.getPosition(), P);//交点Pから光源Qへ向かうベクトルを求める
            r = Vector3D.length(L);//交点と光源までの距離
            L = Vector3D.normalize(L);//単位光源ベクトルを求める
            N = this.calcNorm(P);//交点Qでの単位法線ベクトル
            V = Vector3D.normalize(Vector3D.sub(E, P));//単位法線ベクトル
            n = material.getN();//ハイライト係数

            dot_L_N = Vector3D.dotProduct(L, N);

            ia = material.getKa();

            if (dot_L_N < 0) {
                //光源からの光が交点に照射していなければ               
                id = 0;
                s = 0;
            }
            else{
                //光源からの光が交点に照射していれば

                id = (ii / (r*r)) * material.getKd() * Vector3D.dotProduct(N, L);//Lambertの余弦測に従って求める

                R = Vector3D.sub(Vector3D.scale( 2 * Vector3D.dotProduct(N, L), N) , L);
                s = (ii / (r*r)) * material.getKs() * Math.Pow(Vector3D.dotProduct(R, V), n);//phongのモデルに従って求める
            }
            

            

		    //鏡面反射光色
		    Color specColor = Color.FromArgb(255, 255, 255);
		
		    //物体色
            Color mColor = material.getColor();

            pR = mColor.R; pG = mColor.G; pB = mColor.B;

            pR = (int)(pR * ia + pR * id + specColor.R * s + 0.5);
            pG = (int)(pG * ia + pG * id + specColor.G * s + 0.5);
            pB = (int)(pB * ia + pB * id + specColor.B * s + 0.5);

            if (pR < 0) pR = 0; else if (pR > 255) pR = 255;
            if (pG < 0) pG = 0; else if (pG > 255) pG = 255;
            if (pB < 0) pB = 0; else if (pB > 255) pB = 255;

            return Color.FromArgb(pR, pG, pB);
	}
	
	//使用するマテリアル番号を取得
	//返り値：この物体が使用するマテリアル番号
	public int getUsingMaterialIndex()
	{
		return materialIndex;
	}
	
	//コンストラクタ
	public Ellipse()
	{
		a = 0;
		b = 0;
		c = 0;
		
		pos = new Vector3D();
	}
	
	//値指定コンストラクタ
	public Ellipse(double a, double b, double c, 
				   double x, double y, double z)
	{
		this.a = a;
		this.b = b;
		this.c = c;
		
		this.pos = new Vector3D(x, y, z);
	}
	
	//データが書かれた文字列で初期化
	//楕円データフォーマット:x y z a b c material_index
	public Ellipse(String data)
	{
		double x, y, z;
	
		try
		{
            string[] delimiter = {" "};
            string[] s = data.Split(delimiter, StringSplitOptions.None);		//スペースごとに文字列分割
			if (s.Length >= NUM_DATA){						//入力された数が必要個以上なら読み込み
                x = Double.Parse(s[0]);
                y = Double.Parse(s[1]);
                z = Double.Parse(s[2]);

                a = Double.Parse(s[3]);
                b = Double.Parse(s[4]);
                c = Double.Parse(s[5]);

                materialIndex = Int32.Parse(s[6]);
				
				pos = new Vector3D(x, y, z);
			}
			else
                throw new ArgumentException("[Ellipse]入力値の個数が足りません:" + data);
		}
        catch (ArgumentException e)
		{
			throw e;
		}
	}
    }
}
