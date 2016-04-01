/* 読み込んだ画像を画像濃度127で２値化 */
#include <windows.h>
#include	<stdio.h>
#include	"opgls.ft"

unsigned char 	dat[1024*1024];
int	 			cm, wide, hite;
unsigned char  th[16][16];  //本来ならth[3][3]で良いが、サイズは大きめにとってある
	
void	get_data (int  agc, char  **agv){
	if (agc != 2){
		printf ("使用法:: %s  file.bmp\n", agv[0]);		exit(-1);
	}
	loadbmpfile (agv[1], &cm, &hite, &wide, dat);
}

void	picture_and_hist (void){	// 原画像と２値化ヒ画像の表示
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
		glcolor (r, r, r);			// 原画像を
		gpnt (x, y);				// 第１象限に表示
		
		if (r <= th[x % 4][y % 4])	r = 0; 		
		else			r = 255;	// そうでなければ r = 255
		glcolor (r, r, r);			// ２値化画像を
		gpnt (x-wide, y);			// 第２象限に表示

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