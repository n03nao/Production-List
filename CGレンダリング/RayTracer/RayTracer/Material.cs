using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RayTracer
{
    class Material
    {
        protected double ka;	//�����W��
        protected double kd;	//�g�U���ˌW��
        protected double ks;	//���ʔ��ˌW��
        protected double n;		//Phong�W��
        protected Color color;	//�F

        const int NUM_DATA = 7;

        //�R���X�g���N�^
        public Material()
        {
            ka = 0;
            kd = 0;
            ks = 0;
            n = 0;
            color = Color.FromArgb(0, 0, 0);
        }

        //�l�w��R���X�g���N�^
        public Material(double ka, double kd, double ks,
                        double n, Color col)
        {
            this.ka = ka;
            this.kd = kd;
            this.ks = ks;
            this.n = n;
            this.color = col;
        }

        //�f�[�^�������ꂽ������ŏ�����
        //�}�e���A���f�[�^�t�H�[�}�b�g:r g b ka kd ks n
        public Material(String data)
        {
            int r, g, b;

            try
            {
                //�}�e���A���f�[�^�t�H�[�}�b�g(ka kd ks n color-r g b)
                string[] delimiter = { " " };
                string[] s = data.Split(delimiter, StringSplitOptions.None);//�X�y�[�X���Ƃɕ����񕪊�
                if (s.Length >= NUM_DATA)
                {						//���͂��ꂽ�����K�v�ȏ�Ȃ�ǂݍ���
                    r = Int32.Parse(s[0]);
                    g = Int32.Parse(s[1]);
                    b = Int32.Parse(s[2]);

                    color = Color.FromArgb(r, g, b);

                    ka = Double.Parse(s[3]);
                    kd = Double.Parse(s[4]);
                    ks = Double.Parse(s[5]);
                    n = Double.Parse(s[6]);
                }
                else
                    throw new ArgumentException("[Material]���͒l�̌�������܂���:" + data);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        //�������x
        public double getKa()
        {
            return ka;
        }

        //�g�U�����x
        public double getKd()
        {
            return kd;
        }

        //���ʔ��ˌ����x
        public double getKs()
        {
            return ks;
        }

        //Phong�W��
        public double getN()
        {
            return n;
        }

        //�F
        public Color getColor()
        {
            return color;
        }
    }
}
