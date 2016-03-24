using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RayTracer
{
    interface BaseShape
    {
        //交点を計算
        //E:視点 V:視線ベクトル
        //返り値：衝突位置（衝突していない場合-1）
        double calcT(Vector3D E, Vector3D V);

        //法線を計算
        //P：交点
        //返り値：法線
        Vector3D calcNorm(Vector3D P);

        //シェーディング計算
        //Q：点光源　P：交点　E：視点
        //mat: 使用するマテリアル
        Color calcShading(PointLight Q, Vector3D P, Vector3D E,
                                         Material material);

        //使用するマテリアル番号を取得
        //返り値：この物体が使用するマテリアル番号
        int getUsingMaterialIndex();
    }
}
