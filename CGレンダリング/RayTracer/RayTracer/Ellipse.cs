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
	
	//��_���v�Z
	//E:���_ V:�����x�N�g��
	//�Ԃ�l�F�Փˈʒu�i�Փ˂��Ă��Ȃ��ꍇ-1�j
	public double calcT(Vector3D E, Vector3D V)
	{
		    double alpha, beta, gamma;	//�W���A�萔��
		    double D;		//���ʎ�
		    double K = 1;
		    Matrix M;
		    Vector3D R0d = Vector3D.sub(E, this.pos);   //R0'

            M = Matrix.scaleMatrix(1.0 / (this.a * this.a) , 1.0 / (this.b * this.b), 1.0 / (this.c * this.c));//��
            //M = Matrix.scaleMatrix(1.0 / (this.a * this.a), -1.0 / (this.b * Math.Abs(this.b)), 1.0 / (this.c * this.c));//��t�o�Ȗ�

            alpha = Vector3D.dotProduct(Matrix.tVectorMatrix(V, M), V);

            beta = Vector3D.dotProduct(Matrix.tVectorMatrix(V, M), R0d);

            gamma = Vector3D.dotProduct(Matrix.tVectorMatrix(R0d, M), R0d) - K;

            D = beta * beta - alpha * gamma;//���ʎ�

            if (D<0) {
                return -1;
            }
            else
            {
                return (( -beta -Math.Sqrt(D)) / alpha);
            }
	}
	
	//�@�����v�Z
	//P�F��_
	//�Ԃ�l�F�@��
	public Vector3D calcNorm(Vector3D P)
	{
		Vector3D n;
		Matrix smat = Matrix.scaleMatrix(1 / (this.a * this.a), 
								  		 1 / (this.b * this.b), 
								  		 1 / (this.c * this.c));
								  
		n = Vector3D.normalize(Matrix.matrixVector(smat, Vector3D.sub(P, this.pos)));

		return n;
	}
	
	//�V�F�[�f�B���O�v�Z
	//Q�F�_�����@P�F��_�@E�F���_
	//mat: �g�p����}�e���A��
	public Color calcShading(PointLight Q ,Vector3D P, Vector3D E, 
	 						Material material)
	{
		    double ia, id, s, ii, n;//ia�F�����@id�F�g�U���ˌ��@s�F���ʔ��ˌ��@ii�F�_�����̋��x
            double dot_L_N;//�P�ʌ����x�N�g��L�ƒP�ʖ@���x�N�g��N�Ƃ̓��ς̒l���i�[����ϐ�

		    int pR, pG, pB;
		
		    Vector3D N;//�P�ʖ@���x�N�g��
		    Vector3D L;//�P�ʌ����x�N�g��
            Vector3D R;//�P�ʔ��˃x�N�g��
            Vector3D V;//�P�ʎ����x�N�g��
		    double r;

            ii = Q.getIntensity();//�_�����̋��x
            
            L = Vector3D.sub(Q.getPosition(), P);//��_P�������Q�֌������x�N�g�������߂�
            r = Vector3D.length(L);//��_�ƌ����܂ł̋���
            L = Vector3D.normalize(L);//�P�ʌ����x�N�g�������߂�
            N = this.calcNorm(P);//��_Q�ł̒P�ʖ@���x�N�g��
            V = Vector3D.normalize(Vector3D.sub(E, P));//�P�ʖ@���x�N�g��
            n = material.getN();//�n�C���C�g�W��

            dot_L_N = Vector3D.dotProduct(L, N);

            ia = material.getKa();

            if (dot_L_N < 0) {
                //��������̌�����_�ɏƎ˂��Ă��Ȃ����               
                id = 0;
                s = 0;
            }
            else{
                //��������̌�����_�ɏƎ˂��Ă����

                id = (ii / (r*r)) * material.getKd() * Vector3D.dotProduct(N, L);//Lambert�̗]�����ɏ]���ċ��߂�

                R = Vector3D.sub(Vector3D.scale( 2 * Vector3D.dotProduct(N, L), N) , L);
                s = (ii / (r*r)) * material.getKs() * Math.Pow(Vector3D.dotProduct(R, V), n);//phong�̃��f���ɏ]���ċ��߂�
            }
            

            

		    //���ʔ��ˌ��F
		    Color specColor = Color.FromArgb(255, 255, 255);
		
		    //���̐F
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
	
	//�g�p����}�e���A���ԍ����擾
	//�Ԃ�l�F���̕��̂��g�p����}�e���A���ԍ�
	public int getUsingMaterialIndex()
	{
		return materialIndex;
	}
	
	//�R���X�g���N�^
	public Ellipse()
	{
		a = 0;
		b = 0;
		c = 0;
		
		pos = new Vector3D();
	}
	
	//�l�w��R���X�g���N�^
	public Ellipse(double a, double b, double c, 
				   double x, double y, double z)
	{
		this.a = a;
		this.b = b;
		this.c = c;
		
		this.pos = new Vector3D(x, y, z);
	}
	
	//�f�[�^�������ꂽ������ŏ�����
	//�ȉ~�f�[�^�t�H�[�}�b�g:x y z a b c material_index
	public Ellipse(String data)
	{
		double x, y, z;
	
		try
		{
            string[] delimiter = {" "};
            string[] s = data.Split(delimiter, StringSplitOptions.None);		//�X�y�[�X���Ƃɕ����񕪊�
			if (s.Length >= NUM_DATA){						//���͂��ꂽ�����K�v�ȏ�Ȃ�ǂݍ���
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
                throw new ArgumentException("[Ellipse]���͒l�̌�������܂���:" + data);
		}
        catch (ArgumentException e)
		{
			throw e;
		}
	}
    }
}
