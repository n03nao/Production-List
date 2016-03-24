using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class ViewPointRotate
    {
        private Matrix R = new Matrix();
        private Matrix S = new Matrix();
        private Matrix T = new Matrix();
        /*	public ViewPointRotate(double xz)
            {
        //		this.R.m11 = Math.Cos(xz);	this.R.m12 = 0;		this.R.m13 = 0;
        //		this.R.m21 = 0;			this.R.m22 = 1;		this.R.m23 = 0;
        //		this.R.m31 = 0;			this.R.m32 = 0;		this.R.m33 = Math.Sin(xz);

                this.R.m11 = Math.Cos(xz);	this.R.m12 = 0;	this.R.m13 = -Math.Sin(xz);
                this.R.m21 = 0;			this.R.m22 = 1;	this.R.m23 = 0;
                this.R.m31 = Math.Sin(xz);	this.R.m32 = 0;	this.R.m33 = Math.Cos(xz);
		
            }
        */
        public ViewPointRotate(double xz, double y)
        {
            this.R.m11 = Math.Cos(xz); this.R.m12 = 0; this.R.m13 = -Math.Sin(xz);
            this.R.m21 = 0; this.R.m22 = 1; this.R.m23 = 0;
            this.R.m31 = Math.Sin(xz); this.R.m32 = 0; this.R.m33 = Math.Cos(xz);

            this.S.m11 = Math.Cos(y); this.S.m12 = Math.Sin(y); this.S.m13 = 0;
            this.S.m21 = -Math.Sin(y); this.S.m22 = Math.Cos(y); this.S.m23 = 0;
            this.S.m31 = 0; this.S.m32 = 0; this.S.m33 = 1;

            this.T.m11 = 1; this.T.m12 = 0; this.T.m13 = 0;
            this.T.m21 = 0; this.T.m22 = Math.Cos(y); this.T.m23 = -Math.Sin(y);
            this.T.m31 = 0; this.T.m32 = Math.Sin(y); this.T.m33 = Math.Cos(y);

        }

        public Vector3D RunRotate(Vector3D A)
        {
            Vector3D B = new Vector3D();
            B = Matrix.tVectorMatrix(A, R);
            B = Matrix.tVectorMatrix(B, S);
            B = Matrix.tVectorMatrix(B, T);
            return B;
        }
    }
}
