using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    //行列クラス
    class Matrix
    {
        public double m11, m12, m13;
        public double m21, m22, m23;
        public double m31, m32, m33;

        //単位行列をつくる
        public static Matrix identityMatrix()
        {
            Matrix m = new Matrix();

            m.m11 = 1; m.m12 = 0; m.m13 = 0;
            m.m21 = 0; m.m22 = 1; m.m23 = 0;
            m.m31 = 0; m.m32 = 0; m.m33 = 1;

            return m;
        }

        //拡大縮小行列をつくる
        public static Matrix scaleMatrix(double scaleX, double scaleY, double scaleZ)
        {
            Matrix m = Matrix.identityMatrix();

            m.m11 = scaleX; m.m22 = scaleY; m.m33 = scaleZ;

            return m;
        }

        //ベクトル-行列積
        public static Vector3D tVectorMatrix(Vector3D v, Matrix m)
        {
            Vector3D rv = new Vector3D();

            rv.x = v.x * m.m11 + v.y * m.m21 + v.z * m.m31;
            rv.y = v.x * m.m12 + v.y * m.m22 + v.z * m.m32;
            rv.z = v.x * m.m13 + v.y * m.m23 + v.z * m.m33;

            return rv;
        }

        //行列-ベクトル積
        public static Vector3D matrixVector(Matrix m, Vector3D v)
        {
            Vector3D rv = new Vector3D();

            rv.x = v.x * m.m11 + v.y * m.m12 + v.z * m.m13;
            rv.y = v.x * m.m21 + v.y * m.m22 + v.z * m.m23;
            rv.z = v.x * m.m31 + v.y * m.m32 + v.z * m.m33;

            return rv;
        }
    }
}
