using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RayTracer
{
    interface BaseShape
    {
        //��_���v�Z
        //E:���_ V:�����x�N�g��
        //�Ԃ�l�F�Փˈʒu�i�Փ˂��Ă��Ȃ��ꍇ-1�j
        double calcT(Vector3D E, Vector3D V);

        //�@�����v�Z
        //P�F��_
        //�Ԃ�l�F�@��
        Vector3D calcNorm(Vector3D P);

        //�V�F�[�f�B���O�v�Z
        //Q�F�_�����@P�F��_�@E�F���_
        //mat: �g�p����}�e���A��
        Color calcShading(PointLight Q, Vector3D P, Vector3D E,
                                         Material material);

        //�g�p����}�e���A���ԍ����擾
        //�Ԃ�l�F���̕��̂��g�p����}�e���A���ԍ�
        int getUsingMaterialIndex();
    }
}
