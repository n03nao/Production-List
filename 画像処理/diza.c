/* �ǂݍ��񂾉摜���摜�Z�x127�łQ�l�� */
#include <windows.h>
#include	<stdio.h>
#include	"opgls.ft"

unsigned char 	dat[1024*1024];
int	 			cm, wide, hite;
unsigned char  th[16][16];  //�{���Ȃ�th[3][3]�ŗǂ����A�T�C�Y�͑傫�߂ɂƂ��Ă���
	
void	get_data (int  agc, char  **agv){
	if (agc != 2){
		printf ("�g�p�@:: %s  file.bmp\n", agv[0]);		exit(-1);
	}
	loadbmpfile (agv[1], &cm, &hite, &wide, dat);
}

void	picture_and_hist (void){	// ���摜�ƂQ�l���q�摜�̕\��
	unsigned char	r ;
	int				x, y, i, j;
	glclear ( ) ;
	

    for (y=0; y<hite; y++) for (x=0; x<wide; x++){

		r = dat[y*wide+x];

		th[0][0]= 16; th[1][0]=144; th[2][0]= 48; th[3][0]=176;
		th[0][1]=208; th[1][1]= 80; th[2][1]=240; th[3][1]=100;
		th[0][2]= 64; th[1][2]=192; th[2][2]= 32; th[3][2]=160;
		th[0][3]=255; th[1][3]=128; th[2][3]=224; th[3][3]= 96;

		
		//for (i=0; i<3; i++) for (j=0; j<3; j++){
		//	th[i][j] = r;
		//}
		glcolor (r, r, r);			// ���摜��
		gpnt (x, y);				// ��P�ی��ɕ\��
		
		if (r <= th[x % 4][y % 4])	r = 0; 		
		else			r = 255;	// �����łȂ���� r = 255
		glcolor (r, r, r);			// �Q�l���摜��
		gpnt (x-wide, y);			// ��Q�ی��ɕ\��

 	}	
 	glflush ( );			
}

int	main (int  argc, char  **argv)
{
	get_data(argc,argv);
	ginit(1);
	gdisplay(picture_and_hist);
	return 0;
}