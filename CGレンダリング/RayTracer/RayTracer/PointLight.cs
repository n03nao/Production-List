using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    class PointLight
    {
        protected Vector3D pos;		//�����ʒu
        protected double ii;		//���x

        //�R���X�g���N�^
        public PointLight()
        {
            pos = new Vector3D(0, 0, 0);
            ii = 0;
        }

        //�f�[�^�������ꂽ������ŏ�����
        //�_�����f�[�^�t�H�[�}�b�g:x y z ii
        public PointLight(String data)
        {
            try
            {
                pos = new Vector3D(0, 0, 0);

                //�_�����f�[�^�t�H�[�}�b�g(X Y Z Ii)
                string[] delimiter = { " " };
                string[] s = data.Split(delimiter, StringSplitOptions.None);//�X�y�[�X���Ƃɕ����񕪊�

                if (s.Length >= 4)
                {								//���͂��ꂽ����4�ȏ�Ȃ�ǂݍ���
                    pos.x = Double.Parse(s[0]);    		//�ʒu�A���a
                    pos.y = Double.Parse(s[1]);
                    pos.z = Double.Parse(s[2]);
                    ii = Double.Parse(s[3]);
                }
                else
                    throw new ArgumentException("[plight]���͒l�̌�������܂���:" + data);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        //�ʒu
        public Vector3D getPosition()
        {
            return pos;
        }

        //���x
        public double getIntensity()
        {
            return ii;
        }
    }
}
